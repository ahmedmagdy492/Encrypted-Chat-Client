﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncChatCommonLib.Models
{
    public class SharedConstants
    {
        public static readonly byte[] SERVER_IPV4 = new byte[] { 192, 168, 1, 2 };
        public const int SERVER_PORT = 55332;
        public static readonly byte[] IV = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
        public static readonly byte[] SHARED_KEY = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
        public const string IDENTITY_API_BASE_URL = "https://localhost:7014/";
    }
}
