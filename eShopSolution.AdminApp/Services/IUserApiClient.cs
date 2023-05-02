using eShopSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public interface IUserApiClient
    {

        Task<string> Authenticate(LoginRequest loginRequest);

        Task<bool> Register(RegisterRequest registerRequest);

        Task<bool> Authenticate2FA(ConfirmEmailRequest confirmEmailRequest);


    }
}
