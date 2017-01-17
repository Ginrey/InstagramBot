using System;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;
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
                    string.Format(Session.Language.Get(user.Language, "owu_it_is_you"), user.Account.Referal), replyMarkup : keyboard);
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
                    if (!Session.MySql.IsPresentLicense(user.Account.Uid))
                    {
                        if (user.Account.Following - 70 < 0 || user.Account.Posts - 30 < 0)
                        {
                            Session.Bot?.SendTextMessageAsync(user.TelegramId,
                                string.Format(Session.Language.Get(user.Language, "owu_to_use"),
                                    (70 - user.Account.Following < 0 ? 0 : 70 - user.Account.Following),
                                    (30 - user.Account.Posts < 0 ? 0 : 30 - user.Account.Posts)));
                            Console.WriteLine("[{0}] {1} Don't have criteries", DateTime.Now, user.Account.Referal);
                            System.IO.File.AppendAllText(@"notRegistering.txt",
                                $"{user.TelegramId}-{user.Account.Referal}\n");
                        }
                        else
                        {
                            Console.WriteLine("[{0}] {1} Accept account", DateTime.Now, user.Account.Referal);
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
            }
        }
    }
}
