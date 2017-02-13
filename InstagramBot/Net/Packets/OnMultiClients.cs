using System;
using System.Collections.Generic;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;
using InstagramBot.IO;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace InstagramBot.Net.Packets
{
   public class OnMultiClients : IActionPacket
    {
        public Session Session { get; set; }
        public void Serialize(ActionBot user, StateEventArgs e)
        {
            List<KeyboardButton[]> listMenu = new List<KeyboardButton[]>
            {
                new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuMultiClients.Current))},
                new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuMultiClients.Add))},
                new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuMultiClients.Change))},
                new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuMultiClients.EditNick))},
                new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.PrivateOffice))},
                new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.BackToMenu))}
            };

            ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(listMenu.ToArray());
            Session.Bot?.SendTextMessageAsync(user.TelegramId, Session.Language.Get(user.Language, Config.MenuList.MultiClients),
                replyMarkup: keyboard);
        }

        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(e.Message.Text) || user.Account == null) return;
                string command = Session.Language.GetReverse(e.Message.Text);
                switch (command)
                {
                    case Config.MenuMultiClients.Current:
                        Session.Bot?.SendTextMessageAsync(user.TelegramId, string.Format(Session.Language.Get(user.Language, "omc_current"), user.Account.URL));
                        break;
                    case Config.MenuMultiClients.Add:
                        user.State = States.Registering;
                        break;

                      case  Config.MenuMultiClients.Change:
                        List<MiniInfo> info;
                        Session.MySql.GetIdByTelegramId(user.TelegramId, out info);
                            break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LOG.Add("OMA", ex.Message);
            }
        }
        bool Change(string cmd)
        {
            if (cmd.StartsWith("/Select"))
            {
                string[] lines = cmd.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
                if (lines.Length != 2) return false;

                return true;
            }
                return false;
        }
    }
    
}
