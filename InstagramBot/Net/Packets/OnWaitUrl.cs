#region

using System;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;
using InstagramBot.IO;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using File = System.IO.File;

#endregion

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
                    string.Format(Session.Language.Get(user.Language, "owu_it_is_you"), user.Account.Info.Url),
                    replyMarkup: keyboard);
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
                    if (!Session.MySql.IsPresentInstagram(user.Account.Info.Id))
                    {
                        if (user.Account.Info.Url == "andrey.v2" ||
                            Session.PrivilegeList.Contains(user.Account.Info.Url.ToLower()))
                        {
                            Console.WriteLine("[{0}] {1} Accept account", DateTime.Now, user.Account.Info.Url);
                            user.Account.IsVip = true;
                            user.State = States.WaitUrlFrom;
                            return;
                        }
                     /*   if (user.Account.Following - 70 < 0 || user.Account.Posts - 30 < 0)
                        {
                            Session.Bot?.SendTextMessageAsync(user.TelegramId,
                                string.Format(Session.Language.Get(user.Language, "owu_to_use"),
                                    70 - user.Account.Following < 0 ? 0 : 70 - user.Account.Following,
                                    30 - user.Account.Posts < 0 ? 0 : 30 - user.Account.Posts));
                            Console.WriteLine("[{0}] {1} Don't have criteries", DateTime.Now, user.Account.Info.Url);
                            File.AppendAllText(@"notRegistering.txt",
                                $"{user.TelegramId}-{user.Account.Info.Url}\n");
                        }
                        else if (user.Account.IsPrivate)
                        {
                            Session.Bot?.SendTextMessageAsync(user.TelegramId,
                                Session.Language.Get(user.Language, "ows_nodes_null"));
                        }
                        else    */
                        {
                            Console.WriteLine("[{0}] {1} Accept account", DateTime.Now, user.Account.Info.Url);
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
                LOG.Add("OWU", ex);
            }
        }
    }
}