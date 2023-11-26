using EncChatCommonLib.Models;

namespace EncryptedChatClient.Repository
{
    public interface IChatsRepo
    {
        Task<List<ChatMessage>> GetChatMessagesOfConversation(string conversationId);
        Task<List<Conversation>> GetConversations();
        Task<bool> SaveConversationsBulk(List<Conversation> conversations);
    }
}