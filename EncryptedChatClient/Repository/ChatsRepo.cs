using EncChatCommonLib.Models;
using EncryptedChatClient.Database;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptedChatClient.Repository
{
    public class ChatsRepo : IChatsRepo
    {
        private readonly ChatLocalDB _chatLocalDB;

        public ChatsRepo(ChatLocalDB chatLocalDB)
        {
            _chatLocalDB = chatLocalDB;
        }

        public async Task<List<Conversation>> GetConversations()
        {
            return await _chatLocalDB.Conversations.Include("ChatMessages").ToListAsync();
        }

        public async Task<List<ChatMessage>> GetChatMessagesOfConversation(string conversationId)
        {
            return await _chatLocalDB.ChatMessages.Where(c => c.ConversationId == conversationId).ToListAsync();
        }

        public async Task<bool> SaveConversationsBulk(List<Conversation> conversations)
        {
            foreach (var conversation in conversations)
            {
                if(_chatLocalDB.Conversations.Any(c => c.ConversationId == conversation.ConversationId))
                {
                    foreach(var message in conversation.ChatMessages)
                    {
                        var msgIdParam = new SqliteParameter("@msgId", Guid.NewGuid().ToString("N"));
                        var conversationIdParam = new SqliteParameter("@conversationId", conversation.ConversationId);
                        var msgContentParam = new SqliteParameter("@msgContent", message.MsgContent);
                        var msgDateParam = new SqliteParameter("@msgDate", message.MsgDate);
                        var msgTypeParam = new SqliteParameter("@msgType", message.MsgType);
                        var msgStatusParam = new SqliteParameter("@msgStatus", message.MsgStatus);
                        var senderIdParam = new SqliteParameter("@senderId", message.SenderId);
                        var receiverIdParam = new SqliteParameter("@receiverId", message.ReceiverId);
                        
                        await _chatLocalDB.Database.ExecuteSqlRawAsync("INSERT INTO ChatMessages (MessageId, ConversationId, MsgContent, MsgDate, MsgType, MsgStatus, SenderId, ReceiverId) VALUES (@msgId, @conversationId, @msgContent, @msgDate, @msgType, @msgStatus, @senderId, @receiverId)", msgIdParam, conversationIdParam, msgContentParam, msgDateParam, msgTypeParam, msgStatusParam, senderIdParam, receiverIdParam);
                    }
                }
                else
                {
                    var conversationIdParam = new SqliteParameter("@conversationId", conversation.ConversationId);
                    var withWhomIdParam = new SqliteParameter("@withWhomId", conversation.WithWhomId);
                    var withWhomNameParam = new SqliteParameter("@withWhomName", conversation.WithWhomName);

                    await _chatLocalDB.Database.ExecuteSqlRawAsync("INSERT INTO Conversations (ConversationId, WithWhomId, WithWhomName) VALUES (@conversationId, @withWhomId, @withWhomName)", conversationIdParam, withWhomIdParam, withWhomNameParam);

                    foreach (var message in conversation.ChatMessages)
                    {
                        var msgIdParam = new SqliteParameter("@msgId", Guid.NewGuid().ToString("N"));
                        conversationIdParam = new SqliteParameter("@conversationId", conversation.ConversationId);
                        var msgContentParam = new SqliteParameter("@msgContent", message.MsgContent);
                        var msgDateParam = new SqliteParameter("@msgDate", message.MsgDate);
                        var msgTypeParam = new SqliteParameter("@msgType", message.MsgType);
                        var msgStatusParam = new SqliteParameter("@msgStatus", message.MsgStatus);
                        var senderIdParam = new SqliteParameter("@senderId", message.SenderId);
                        var receiverIdParam = new SqliteParameter("@receiverId", message.ReceiverId);

                        await _chatLocalDB.Database.ExecuteSqlRawAsync("INSERT INTO ChatMessages (MessageId, ConversationId, MsgContent, MsgDate, MsgType, MsgStatus, SenderId, ReceiverId) VALUES (@msgId, @conversationId, @msgContent, @msgDate, @msgType, @msgStatus, @senderId, @receiverId)", msgIdParam, conversationIdParam, msgContentParam, msgDateParam, msgTypeParam, msgStatusParam, senderIdParam, receiverIdParam);
                    }
                }
            }

            return true;
        }
    }
}
