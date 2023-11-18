using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Enc_Chat_Server.Services
{
    internal static class LoggerService
    {
        public static void LogWarning(string msg)
        {
            ConsoleColor originalColor = ForegroundColor;
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine(msg);
            ForegroundColor = originalColor;
        }

        public static void LogSuccess(string msg)
        {
            ConsoleColor originalColor = ForegroundColor;
            ForegroundColor = ConsoleColor.Green;
            WriteLine(msg);
            ForegroundColor = originalColor;
        }

        public static void LogError(string msg)
        {
            ConsoleColor originalColor = ForegroundColor;
            ForegroundColor = ConsoleColor.Red;
            WriteLine(msg);
            ForegroundColor = originalColor;
        }

        public static void LogSpecialMessage(string msg)
        {
            ConsoleColor originalForeColor = ForegroundColor;
            ConsoleColor originalBackColor = BackgroundColor;
            BackgroundColor = ConsoleColor.Green;
            Write("Message: ");
            BackgroundColor = originalBackColor;
            ForegroundColor = ConsoleColor.Green;
            WriteLine(msg);
            ForegroundColor = originalForeColor;
        }
    }
}
