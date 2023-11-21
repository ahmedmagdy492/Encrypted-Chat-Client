using EncChatCommonLib.Models;
using EncChatCommonLib.ViewModels;
using EncryptedChatClient.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace EncryptedChatClient.Services
{
    internal class UserService : IUserService, IDisposable
    {
        public readonly HttpClient _httpClient;

        public UserService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(Constants.IDENTITY_API_BASE_URL)
            };
        }

        public async Task<ResultObject> Signup(User user)
        {
            var httpResponse = await _httpClient.PostAsJsonAsync("signup", user);
            return new ResultObject
            {
                StatusCode = httpResponse.StatusCode,
                Result = await httpResponse.Content.ReadAsStringAsync()
            };
        }

        public async Task<ResultObject> Login(LoginViewModel user)
        {
            var httpResponse = await _httpClient.PostAsJsonAsync("login", user);
            return new ResultObject
            {
                StatusCode = httpResponse.StatusCode,
                Result = await httpResponse.Content.ReadAsStringAsync()
            };
        }

        public async Task<User> GetUserById(string id)
        {
            return await _httpClient.GetFromJsonAsync<User>($"users/{id}");
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
