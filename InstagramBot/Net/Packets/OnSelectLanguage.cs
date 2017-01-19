using System;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace InstagramBot.Net.Packets
{
    public class OnSelectLanguage : IActionPacket
    {
        public Session Session { get; set; }
        public void Serialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                var keyboard = new InlineKeyboardMarkup(new[]
                {
                    new InlineKeyboardButton("Русский 🇷🇺", "/Russian"),
                    new InlineKeyboardButton("English 🇺🇸", "/English")
                });
                Session.Bot?.SendTextMessageAsync(user.TelegramId, "Выберите язык для использования сервиса\n" +
                                                                   "Select language fot using service\n",
                    replyMarkup: keyboard);
            }
            catch { }
        }
        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            
            if (e.Message.Text.Contains("/Russian"))
           {
               user.Language = IO.Language.Russian;
               user.State = States.FirstStep;
           }
            if (e.Message.Text.Contains("/English"))
            {
                user.Language = IO.Language.English;
                user.State = States.FirstStep;
            }
        }
    }
}
