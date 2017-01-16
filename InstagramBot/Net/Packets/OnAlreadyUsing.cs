using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace InstagramBot.Net.Packets
{
    class OnAlreadyUsing : IActionPacket
    {
        public Session Session { get; set; }
       
        public void Serialize(ActionBot user, StateEventArgs e)
        {
            try { 
            string referal;
            if (user.Account != null) return;
            if (Session.MySql.GetReferalByTelegramId(user.TelegramId, out referal))
            {
                user.Account = Session.WebInstagram.GetAccount(referal);

                if (user.Account == null)
                        Session.Bot?.SendTextMessageAsync(user.TelegramId,
                                string.Format(Session.Language.Get(user.Language, "oau_login_failed")));
                    else
                {
                    Console.WriteLine("[{0}] {1} Enter to account", DateTime.Now, user.Account.Referal);
                    ShowMainMenu(user);
                }
                }
            }
            catch (Exception ex) { }
        }

        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(e.Message.Text) || user.Account == null) return;
                string command = Session.Language.GetReverse(user.Language, e.Message.Text);
                switch (command)
                {
                    case Config.MenuList.ReferalUrl:
                        Session.Bot?.SendTextMessageAsync(user.TelegramId,
                            string.Format(Session.Language.Get(user.Language, "oau_signing_up")));
                         Session.Bot?.SendTextMessageAsync(user.TelegramId,
                            "t.me/scs110100bot?start=" + user.Account.Referal);
                        break;
                    case Config.MenuList.MyReferals:
                        int count;
                        Session.MySql.GetCountFollows(user.Account.Uid, out count);
                        Session.Bot?.SendTextMessageAsync(user.TelegramId,
                           string.Format(Session.Language.Get(user.Language, Config.MenuList.ReferalUrl)));
                        Session.Bot?.SendTextMessageAsync(user.TelegramId, 
                            string.Format(Session.Language.Get(user.Language, "oau_signing_up"), count, count < 2 ? count : 2));
                        break;
                    case Config.MenuList.MyListUsers:
                        ShowStruct(user, false);
                        break;
                    case Config.MenuList.MyPrivateFollows:
                        ShowMyRedList(user);
                        break;

                    case Config.MenuList.Order:
                         Session.Bot?.SendTextMessageAsync(user.TelegramId, Config.Order(user.Language));
                        break;
                    case Config.MenuList.PrivateOffice:
                        ShowLK(user);
                        break;
                    case Config.MenuList.BackToMenu:
                        ShowMainMenu(user, Config.MenuList.MainMenu);
                        break;
                    case Config.MenuList.Status:
                        ShowStatus(user);
                        break;
                    case Config.MenuList.Struct:
                        ShowStruct(user, true);
                        break;
                    case Config.MenuList.CheckUsersOnInstagram:
                        user.State = States.OnFindClients;
                        break;
                    case Config.MenuList.PromoMaterials:
                         Session.Bot?.SendTextMessageAsync(user.TelegramId, Session.Language.Get(user.Language, "promo_1"));
                        FileToSend video = new FileToSend("BAADAgADcAADLiR6DtKW-1XLqIZ_Ag");
                         Session.Bot?.SendVideoAsync(user.TelegramId, video);
                         Session.Bot?.SendTextMessageAsync(user.TelegramId, Session.Language.Get(user.Language, "promo_2"));
                        break;
                    case Config.MenuList.WhereReferals:
                         Session.Bot?.SendTextMessageAsync(user.TelegramId, "https://youtu.be/nWdoU7e1OxU");
                        break;

                    case Config.MenuList.HowMachUsers:
                        if (!adminList.Contains(user.TelegramId)) return;
                        var list = Session.MySql.GetTelegrams();
                        Session.Bot?.SendTextMessageAsync(user.TelegramId,
                            string.Format(Session.Language.Get(user.Language, "oau_all_users"), list.Count));

                        break;
                    case "1111qqqqaaal344asdw":
                        var list3 = Session.MySql.GetTelegrams();
                        foreach(var v in list3)
                             Session.Bot?.SendTextMessageAsync(v, @"ИТОГИ 1 ДНЯ РАБОТЫ scs110100bot
Обращение к пользователям автора проекта Ильи Столбова(100LBOV)
https://youtu.be/Zv70LJ8nLFM");
                        break;
                    default:

                        break;
                }
            }
            catch { }
        }

        List<long> adminList = new List<long>
        {
            243797329,
            242885678
        };
        void ShowStatus(ActionBot user)
        {
            string status = Session.MySql.IsLicenseStart(user.Account.Uid) ? 
                Session.Language.Get(user.Language, "status_growth") : 
                Session.Language.Get(user.Language, "status_start");
            Session.Bot?.SendTextMessageAsync(user.TelegramId, 
                string.Format(Session.Language.Get(user.Language, "oau_your_status"), status));
        }

        void ShowStruct(ActionBot user, bool isStruct)
        {
            int count;
            var list = Session.MySql.GetStructInfo(user.Account.Uid);
            int lineCount = 0, stateRost = 0, stateStart = 0, blocked = 0;
            string myreferals = Session.Language.Get(user.Language, "oau_invite_list");
            foreach(var i in list)
            {
                if (i.States == States.Blocked) blocked++;
                if (i.Status) stateRost++;
                else stateStart++;
                myreferals += i.Referal + "\n";
            }
            if (isStruct)
            {
                Session.MySql.GetCountFollows(user.Account.Uid, out count);
               Session.Bot?.SendTextMessageAsync(user.TelegramId,
               string.Format(Session.Language.Get(user.Language, "oau_your_structure"), list.Count, stateRost, stateStart, blocked));
            }
            else
                Session.Bot?.SendTextMessageAsync(user.TelegramId, myreferals);
        }
        void ShowMyRedList(ActionBot user)
        {
            List<string> redlist;
            Session.MySql.GetRedList(user.Account.Uid, out redlist);
            string text = redlist.Aggregate("", (current, t) => current + t + "\n");
            Session.Bot?.SendTextMessageAsync(user.TelegramId,
                string.Format(Session.Language.Get(user.Language, "oau_redlist"), text));
        }

        void ShowMainMenu(ActionBot user, string text = Config.MenuList.CompleteEnter)
        {
            ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(new[]
               {
                    new[] {new KeyboardButton(Config.MenuList.PrivateOffice) },
                    new[] {new KeyboardButton(Config.MenuList.ReferalUrl) },
                    new[] {new KeyboardButton(Config.MenuList.Order) },
                      new[] {new KeyboardButton(Config.MenuList.WhereReferals) },
                 new[] {new KeyboardButton(Config.MenuList.PromoMaterials) }
        });
            Session.Bot?.SendTextMessageAsync(user.TelegramId, text, replyMarkup: keyboard);
        }
        void ShowLK(ActionBot user)
        {
            ReplyKeyboardMarkup keyboard = adminList.Contains(user.TelegramId)? new ReplyKeyboardMarkup(new[]
               {
                    new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.MyReferals)) },
                    new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.MyListUsers)) },
                    new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.MyPrivateFollows)) },
                    new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.Status)) },
                    new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.Struct)) },
                    new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.HowMachUsers)) },
                    new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.CheckUsersOnInstagram)) },
                    new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.BackToMenu)) },
                }) :
                new ReplyKeyboardMarkup(new[]
               {
                      new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.MyReferals)) },
                    new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.MyListUsers)) },
                    new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.MyPrivateFollows)) },
                    new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.Status)) },
                    new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.Struct)) },
                    new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.CheckUsersOnInstagram)) },
                    new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.BackToMenu)) },
                });
            Session.Bot?.SendTextMessageAsync(user.TelegramId, Session.Language.Get(user.Language, Config.MenuList.LK), replyMarkup: keyboard);
           
        }
    }
}
