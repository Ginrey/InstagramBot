using System;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace InstagramBot.Net.Packets
{
   public class OnFirstStep : IActionPacket
    {
        public Session Session { get; set; }
        public async void Serialize(ActionBot user, StateEventArgs e)
        {
            try
            {
              await  Session.Bot?.SendTextMessageAsync(user.TelegramId,
                   string.Format(Session.Language.Get(user.Language, "or_welcome"), "https://youtu.be/nOCia6abu3E"));
             //   Session.Bot?.SendVideoAsync(user.TelegramId, "BAADAgADHwADNf4AAUiMH8oazAp5qQI");

                var keyboard = new InlineKeyboardMarkup(new[]
                {
                    new InlineKeyboardButton(Session.Language.Get(user.Language,"ofs_next"), "/Next")
                });
             await   Session.Bot?.SendTextMessageAsync(user.TelegramId, "Нажмите после просмотра",
                    replyMarkup: keyboard);
            }
            catch(Exception ex)
            { }
        }


        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                if (e.Message.Text.StartsWith("/Next"))
                {
                    user.State = States.SecondStep;
                }
            }catch { }
        }
    }
}
