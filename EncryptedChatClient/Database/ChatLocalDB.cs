using EncChatCommonLib.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptedChatClient.Database
{
    public class ChatLocalDB : DbContext
    {
        public ChatLocalDB()
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite("Data Source=chats.db");

        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ChatMessage> ChatMessages {  get; set; }
    }
}
