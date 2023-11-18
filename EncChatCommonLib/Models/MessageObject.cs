using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncChatCommonLib.Models
{
    public class Parameter
    {
        public string ParamName { get; set; }
        public object ParamValue { get; set; }
    }

    public class MessageObject
    {
        public string MyConnectionId { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public List<Parameter> MethodParams { get; set; } = new List<Parameter>();
    }
}
