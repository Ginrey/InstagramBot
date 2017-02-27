using System;
using System.IO;
using System.Text;

namespace InstagramBot.IO
{
   public static class LOG
    {
        public static bool Enabled { get; set; } = true;
        public static void Add(string type, Exception ex)
        {
            if (!Enabled) return;
            string stack = ex.StackTrace ?? "";
            File.AppendAllText("Log.txt", $"{DateTime.Now.ToShortDateString()} {type}: {ex.Message}\r\n {stack}\r\n\r\n", Encoding.UTF8);
        }
    }
}
