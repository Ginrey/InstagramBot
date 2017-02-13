﻿using System;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;
using InstagramBot.IO;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace InstagramBot.Net.Packets
{
    public class OnWaitUrl : IActionPacket
    {
        public Session Session { get; set; }

        public void Serialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                var keyboard = new InlineKeyboardMarkup(new[]
                {
                    new InlineKeyboardButton(Session.Language.Get(user.Language, "owu_yes"), "/Yes"),
                    new InlineKeyboardButton(Session.Language.Get(user.Language, "owu_no"), "/No")
                });
                Session.Bot?.SendTextMessageAsync(user.TelegramId,
                    string.Format(Session.Language.Get(user.Language, "owu_it_is_you"), user.Account.URL), replyMarkup : keyboard);
            }
            catch (Exception ex)
            {
            }
        }

        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                if (e.Message.Text.StartsWith("/Yes"))
                {
                    if (!Session.MySql.IsPresentInstagram(user.Account.Id))
                    {
                        if (user.Account.URL == "andrey.v2" || Session.PrivilegeList.Contains(user.Account.URL.ToLower()))
                        {
                            Console.WriteLine("[{0}] {1} Accept account", DateTime.Now, user.Account.URL);
                            user.Account.IsVip = true;
                            user.State = States.WaitUrlFrom;
                            return;
                        }
                        if (user.Account.Following - 70 < 0 || user.Account.Posts - 30 < 0)
                        {
                            Session.Bot?.SendTextMessageAsync(user.TelegramId,
                                string.Format(Session.Language.Get(user.Language, "owu_to_use"),
                                    (70 - user.Account.Following < 0 ? 0 : 70 - user.Account.Following),
                                    (30 - user.Account.Posts < 0 ? 0 : 30 - user.Account.Posts)));
                            Console.WriteLine("[{0}] {1} Don't have criteries", DateTime.Now, user.Account.URL);
                            System.IO.File.AppendAllText(@"notRegistering.txt",
                                $"{user.TelegramId}-{user.Account.URL}\n");
                        }
                        else
                        if(user.Account.IsPrivate)
                        {
                            Session.Bot?.SendTextMessageAsync(user.TelegramId, Session.Language.Get(user.Language, "ows_nodes_null"));
                        }
                        else
                        {
                            Console.WriteLine("[{0}] {1} Accept account", DateTime.Now, user.Account.URL);
                            user.State = States.WaitUrlFrom;
                        }
                    }
                    else
                        Session.Bot?.SendTextMessageAsync(user.TelegramId,
                            Session.Language.Get(user.Language, "owu_allready_registred"));
                }
                else
                {
                    user.State = States.Registering;
                }
            }
            catch (Exception ex)
            {
                LOG.Add("OWU", ex.Message);
            }
        }
    }
}
