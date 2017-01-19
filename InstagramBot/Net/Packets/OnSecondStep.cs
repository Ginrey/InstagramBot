using System;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace InstagramBot.Net.Packets
{
   public class OnSecondStep:IActionPacket
    {
        public Session Session { get; set; }
        public async void Serialize(ActionBot user, StateEventArgs e)
        {
            try
            {
               await  Session.Bot?.SendTextMessageAsync(user.TelegramId, "Как работает бот \n https://youtu.be/76b6r9spW1k");
               
                //  await  Session.Bot?.SendVideoAsync(user.TelegramId, "BAADAgADIQADNf4AAUgNJ_9g8ZiqrwI");

                var keyboard = new InlineKeyboardMarkup(new[]
                {
                    new InlineKeyboardButton(Session.Language.Get(user.Language,"ofs_next"), "/Next")
                });
              await  Session.Bot?.SendTextMessageAsync(user.TelegramId, "Нажмите после просмотра",
                      replyMarkup: keyboard);
            }
            catch { }
        }

        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                if (e.Message.Text.StartsWith("/Next"))
                {
                    user.State = States.Registering;
                }
            }
            catch { }
        }
    }
}
