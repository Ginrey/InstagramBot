#region

using System;
using System.Collections.Generic;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;
using InstagramBot.IO;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using File = System.IO.File;

#endregion

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
            Session.Bot?.SendTextMessageAsync(user.TelegramId,
                Session.Language.Get(user.Language, Config.MenuList.MultiClients),
                replyMarkup: keyboard);
        }

        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(e.Message.Text) || (user.Account == null)) return;
                if (Change(user, e.Message.Text)) return;

                string command = Session.Language.GetReverse(e.Message.Text);
                switch (command)
                {
                    case Config.MenuMultiClients.Current:
                        Session.Bot?.SendTextMessageAsync(user.TelegramId,
                            string.Format(Session.Language.Get(user.Language, "omc_current"), user.Account.Info.Url));
                        break;
                    case Config.MenuMultiClients.Add:
                        Session.Bot?.SendTextMessageAsync(user.TelegramId,
                            "Для выхода в главное меню отправьте команду /start");
                        user.AdditionInfo.IsAlreadyUses = true;
                        user.State = States.Registering;
                        Console.WriteLine("[{0}] {1} Starting multi register", DateTime.Now, user.Account.Info.Url);
                        break;
                    case Config.MenuMultiClients.Change:
                        List<MiniInfo> info;
                        Session.MySql.GetIdByTelegramId(user.TelegramId, out info);
                        var keyboard = new InlineKeyboardMarkup(InlineKeyboardMarkupMaker(info));
                        Session.Bot?.SendTextMessageAsync(user.TelegramId, "Список", replyMarkup: keyboard);
                        break;
                    case Config.MenuMultiClients.EditNick:
                        File.AppendAllText("EditNick.txt", user.Account.Info.Id + "\r\n");
                        Session.Bot?.SendTextMessageAsync(user.TelegramId,
                            "Запрос будет обработан в течение 24 часов. Спасибо");
                        break;

                    case Config.MenuList.PrivateOffice:
                    case Config.MenuList.BackToMenu:
                        user.State = States.OnAlreadyUsing;
                        user.NextStep(e.Message);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Session.Bot?.SendTextMessageAsync(user.TelegramId, "MultiClient Error");
                LOG.Add("OMA", ex);
            }
        }

        bool Change(ActionBot user, string cmd)
        {
            if (!cmd.StartsWith("/Select")) return false;
            string[] lines = cmd.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length != 2) return false;
            MiniInfo info;
            Session.MySql.GetReferal(lines[1], out info);
            if (info.Id == -1)
            {
                throw new Exception();
            }
            user.Account.Info = info;
            user.Account.IsVip = Bloggers.Contains(user.Account.Info.Id);
            user.State = States.OnAlreadyUsing;
            Menu.ShowMainMenu(user);
            return true;
        }

        InlineKeyboardButton[][] InlineKeyboardMarkupMaker(List<MiniInfo> list)
        {
            InlineKeyboardButton[][] iks = new InlineKeyboardButton[list.Count][];
            for (int i = 0; i < list.Count; i++)
            {
                iks[i] = new[]
                {
                    new InlineKeyboardButton(list[i].Url, "/Select " + list[i].Url)
                };
            }
            return iks;
        }
    }
}