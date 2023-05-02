using eShopSolution.AdminApp.Utilities;
using eShopSolution.ViewModels.System.Users;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public class UserApiClient : IUserApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string _baseUrl;
        public UserApiClient (IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            var builder = new ConfigurationBuilder().AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "Properties", "launchSettings.json"));
            var configuration = builder.Build();
            _baseUrl = new SystemBase(configuration).GetBaseURL();
        }
        public async Task<string> Authenticate(LoginRequest loginRequest)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_baseUrl);
            var json = JsonConvert.SerializeObject(loginRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await client.PostAsync("/api/users/login", httpContent);
            
            if (res.IsSuccessStatusCode)
            {
                var token = await res.Content.ReadAsStringAsync();
                return token;
            }
            return string.Empty;
        }

        public async Task<bool> Authenticate2FA(ConfirmEmailRequest confirmEmailRequest)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_baseUrl);
            var json = JsonConvert.SerializeObject(confirmEmailRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await client.PostAsync("/api/users/auth2FA", httpContent);
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> Register(RegisterRequest registerRequest)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_baseUrl);
            var json = JsonConvert.SerializeObject(registerRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await client.PostAsync("/api/users/register", httpContent);
            return res.IsSuccessStatusCode;
        }
    }
}
