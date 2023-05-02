using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Utilities
{
    public class SystemBase
    {
        private readonly IConfiguration _configuration;
        public SystemBase(IConfiguration configuration)
        {
        
            
            _configuration = configuration;
        }

        public string GetBaseURL()
        {
            return _configuration.GetSection("profiles").GetSection("eShopSolution.AdminApp").GetSection("applicationUrl").Value.Split(";")[0];
        }
    }
}
