#region

using System;
using System.Collections.Generic;
using InstagramBot.Data.Accounts;
using InstagramBot.Data.SQL;
using InstagramBot.IO;
using InstagramBot.Net;
using InstagramBot.Net.Web;
using Telegram.Bot;

#endregion

namespace InstagramBot
{
    public class Session
    {
        int indexSql, indexWeb;

        readonly List<MySqlDatabase> listsql = new List<MySqlDatabase>();

        public Session(string token = "")
        {
            Token = token;
            if (!string.IsNullOrEmpty(Token)) Bot = new TelegramBotClient(Token);
            string connectionString = "SERVER=WIN-344VU98D3RU\\SQLEXPRESS;DATABASE=Instagram;Trusted_Connection=True";
#if DEBUG
            connectionString = "SERVER=DESKTOP-VBFBI8T;DATABASE=Instagram;Trusted_Connection=True";
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
            Menu.Session = MenuBloggers.Session = Refresher.Session = this;
            Refresher.Refresh();
            List<long> list;
            MySql.GetBlockList(out list);
            BlockedList = list;
            PrivilegeList = new List<string>();
        }

        public Connection Connection { get; }
        public Multithreading Multithreading { get; set; }
        public Languages Language { get; set; } = new Languages();
        public List<long> BlockedList { get; set; }
        public List<string> PrivilegeList { get; set; }

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