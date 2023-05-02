using eShopSolution.AdminApp.Services;
using eShopSolution.Application.Services;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Controllers
{
    public class UserController : Controller
    {
        
        private readonly IConfiguration _configuration;
        private readonly IUserApiClient _userApiClient;
        public UserController(IConfiguration configuration,IUserApiClient userApiClient)
        {
            
            _userApiClient = userApiClient;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> LoginAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Prevent Call Api By Third-party (PostMan, ...)
        public async Task<IActionResult> LoginAsync(LoginRequest loginRequest,[FromQuery]string ReturnUrl)
        {
            if (!ModelState.IsValid)
                return View(loginRequest);
            var token = await _userApiClient.Authenticate(loginRequest);
            //Giai ma token (Decode)
            if (token == null)
            {
                ModelState.AddModelError("", "Username or password is invalid!");
                return View(loginRequest);
            }
            var isConfirmedEmailStr = false;
            var userPrinciple = ValidateToken(token);
            Boolean.TryParse(userPrinciple.FindFirstValue(ClaimTypes.Authentication),out isConfirmedEmailStr);
            if(!isConfirmedEmailStr)
            {
                string email = userPrinciple.FindFirstValue(ClaimTypes.Email);
                return RedirectToAction("ConfirmEmail", new ConfirmEmailRequest { Email = email });
            }
            var authProperties = new AuthenticationProperties
            {

                ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(60),
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,userPrinciple,authProperties);
            return Redirect(ReturnUrl ?? "/");
        }

        public IActionResult Logout()
        {
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult Forbidden()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
                return View(registerRequest);

            var isSucceeded = await _userApiClient.Register(registerRequest);
            if (!isSucceeded)
                return View(registerRequest);


            return RedirectToAction("ConfirmEmail", new ConfirmEmailRequest { Email = registerRequest.Email });
        }

        [HttpGet]
        public IActionResult ConfirmEmail(string email)
        {
            ConfirmEmailRequest confirmEmailRequest = new ConfirmEmailRequest { Email = email };
            return View(confirmEmailRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailRequest confirmEmailRequest)
        {
            
            if (!ModelState.IsValid)
                return View(confirmEmailRequest);

            var isSucceed = await _userApiClient.Authenticate2FA(confirmEmailRequest);
            if(isSucceed)
                return RedirectToAction("Login");
            return View(confirmEmailRequest);
                
        }
        private ClaimsPrincipal ValidateToken(string token)
        {
            IdentityModelEventSource.ShowPII = true;
            SecurityToken validateToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;
            validationParameters.ValidAudience = _configuration.GetSection("Tokens").GetSection("Issuer").Value;
            validationParameters.ValidIssuer = _configuration.GetSection("Tokens").GetSection("Issuer").Value;
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Tokens").GetSection("Key").Value));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out validateToken);

            Debug.WriteLine(validateToken);

            return principal;
        }

    }
}
