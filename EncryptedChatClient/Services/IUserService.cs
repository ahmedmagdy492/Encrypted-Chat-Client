using EncChatCommonLib.Models;
using EncChatCommonLib.ViewModels;
using EncryptedChatClient.Models;

namespace EncryptedChatClient.Services
{
    internal interface IUserService
    {
        Task<User> GetUserById(string id);
        Task<ResultObject> Login(LoginViewModel user);
        Task<ResultObject> Signup(User user);
    }
}