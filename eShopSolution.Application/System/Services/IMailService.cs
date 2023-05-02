using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Services
{
    public interface IMailService
    {
        Task<bool> SendMailTwoFactor(string email, string code);
            
    }
}
