using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncChatCommonLib.Models
{
    public class Conversation
    {
        [Key]
        public string ConversationId { get; set; } = Guid.NewGuid().ToString("N");
        public string WithWhomId { get; set; }
        public string WithWhomName { get; set; }

        public List<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
    }
}
