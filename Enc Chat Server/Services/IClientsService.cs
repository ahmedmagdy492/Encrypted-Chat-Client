using EncChatCommonLib.Models;

namespace Enc_Chat_Server.Services
{
    internal interface IClientsService
    {
        void Dispose();
        Task<List<User>> GetUsers(int pageNo = 1, int pageSize = 10);
    }
}