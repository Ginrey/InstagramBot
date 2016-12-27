using InstagramBot.Data.SQL;
using InstagramBot.Net;
using Telegram.Bot;

namespace InstagramBot
{
    public class Session
    {
        public Connection Connection { get; }
        public MySqlDatabase MySql { get; set; }
        public TelegramBotClient Bot { get; set; }
        string Token { get; }
        bool Started { get; set; }
        public Session(string token = "")
        {
            Token = token;
            if (!string.IsNullOrEmpty(Token)) Bot = new TelegramBotClient(Token);
            Connection = new Connection(this);
            MySql = new MySqlDatabase("SERVER=DESKTOP-VBFBI8T;DATABASE=Instagram_DB;Trusted_Connection=True");
        }

        public void Start()
        {
            lock (Connection)
            {
                if (Started)
                {
                    return;
                }
                Started = true;
            }
            Connection.Connect();
        }

        public void Stop()
        {
            lock (Connection)
            {
                if (!Started)
                {
                    return;
                }
                Started = false;
            }
            Connection.Close();
        }
    }
}
