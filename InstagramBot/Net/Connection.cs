﻿#region

using System;
using System.Collections.Generic;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

#endregion

namespace InstagramBot.Net
{
    public class Connection
    {
        public Dictionary<long, ActionBot> Users = new Dictionary<long, ActionBot>();

        public Connection(Session session)
        {
            Session = session;
            Session.Multithreading.Start();
            PacketsRegistry = new PacketsRegistry(session);
            Bot.OnMessage += OnMessageReceived;
            Bot.OnCallbackQuery += OnInlineQueryReceived;
        }

        public Session Session { get; set; }
        public PacketsRegistry PacketsRegistry { get; set; }
        TelegramBotClient Bot => Session.Bot;

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

        void OnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            if (message.Type == MessageType.TextMessage)
            {
                if (message.Text.StartsWith("/start"))
                {
                    string[] lines = message.Text.Split();
                    string fromreferal = lines.Length == 2 ? lines[1] : "";
                    long id = message.Chat.Id;
                    var state = GetLicenseState(id);
                    Users[id] = new ActionBot(id, Session, fromreferal);
                    Users[id].SetState(state);
                }
                else if (message.Text.StartsWith("/test"))
                {
                    Session.Bot?.SendTextMessageAsync(message.Chat.Id, "CompleteTest");
                }
                else
                {
                    long id = message.Chat.Id;
                    if (Users.ContainsKey(id))
                    {
                        var user = Users[message.Chat.Id];
                        user.NextStep(message);
                    }
                    else
                    {
                        var state = GetLicenseState(id);
                        Users[id] = new ActionBot(id, Session, "");
                        Users[id].SetState(state);
                    }
                    //  user.NextStep(message);
                    return;
                }
            }
            if (message.Type == MessageType.VideoMessage)
            {
                Session.Bot?.SendVideoAsync(message.Chat.Id, message.Video.FileId);
            }
        }

        void OnInlineQueryReceived(object sender, CallbackQueryEventArgs messageEventArgs)
        {
            var message = messageEventArgs.CallbackQuery.Message;
            message.Text = messageEventArgs.CallbackQuery.Data;
            OnMessageReceived(sender, new MessageEventArgs(message));
        }

        States GetLicenseState(long uid)
        {
            return Session.MySql.IsPresentTelegram(uid) ? States.OnAlreadyUsing : States.FirstStep;
        }
    }
}