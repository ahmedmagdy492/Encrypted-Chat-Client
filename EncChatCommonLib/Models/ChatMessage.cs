using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncChatCommonLib.Models
{
    public enum MessageType
    {
        TextPlain,
        ImageBase64,
    }

    public enum MessageStatus
    {
        Sent,
        ToBeSent,
        FailedToSend,
        UnRead,
        Read
    }

    public class ChatMessage
    {
        [Key]
        public string MessageId { get; set; } = Guid.NewGuid().ToString("N");
        [ForeignKey(nameof(Conversation))]
        public string ConversationId { get; set; }
        public string MsgContent { get; set; }
        public DateTime? MsgDate { get; set; }
        public MessageType MsgType { get; set; }
        public MessageStatus MsgStatus { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }

        public Conversation Conversation { get; set; }
    }
}
