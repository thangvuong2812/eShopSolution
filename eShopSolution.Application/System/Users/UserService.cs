using DataAccess.Models;
using eShopSolution.Application.Services;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMailService _mailService;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;
        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager, IConfiguration configuration, IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mailService = mailService;
        }

        public async Task<string> Authenticate(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByNameAsync(loginRequest.UserName);
            if (user == null)
                return null;
            var result = await _signInManager.PasswordSignInAsync(user, loginRequest.Password, loginRequest.IsRemember, true);
            if (!result.Succeeded)
                return null;
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.LastName + " " + user.FirstName),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Authentication, user.EmailConfirmed.ToString()),
                new Claim(ClaimTypes.Role, string.Join(";",roles))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Tokens").GetSection("Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration.GetSection("Tokens").GetSection("Issuer").Value, _configuration.GetSection("Tokens").GetSection("Issuer").Value,claims, expires: DateTime.Now.AddHours(2),signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> Register(RegisterRequest registerRequest)
        {

            var user = new User
            {
                FirstName = registerRequest.FirstName,
                DOB = registerRequest.Dob,
                Email = registerRequest.Email,
                LastName = registerRequest.LastName,
                UserName = registerRequest.UserName,
                PhoneNumber = registerRequest.PhoneNumber,
                TwoFactorEnabled = true
            };
            
            var result = await _userManager.CreateAsync(user, registerRequest.Password);
            if(result.Succeeded)
            {
                var token2FA = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
                var isSucceeded = await _mailService.SendMailTwoFactor(user.Email, token2FA);
                return isSucceeded;
            }
            return false;
        }

        public async Task<string> GenerateTwoFactorTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return string.Empty;
            var token2FA = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider);

            return token2FA;
        }

        public async Task<bool> Authenticate2FA(string token2FA)
        {

            //BUG at here
            var result = await _signInManager.TwoFactorSignInAsync(TokenOptions.DefaultEmailProvider, token2FA, false, false);
            return result.Succeeded;
        }
    }
}
