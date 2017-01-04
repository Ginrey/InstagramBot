using InstagramBot.Data;
using InstagramBot.Data.Accounts;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace InstagramBot.Net.Packets
{
    public class OnWaitUrl : ActionPacket
    {
        public Session Session { get; set; }
        public void Serialize(ActionBot user, StateEventArgs e)
        {
            var keyboard = new InlineKeyboardMarkup(new[]
            {
                new InlineKeyboardButton("Да","/Yes"),
                new InlineKeyboardButton("Нет", "/No")
            });
            Session.Bot?.SendTextMessageAsync(user.TelegramID, "Это ваш аккаунт?\n" + user.Account.Name, replyMarkup : keyboard);
        }
        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            if (e.Message.Text.StartsWith("/Yes"))
            {
                if (!Session.MySql.IsPresentLicense(user.Account.Uid))
                    user.State++;
                else
                    Session.Bot?.SendTextMessageAsync(user.TelegramID, "Данный аккаунт уже зарегистрирован");
            }
            else
            {
                user.State = States.Registering;
            }
        }
    }
}
