using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Models.Authentication;
using schedulerfrontend.Helpers;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

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
        public async Task<LoginResult> Login(LoginModel loginModel)
        {
            var loginAsJson = JsonSerializer.Serialize(loginModel);
            var response = await _httpClient.PostAsync("api/login", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
            var loginResult = JsonSerializer.Deserialize<LoginResult>(await response.Content.ReadAsStringAsync(), 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
            if (!response.IsSuccessStatusCode)
            {
                return loginResult!;
            }
            await _localStorageService.SetItemAsync("authToken", loginResult!.Token);
            ((ApiAuthenticationStateProvider)_stateProvider).MarkUserAsAuthenticated(loginModel.Email!);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);
            return loginResult;
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

      
    }
}
