using EncChatCommonLib.Models;
using Microsoft.EntityFrameworkCore;

namespace Enc_Chat_Identity_API.Database
{
    public class EncChatDB : DbContext
    {
        public EncChatDB(DbContextOptions<EncChatDB> options)
        : base(options) { }

        public DbSet<User> Users => Set<User>();
    }
}
