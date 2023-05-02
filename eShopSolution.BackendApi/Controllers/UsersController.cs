using eShopSolution.Application.System.Users;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody]LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var token = await _userService.Authenticate(loginRequest);
            if (string.IsNullOrEmpty(token))
                return NotFound("Username or password is invalid");
            return Ok(token);
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.Register(registerRequest);
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpPost("auth2FA")]
        [AllowAnonymous]
        public async Task<IActionResult> Auth2FA([FromBody] ConfirmEmailRequest confirmEmailRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var isSucceed = await _userService.Authenticate2FA(confirmEmailRequest.Code);
            if (isSucceed)
                return Ok();
            return BadRequest();
        }
    }
}
