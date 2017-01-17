using System;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;
using InstagramBot.IO;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace InstagramBot.Net.Packets
{
   public class OnChangeLanguage : IActionPacket
    {
        public Session Session { get; set; }
        public void Serialize(ActionBot user, StateEventArgs e)
        {
           var keyboard = new InlineKeyboardMarkup(new[]
               {
                    new InlineKeyboardButton("Русский 🇷🇺", "/Russian"),
                    new InlineKeyboardButton("English 🇺🇸", "/English")
                });
            Session.Bot?.SendTextMessageAsync(user.TelegramId, Session.Language.Get(user.Language, "ocl_selectlang"),
                replyMarkup: keyboard);
        }

        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            if (!e.Message.Text.StartsWith("/")) return;
            if (e.Message.Text.Contains("/Russian"))
            {
                user.Language = Language.Russian;
                Session.MySql.UpdateLanguage(user.Account.Uid, Language.Russian);
            }
            if (e.Message.Text.Contains("/English"))
            {
                user.Language = Language.English;
                Session.MySql.UpdateLanguage(user.Account.Uid, Language.English);
            }
            user.State = States.OnAlreadyUsing;
            Menu.ShowMainMenu(user, "--------------");
        }
    }
}
