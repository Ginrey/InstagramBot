using System;
using System.Collections.Generic;
using System.Linq;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace InstagramBot.IO
{
   public static class MenuBloggers
    {
        public static Session Session { get; set; }
        static DateTime yesterday = DateTime.Now.Date.AddDays(-1);
        static DateTime today = DateTime.Now.Date;
        public static void ShowMembersCount(ActionBot user)
        {
            if (!user.Account.IsVip) return;
                int count;
            Session.MySql.GetCountCorruptionList(out count);
            List<Privilege> nowlist;
            Session.MySql.GetQuotaFromCorruption(out nowlist);
            double sum = nowlist.Sum(item => item.Coefficient);
            int countnew;
            Session.MySql.GetCountCorruptionAddedWithDate(today, out countnew);
            string sendText = "Участники револьвера\n";

            sendText += $"Количество: {count} /List_members\n";
            sendText += $"Охват аудитории: {sum}\n";
            sendText += $"Добавились {today.ToShortDateString()}: {countnew} /List_new\n";
           
            Session.Bot?.SendTextMessageAsync(user.TelegramId, sendText);
        }

        public static void ShowStatistics(ActionBot user)
        {
            if (!user.Account.IsVip) return;
            List<Privilege> list;
            Session.MySql.GetCorruptionList(out list);
            double summcoeff=0;
            Privilege top = new Privilege();
            foreach(var item in list)
            {
                if (item.Coefficient > top.Coefficient)
                    top = item;
                summcoeff += item.Coefficient;
            }
            int countRegToday, countRegAll;
            Session.MySql.GetCountFromCorruptionWithDate(today, out countRegToday);
            Session.MySql.GetCountFromCorruption(out countRegAll);

            List<Privilege> nowlist;
            Session.MySql.GetQuotaFromCorruption(out nowlist);
            double sum = nowlist.Sum(item => item.Coefficient);

            int countnew;
            Session.MySql.GetCountCorruptionAddedWithDate(today, out countnew);

            string sendText = "Участники револьвера\n";
            sendText += $"Количество: {list.Count} /List_members\n";
            sendText += $"Охват аудитории: {sum}\n";
            sendText += $"Добавились {today.ToShortDateString()}: {countnew} /List_new\n" +
                        $"-------------------\n";
            sendText += $"За {today.ToShortDateString()} из револьвера зарегистрировалось {countRegToday}\n";
            sendText += $"Всего из револьвера зарегистрировалось {countRegAll}\n";
            sendText += "\nДополнительно\n";
            sendText += $"Итого: {today.ToShortDateString()} /List_overall\n";
            sendText += $"Лидер квота: {top.URL} ({top.Coefficient}) - {100 / summcoeff * top.Coefficient:0.0000}%\n";
          
            Session.Bot?.SendTextMessageAsync(user.TelegramId, sendText);
        }

        public static void ShowRegistering(ActionBot user)
        {
            if (!user.Account.IsVip) return;
            int countRegToday, countMyAll, countTodayAll, countAll;
            Session.MySql.GetCountWithDate(user.Account.Id, today, out countRegToday);
            Session.MySql.GetCountFromMyUrl(user.Account.Id, out countMyAll);
            Session.MySql.GetCountFromCorruptionWithDate(today, out countTodayAll);
            Session.MySql.GetCountFromCorruption(out countAll);
            string sendText = "Количество собственных регистраций\n";
            sendText += $"Собственных {today.ToShortDateString()}: {countRegToday}\n" +
                        $"Всего зарегистрировано за весь период: {countMyAll}\n\n";
            sendText += "Количество регистраций всего револьвера\n";
            sendText += $"{today.ToShortDateString()}: {countTodayAll}\n" +
                        $"За весь период {countAll}";
            Session.Bot?.SendTextMessageAsync(user.TelegramId, sendText);
        }
        public static void ShowChat(ActionBot user)
        {
            if (!user.Account.IsVip) return;
            InlineKeyboardButton[][] iks = new InlineKeyboardButton[1][];
            iks[0] = new[]
            {
                new InlineKeyboardButton("Перейти", "/Url")
                {
                    Url = "https://t.me/joinchat/AAAAAEIqhbjjDvIou8nhlw"
                }
            };
        
            Session.Bot?.SendTextMessageAsync(user.TelegramId, "Для перехода в чат нажмите", replyMarkup: new InlineKeyboardMarkup(iks));
        }
        public static void ShowChannel(ActionBot user)
        {
            if (!user.Account.IsVip) return;
            InlineKeyboardButton[][] iks = new InlineKeyboardButton[1][];
            iks[0] = new[]
            {
                new InlineKeyboardButton("Перейти", "/Url")
                {
                    Url = "https://t.me/joinchat/AAAAAEKmX0aF-dK_1buOAA"
                }
            };

            Session.Bot?.SendTextMessageAsync(user.TelegramId, "Для перехода в канал нажмите", replyMarkup: new InlineKeyboardMarkup(iks));
        }
        public static void ShowMyInfo(ActionBot user)
        {
            if (!user.Account.IsVip) return;
            int FreePlace = GetCountTree(user.Account.Id);
            int countRegYesterday;

            Session.MySql.GetCountWithDate(user.Account.Id, yesterday, out countRegYesterday);
            double summcoeff = Bloggers.privilegeListCopy.Sum(item => item.Coefficient);

            Privilege info;
            Session.MySql.GetMyCorruptionInfo(user.Account.Id, out info);

            int countRegisterYesterday;
            Session.MySql.GetCountWithDate(user.Account.Id, yesterday, out countRegisterYesterday);

            int countMyAll;
            Session.MySql.GetCountFromMyUrl(user.Account.Id, out countMyAll);
            FreePlace = FreePlace == 0 ? 1 : FreePlace;
            string sendText = "Личная информация:\n";
            sendText += $"Количество свободных мест = {FreePlace}\n";
            sendText += $"Количество зарегистрированных за {yesterday.ToShortDateString()} = {countRegYesterday}\n";
            sendText += $"Количество зарегистрированных за весь период = {countMyAll}\n";
            sendText += "------------------------\n" +
                        "Верхние линии /List_upline\n" +
                        "------------------------\n";
            sendText += $"Суточный лимит трафика =  {80 / summcoeff * info.Coefficient:0.0000}%\n";
            sendText += $"Накопительный лимит трафика = {20 / summcoeff * info.Coefficient:0.0000}%\n";
            sendText += $"ИТОГО на {today.ToShortDateString()} общий лимит трафика = {100 / summcoeff * info.Coefficient:0.0000}%\n\n";
            sendText += $"СЛТ {80 / summcoeff * info.Coefficient:0.0000}% + НЛТ {20 / summcoeff * info.Coefficient:0.0000}% = ОЛТ {100 / summcoeff * info.Coefficient:0.0000}%\n\n" +
                        $"/Statistics";
            Session.Bot?.SendTextMessageAsync(user.TelegramId, sendText);
        }
        public static void ShowListMembers(ActionBot user)
        {
            if (!user.Account.IsVip) return;
            List<Privilege> list;
            Session.MySql.GetQuotaFromCorruption(out list);
            string sendText = "";
            foreach (var item in list)
            {
                int freeplace = GetCountTree(item.ID);
                freeplace = freeplace == 0 ? 1 : freeplace;
                sendText += $"{item.URL}: {item.Coefficient+1} РР - {freeplace} СМ\n";
            }
            Session.Bot?.SendTextMessageAsync(user.TelegramId, sendText);
        }
        public static void ShowListNew(ActionBot user)
        {
            if (!user.Account.IsVip) return;
            List<Privilege> list;
            Session.MySql.GetListCorruptionAddedWithDate(today, out list);
            string sendText = "";
            foreach (var item in list)
                sendText += $"{today.ToShortDateString()} {item.URL} ({item.Coefficient})\n";
            if(string.IsNullOrEmpty(sendText))
                sendText += $"{today.ToShortDateString()} (0)\n";
            Session.Bot?.SendTextMessageAsync(user.TelegramId, sendText);
        }

        public static void ShowListStatistics(ActionBot user)
        {
            if (!user.Account.IsVip) return;

            double summcoeff = Bloggers.privilegeListCopy.Sum(item => item.Coefficient);

            List<BloggerStatistics> list;
            Session.MySql.GetMyStatisticsCorruption(user.Account.Id, out list);
            string sendText = "";
            foreach (var item in list)
            {
                int count;
                Session.MySql.GetCountWithDate(item.ID, item.Date.Date, out count);
                sendText += $"{item.Date.ToShortDateString()} = {count} ак, {100/summcoeff*item.Coefficient:0.0000}%\n";
            }
            if (sendText == "") sendText = "Ваша статистика появится со следующего дня";
            Session.Bot?.SendTextMessageAsync(user.TelegramId, sendText);
        }

        public static void ShowListOverall(ActionBot user)
        {
            if (!user.Account.IsVip) return;
            List<BloggerStatistics> list;
            Session.MySql.GetStatisticsCorruptionWithDate(yesterday, out list);
            double summ = list.Sum(item => item.Coefficient);
            string sendText = "";
            foreach (var item in list)
                sendText += $"{item.Date.ToShortDateString()} {item.URL} {100 / summ * item.Coefficient:0.0000}%\n";
            Session.Bot?.SendTextMessageAsync(user.TelegramId, sendText);
        }
        public static void ShowListUpline(ActionBot user)
        {
            if (!user.Account.IsVip) return;
        
            MiniInfo info = new MiniInfo(user.Account.Id, user.Account.URL);
            List<string> sendText = new List<string>();
            do
            {
                sendText.Add(info.URL);
                Session.MySql.GetTreeInstagram(info.ID, out info);
            } while (info.ID != 1647550018 && info.ID != 442320062);
            sendText.Add("100lbov");
            sendText.Reverse();
            string text = "";
            foreach (string t in sendText)
                text += t+"\n";
            Session.Bot?.SendTextMessageAsync(user.TelegramId, text);
        }

        static int GetCountTree(long id)
        {
            int count = 8;
            MiniInfo info = new MiniInfo(id);
            do
            {
                count--;
                Session.MySql.GetTreeInstagram(info.ID, out info);
            } while (info.ID != 1647550018 && info.ID != 442320062);

            return count;
        }
    }
}
