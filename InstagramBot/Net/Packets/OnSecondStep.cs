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
               await  Session.Bot?.SendTextMessageAsync(user.TelegramId, Session.Language.Get(user.Language, "oss_how_work") +"\n https://youtu.be/76b6r9spW1k");
               
                var keyboard = new InlineKeyboardMarkup(new[]
                {
                    new InlineKeyboardButton(Session.Language.Get(user.Language,"ofs_next"), "/Next")
                });
              await  Session.Bot?.SendTextMessageAsync(user.TelegramId, Session.Language.Get(user.Language, "ofs_press_after"),
                      replyMarkup: keyboard);
            }
            catch (Exception ex)
            { }
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
            catch (Exception ex)
            { }
        }
    }
}
