using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace InstagramBot.Net.Packets
{
    class OnAlreadyUsing : ActionPacket
    {
        public Session Session { get; set; }
       
        public void Serialize(ActionBot user, StateEventArgs e)
        {
            string referal;
            if (user.Account != null) return;
            if (Session.MySql.GetReferalByTelegramId(user.TelegramID, out referal))
            {
                user.Account = Session.WebInstagram.GetAccount(referal);

                if (user.Account == null)
                    Session.Bot?.SendTextMessageAsync(user.TelegramID, "Не удалось войти, повторите попытку");
                else
                {
                    Console.WriteLine("[{0}] {1} Заходит на аккаунт", DateTime.Now, user.Account.Referal);
                    ShowMainMenu(user);
                }
            }
        }

        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Message.Text) || user.Account == null) return;
            switch (e.Message.Text)
            {
                case Config.MenuList.ReferalUrl:
                case "/get_referal":
                    Session.Bot?.SendTextMessageAsync(user.TelegramID,
                        "Реферальная ссылка:\n" + "https://t.me/scs110100bot?start=" +
                        user.Account.Referal+ "\nТакже, при регистрации, реферал может использовать ваш ник в Instagram");
                    break;
                case Config.MenuList.MyReferals:
                case "/count_follows":
                    int count;
                    Session.MySql.GetCountFollows(user.Account.Uid, out count);
                    Session.Bot?.SendTextMessageAsync(user.TelegramID, "Количество людей зарегистрированных по вашей ссылке: " + count);
                    break;
                case Config.MenuList.MyPrivateFollows:
                case "/need_follows":
                    ShowMyRedList(user);
                    break;
                case Config.MenuList.Order:
                case "/order":
                    Session.Bot?.SendTextMessageAsync(user.TelegramID, Config.Order);
                    break;
                case Config.MenuList.PrivateOffice:
                    ShowLK(user);
                    break;
                case Config.MenuList.BackToMenu:
                    ShowMainMenu(user, "⬇ Главное меню ⬇");
                    break;
                case Config.MenuList.Status:
                    ShowStatus(user);
                    break;
                case Config.MenuList.Struct:
                    ShowStruct(user);
                    break;
                case Config.MenuList.CheckUsersOnInstagram:
                    user.State = States.OnFindClients;
                    break;
                case Config.MenuList.PromoMaterials:
                    Session.Bot?.SendTextMessageAsync(user.TelegramID,Config.TextUpVideo);
                    Session.Bot?.SendVideoAsync(user.TelegramID, "BAADAgADIQADLiR6DtDX7_XxczQ5Ag");
                    Session.Bot?.SendTextMessageAsync(user.TelegramID, Config.TextDownVideo);
                    break;
                case Config.MenuList.WhereReferals:
                    Session.Bot?.SendVideoAsync(user.TelegramID, "BAADAgADLwADLiR6DhSg_EdEBaRkAg");
                    break;

                case Config.MenuList.HowMachUsers:
                    if (!adminList.Contains(user.TelegramID)) return;
                    var list = Session.MySql.GetTelegrams();
                        Session.Bot?.SendTextMessageAsync(user.TelegramID, "Всего пользователей: "+ list.Count);
                    break;
                default:

                    break;
            }
        }

        List<long> adminList = new List<long>
        {
            243797329,
            242885678
        };
        void ShowStatus(ActionBot user)
        {
            string status = Session.MySql.IsLicenseStart(user.Account.Uid) ? "РОСТ" : "СТАРТ";
            Session.Bot?.SendTextMessageAsync(user.TelegramID, " Ваш статус: "+status);
        }

        void ShowStruct(ActionBot user)
        {
            int count;
            var list = Session.MySql.GetStructInfo(user.Account.Uid);
            int lineCount = 0, stateRost = 0, stateStart = 0, blocked = 0;
            foreach(var i in list)
            {
                if (i.States == States.Blocked) blocked++;
                if (i.Status) stateRost++;
                else stateStart++;
            }
            Session.MySql.GetCountFollows(user.Account.Uid, out count);
            Session.Bot?.SendTextMessageAsync(user.TelegramID, 
                $"Ваша структура: \nобщее колличество рефералов = {+list.Count} \n" +
                $"из них: \n Линия РОСТа = {stateRost}\n в статусе СТАРТ = {stateStart} \n" +
                $" заблокированных = {blocked}");
        }
        void ShowMyRedList(ActionBot user)
        {
            List<string> redlist;
            Session.MySql.GetRedList(user.Account.Uid, out redlist);
            string text = redlist.Aggregate("", (current, t) => current + (t + "\n"));
            Session.Bot?.SendTextMessageAsync(user.TelegramID,
                " REDLIST(мои личные подписки):\n" + text);
        }

        void ShowMainMenu(ActionBot user, string text= "Вход успешно выполнен")
        {
            ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(new[]
               {
                    new[] {new KeyboardButton(Config.MenuList.PrivateOffice) },
                    new[] {new KeyboardButton(Config.MenuList.ReferalUrl) },
                    new[] {new KeyboardButton(Config.MenuList.Order) },
                      new[] {new KeyboardButton(Config.MenuList.WhereReferals) },
                 new[] {new KeyboardButton(Config.MenuList.PromoMaterials) }
        });
            Session.Bot?.SendTextMessageAsync(user.TelegramID, text, replyMarkup: keyboard);
        }
        void ShowLK(ActionBot user)
        {
            ReplyKeyboardMarkup keyboard = adminList.Contains(user.TelegramID)? new ReplyKeyboardMarkup(new[]
               {
                    new[] {new KeyboardButton(Config.MenuList.MyReferals) },
                    new[] {new KeyboardButton(Config.MenuList.MyPrivateFollows) },
                    new[] {new KeyboardButton(Config.MenuList.Status) },
                    new[] {new KeyboardButton(Config.MenuList.Struct) },
                    new[] {new KeyboardButton(Config.MenuList.HowMachUsers) },
                     new[] {new KeyboardButton(Config.MenuList.CheckUsersOnInstagram) },
                    
                    new[] {new KeyboardButton(Config.MenuList.BackToMenu) },
                }) :
                new ReplyKeyboardMarkup(new[]
               {
                    new[] {new KeyboardButton(Config.MenuList.MyReferals) },
                    new[] {new KeyboardButton(Config.MenuList.MyPrivateFollows) },
                    new[] {new KeyboardButton(Config.MenuList.Status) },
                    new[] {new KeyboardButton(Config.MenuList.Struct) },
                     new[] {new KeyboardButton(Config.MenuList.CheckUsersOnInstagram) },
                    new[] {new KeyboardButton(Config.MenuList.BackToMenu) },
                });
            Session.Bot?.SendTextMessageAsync(user.TelegramID, "⬇ Личный кабинет ⬇", replyMarkup: keyboard);
           
        }
    }
}
