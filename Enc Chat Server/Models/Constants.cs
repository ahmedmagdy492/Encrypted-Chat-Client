using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enc_Chat_Server.Models
{
    internal static class Constants
    {
        public const int PORT = 55332;
        public const int MAX_MSG_SIZE_IN_BYTES = 4096;
        public static readonly byte[] SHARED_KEY = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
        public const string PUBLIC_KEY_XML = "<RSAKeyValue><Modulus>vosBzWwkjlliRWJ1bTI/8s/RscDp3GKLOKI1Osklmstq9yD/9j6fN7CHmI5n4kWWUTFsbZrfs9VyPf8Owp1AHswUF2X+SUUQ33oPJAl6gMytenhNEmIXZCoAnfhawPZpBAwpUxF0T8yNFdf39VJbp6FK0AnLtZ6hcPJ4J85BCZE=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
    }
}
