using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Models.Authentication;
using System.Net.Http.Json;

namespace schedulerfrontend.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _stateProvider;
        private readonly ILocalStorageService _localStorageService;
        public AuthService(HttpClient httpClient, AuthenticationStateProvider stateProvider, 
            ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _stateProvider = stateProvider;
            _localStorageService = localStorageService;
        }
        public async Task<RegisterResult> Register(RegisterModel registerModel)
        {
            var result = await _httpClient.PostAsJsonAsync("api/accounts", registerModel);
            if (!result.IsSuccessStatusCode)
            {
                return new RegisterResult { Successful = false, Errors = new List<string>() { "Error occured" } };
            }
            return new RegisterResult { Successful = true, Errors = new List<string>() { "Account Created successfully " } };
        }
        public Task<LoginResult> Login(LoginModel loginModel)
        {
            throw new NotImplementedException();
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

      
    }
}
