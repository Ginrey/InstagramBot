﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace InstagramBot.IO
{
   public static class Menu
   {
       public static Session Session { get; set; }

       static List<long> adminList = new List<long>
       {
           243797329,
           242885678
       };

       public static void ShowMyUrl(ActionBot user)
       {
           Session.Bot?.SendTextMessageAsync(user.TelegramId,
               string.Format(Session.Language.Get(user.Language, "oau_signing_up")));
           Session.Bot?.SendTextMessageAsync(user.TelegramId,
               "t.me/scs110100bot?start=" + user.Account.Referal);
       }

       public static void ShowMyReferals(ActionBot user)
       {
           int count;
           Session.MySql.GetCountFollows(user.Account.Uid, out count);
           Session.Bot?.SendTextMessageAsync(user.TelegramId,
               string.Format(Session.Language.Get(user.Language, "oau_number_registred_users"), count, count < 2 ? count : 2));
       }

       public static void ShowStatus(ActionBot user)
       {
           string status = Session.MySql.IsLicenseStart(user.Account.Uid)
               ? Session.Language.Get(user.Language, "status_growth")
               : Session.Language.Get(user.Language, "status_start");
           Session.Bot?.SendTextMessageAsync(user.TelegramId,
               string.Format(Session.Language.Get(user.Language, "oau_your_status"), status));
       }

       public static void ShowStruct(ActionBot user, bool isStruct)
       {
           int count;
           var list = Session.MySql.GetStructInfo(user.Account.Uid);
           int lineCount = 0, stateRost = 0, stateStart = 0, blocked = 0;
           string myreferals = Session.Language.Get(user.Language, "oau_invite_list");
           foreach (var i in list)
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
                   string.Format(Session.Language.Get(user.Language, "oau_your_structure"), list.Count, stateRost,
                       stateStart, blocked));
           }
           else
               Session.Bot?.SendTextMessageAsync(user.TelegramId, myreferals);
       }

        public static void ShowPromo(ActionBot user)
        {
            Session.Bot?.SendTextMessageAsync(user.TelegramId, Session.Language.Get(user.Language, "promo_1"));
            FileToSend video = new FileToSend("BAADAgADBAADUQ2IDnXWKI0cjPv7Ag");
            Session.Bot?.SendVideoAsync(user.TelegramId, video);
            Session.Bot?.SendTextMessageAsync(user.TelegramId, Session.Language.Get(user.Language, "promo_2"));
        }

        public static void ShowMyRedList(ActionBot user)
       {
           List<string> redlist;
           Session.MySql.GetRedList(user.Account.Uid, out redlist);
           string text = redlist.Aggregate("", (current, t) => current + t + "\n");
           Session.Bot?.SendTextMessageAsync(user.TelegramId,
               string.Format(Session.Language.Get(user.Language, "oau_redlist"), text));
       }

       public static void ShowMainMenu(ActionBot user, string text = Config.MenuList.CompleteEnter)
       {
           text = Session.Language.Get(user.Language, text);
           ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(new[]
           {
               new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.PrivateOffice))},
               new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.ReferalUrl))},
               new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.Order))},
               new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.WhereReferals))},
               new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.PromoMaterials))}
           });
           Session.Bot?.SendTextMessageAsync(user.TelegramId, text, replyMarkup: keyboard);
       }

       public static void ShowLK(ActionBot user)
       {
           List<KeyboardButton[]> listMenu = new List<KeyboardButton[]>();
           listMenu.Add(new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.MyReferals))});
           listMenu.Add(new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.MyListUsers))});
           listMenu.Add(new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.MyPrivateFollows))});
           listMenu.Add(new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.Status))});
           listMenu.Add(new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.Struct))});
           if (adminList.Contains(user.TelegramId))
           listMenu.Add(new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.HowMachUsers))});
           listMenu.Add(new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.CheckUsersOnInstagram))});
           listMenu.Add(new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.ChangeLanguage))});
           listMenu.Add(new[] {new KeyboardButton(Session.Language.Get(user.Language, Config.MenuList.BackToMenu))});

           ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(listMenu.ToArray());
           Session.Bot?.SendTextMessageAsync(user.TelegramId, Session.Language.Get(user.Language, Config.MenuList.LK),
               replyMarkup: keyboard);
       }

        public static void ShowCountUsers(ActionBot user)
        {
            if (!adminList.Contains(user.TelegramId)) return;
            var list = Session.MySql.GetTelegrams();
            Session.Bot?.SendTextMessageAsync(user.TelegramId,
                string.Format(Session.Language.Get(user.Language, "oau_all_users"), list.Count));
        }
   }
}