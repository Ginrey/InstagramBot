using System;
using System.Collections.Generic;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace InstagramBot.Net
{
    public class Connection
    {
        public Session Session { get; set; }
        public Dictionary<int, ActionBot> Users = new Dictionary<int, ActionBot>();
        public PacketsRegistry PacketsRegistry { get; set; }
        TelegramBotClient Bot => Session.Bot;

        public Connection(Session session)
        {
            Session = session;
            PacketsRegistry = new PacketsRegistry(session);
            Bot.OnMessage += OnMessageReceived;
        }

        public void Connect()
        {
            if (Bot == null) return;
            var me = Bot.GetMeAsync().Result;
            Console.Title = me.Username;
            Bot.StartReceiving();
        }

        public void Close()
        {
            
            Bot?.StopReceiving();
            Console.Title = "Closed";
        }

        private void OnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            if (message.Type == MessageType.TextMessage)
            {
                if (message.Text.StartsWith("/start"))
                {
                    if (!Users.ContainsKey(message.From.Id))
                    {
                        int id = message.From.Id;
                        Users.Add(id, new ActionBot(id, Session, GetLicenseState(id)));
                    }
                    return;
                }
                if (!Users.ContainsKey(message.From.Id)) return;
                var user = Users[message.From.Id];
                user.Message = message;
                //Todo
                user.State++;

                switch (user.State)
                {
                    case States.Registering:
                        break;
                    case States.WaitUrl:
                        break;
                    case States.WaitSubscribe:
                        break;
                    case States.Done:
                        break;
                    case States.Blocked:
                        break;
                    case States.Null:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        States GetLicenseState(int uid)
        {
            States state;
            Session.MySql.GetLicenseState(uid, out state);
            return state;
        }
    }
}
