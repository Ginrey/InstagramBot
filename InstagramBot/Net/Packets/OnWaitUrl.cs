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
        public async void Serialize(ActionBot user, StateEventArgs e)
        {
            var keyboard = new InlineKeyboardMarkup(new[]
            {
                new InlineKeyboardButton("Да","/Yes"),
                new InlineKeyboardButton("Нет", "/No")
            });
           await Session.Bot?.SendTextMessageAsync(user.TelegramID, "Это ваш аккаунт?\nhttp://instagram.com/" + user.Account.Referal, replyMarkup : keyboard);
        }
        public async void Deserialize(ActionBot user, StateEventArgs e)
        {
            if (e.Message.Text.StartsWith("/Yes"))
            {
                if (!Session.MySql.IsPresentLicense(user.Account.Uid))
                {
                    if (user.Account.Following - 100 < 0 || user.Account.Posts - 50 < 0)
                    {
                        await Session.Bot?.SendTextMessageAsync(user.TelegramID,
                            "Чтобы пользоваться сервисом вам необходимо еще " +
                            $"{(100 - user.Account.Following < 0 ? 0 : 100 - user.Account.Following)} подписчиков и {(50 - user.Account.Posts < 0 ? 50 : 50 - user.Account.Posts)} публикаций");
                        Console.WriteLine("[{0}] {1} Не прошел по критериям", DateTime.Now, user.Account.Referal);
                        System.IO.File.AppendAllText(@"notRegistering.txt", $"{user.TelegramID}-{user.Account.Referal}\n");
                    }
                    else
                    {
                        Console.WriteLine("[{0}] {1} Подтвердил свой аккаунт", DateTime.Now, user.Account.Referal);
                        user.State++;
                    }
                }
                else
                   await Session.Bot?.SendTextMessageAsync(user.TelegramID, "Данный аккаунт уже зарегистрирован");
            }
            else
            {
                user.State = States.Registering;
            }
        }
    }
}
