using EncChatCommonLib.Models;
using EncChatCommonLib.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Enc_Chat_Server.Services
{
    internal class UserService : IDisposable, IUserService
    {
        public readonly HttpClient _httpClient;

        public UserService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(SharedConstants.IDENTITY_API_BASE_URL)
            };
        }

        public async Task<List<User>> GetUsers(int pageNo = 1, int pageSize = 10)
        {
            return await _httpClient.GetFromJsonAsync<List<User>>($"users/{pageNo}/{pageSize}");
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
