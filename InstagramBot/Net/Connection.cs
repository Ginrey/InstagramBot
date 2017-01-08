using System;
using System.Collections.Generic;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace InstagramBot.Net
{
    public class Connection
    {
        public Session Session { get; set; }
        public Dictionary<long, ActionBot> Users = new Dictionary<long, ActionBot>();
        public PacketsRegistry PacketsRegistry { get; set; }
        TelegramBotClient Bot => Session.Bot;
     
        public Connection(Session session)
        {
            Session = session;
            PacketsRegistry = new PacketsRegistry(session);
            Bot.OnMessage += OnMessageReceived;
            Bot.OnCallbackQuery += OnInlineQueryReceived;
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
                    string[] lines = message.Text.Split();
                    string fromreferal = lines.Length == 2 ? lines[1] : "";
                    if (!Users.ContainsKey(message.Chat.Id))
                    {
                        long id = message.Chat.Id; 
                        Users.Add(id, new ActionBot(id, Session, GetLicenseState(id), fromreferal));
                    }
                    return;
                }
                if (!Users.ContainsKey(message.Chat.Id)) return;
                var user = Users[message.Chat.Id];
                user.NextStep(message);
            }
        }

        private void OnInlineQueryReceived(object sender, CallbackQueryEventArgs messageEventArgs)
        {
            var message = messageEventArgs.CallbackQuery.Message;
            message.Text = messageEventArgs.CallbackQuery.Data;
            OnMessageReceived(sender, new MessageEventArgs(message));
        }

        States GetLicenseState(long uid)
        {
            States state;
            Session.MySql.GetLicenseState(uid, out state);
            return state;
        }
    }
}
