using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EncChatCommonLib.Services
{
    public static class HelperService
    {
        public static bool IsValidEmail(string email)
        {
            var regex = new Regex(@"[A-Za-z]+[0-9]*@[A-Za-z]+\.[A-Za-z]{1,3}");
            return regex.IsMatch(email);
        }
    }
}
