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
            try
            {
                var keyboard = new InlineKeyboardMarkup(new[]
                {
                    new InlineKeyboardButton("Да", "/Yes"),
                    new InlineKeyboardButton("Нет", "/No")
                });
                await
                    Session.Bot?.SendTextMessageAsync(user.TelegramId,
                        "Это ваш аккаунт?\nhttp://instagram.com/" + user.Account.Referal, replyMarkup: keyboard);
            }catch(Exception ex) { }
        }

        public async void Deserialize(ActionBot user, StateEventArgs e)
        {
            try {
                if (e.Message.Text.StartsWith("/Yes"))
                {
                    if (!Session.MySql.IsPresentLicense(user.Account.Uid))
                    {
                        if (user.Account.Following - 70 < 0 || user.Account.Posts - 30 < 0)
                        {

                            await Session.Bot?.SendTextMessageAsync(user.TelegramId,
                                "Чтобы пользоваться сервисом вам необходимо еще " +
                                $"{(70 - user.Account.Following < 0 ? 0 : 70 - user.Account.Following)} подписчиков и {(30 - user.Account.Posts < 0 ? 0 : 30 - user.Account.Posts)} публикаций");
                            Console.WriteLine("[{0}] {1} Не прошел по критериям", DateTime.Now, user.Account.Referal);
                            System.IO.File.AppendAllText(@"notRegistering.txt",
                                $"{user.TelegramId}-{user.Account.Referal}\n");
                        }
                        else
                        {
                            Console.WriteLine("[{0}] {1} Подтвердил свой аккаунт", DateTime.Now, user.Account.Referal);
                            user.SetState(States.WaitUrlFrom);
                        }
                    }
                    else
                        await Session.Bot?.SendTextMessageAsync(user.TelegramId, "Данный аккаунт уже зарегистрирован");
                }
                else
                {
                    user.SetState(States.Registering);
                }
            }
            catch (Exception ex) { }
        }
    }
}
