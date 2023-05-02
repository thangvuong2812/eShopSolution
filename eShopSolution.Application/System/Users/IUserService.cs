using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.System.Users
{
    public interface IUserService
    {
        Task<string> Authenticate(LoginRequest loginRequest);
        Task<bool> Register(RegisterRequest registerRequest);

        Task<bool> Authenticate2FA(string token2FA);
    }
}
