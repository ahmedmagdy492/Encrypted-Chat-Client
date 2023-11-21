using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EncryptedChatClient.Models
{
    internal class ResultObject
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Result { get; set; }
    }
}
