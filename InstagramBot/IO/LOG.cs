using System;
using System.IO;
using System.Text;

namespace InstagramBot.IO
{
   public static class LOG
    {
        public static bool Enabled { get; set; } = true;
        public static void Add(string type, string text)
        {
            if(Enabled)
            File.AppendAllText("Log.txt", $"{DateTime.Now.ToShortDateString()} {type}: {text}\n", Encoding.UTF8);
        }
    }
}
