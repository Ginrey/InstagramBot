using System;
using System.Collections.Generic;
using InstagramBot.Data;
using InstagramBot.Data.SQL;
using InstagramBot.IO;
using InstagramBot.Net;
using InstagramBot.Net.Web;
using Telegram.Bot;

namespace InstagramBot
{
    public class Session
    {
        public Connection Connection { get; }
        public BlackList BlackList = new BlackList();
        List<MySqlDatabase> listsql = new List<MySqlDatabase>();
        public Multithreading Multithreading { get; set; }
        int indexSql = 0, indexWeb = 0;
        public Languages Language { get; set; } = new Languages();
        public List<long> BlockedList { get; set; }

        public MySqlDatabase MySql
        {
            get
            {
                indexSql++;
                if (indexSql == listsql.Count - 1) indexSql = 0;
                return listsql[indexSql];
            }
        }

        public TelegramBotClient Bot { get; set; }
        public WebInstagram WebInstagram
        {
            get
            {
                indexWeb++;
                if (indexWeb > ListWebInstagram.Count - 1) indexWeb = 0;
                return ListWebInstagram[indexWeb];
            }
        }
      
        public List<WebInstagram> ListWebInstagram { get; set; }
        string Token { get; }
        bool Started { get; set; }
        
        public Session(string token = "")
        {
            Token = token;
            if (!string.IsNullOrEmpty(Token)) Bot = new TelegramBotClient(Token);
            string connectionString = "SERVER=WIN-344VU98D3RU\\SQLEXPRESS;DATABASE=Instagram_DB;Trusted_Connection=True";
#if DEBUG
            connectionString = "SERVER=DESKTOP-VBFBI8T;DATABASE=Instagram_DB;Trusted_Connection=True";
#endif
            for (int i = 0; i < 100; i++)
            {
                //DESKTOP-VBFBI8T
                //WIN-344VU98D3RU\\SQLEXPRESS
                listsql.Add(new MySqlDatabase(connectionString));
                listsql[i].Connect();
            }
            Multithreading = new Multithreading(Environment.ProcessorCount);
            Connection = new Connection(this);
            Menu.Session = this;
            BlockedList = MySql.GetBlockList();
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
