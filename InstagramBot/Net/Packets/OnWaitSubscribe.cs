using System;
using System.Collections.Generic;
using System.Linq;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace InstagramBot.Net.Packets
{
    public class OnWaitSubscribe : IActionPacket
    {
        public Session Session { get; set; }

        InlineKeyboardButton[][] InlineKeyboardMarkupMaker(Dictionary<string, bool> items)
        {
            InlineKeyboardButton[][] iks = new InlineKeyboardButton[items.Count + 1][];

            for (int i = 0; i < items.Count; i++)
            {
                string value = items.Keys.ElementAt(i);
                iks[i] = new[]
                {
                    new InlineKeyboardButton(value, "/Url")
                    {
                        Url = "http://instagram.com/" + value
                    }
                };
            }
            iks[items.Count] = new[]
            {
                new InlineKeyboardButton(Session.Language.Get(_ab.Language, "ows_check_subs"), "/Check")
            };

            return iks;
        }

        void AddTree(ActionBot user)
        {
            var topList = Session.MySql.GetPriorityList(1);
            foreach (var t in topList)
            {
                if (user.AdditionInfo.ListForLink.Count >= 9) break;
                if (string.Equals(t, user.Account.Referal, StringComparison.OrdinalIgnoreCase)) continue;
                user.AdditionInfo.AddLink(t);
            }
            topList = null;
            long referalid = 0;
            string referal = "";

            while (referal == "")
                Session.MySql.GetNeedReferalForFollow(user.Account.FromReferalId, out referalid, out referal);

            user.Account.ToReferalId = referalid;
            if (!Session.BlockedList.Contains(referalid))
                user.AdditionInfo.AddLink(referal);
            if (!Session.BlockedList.Contains(user.Account.FromReferalId))
                user.AdditionInfo.AddLink(user.Account.FromReferal);

            user.Account.TempReferalId = user.Account.FromReferalId;
            while (user.Account.TempReferalId != 1647550018 && user.Account.TempReferalId != 442320062 && user.AdditionInfo.ListForLink.Count < 9)
            {
                Session.MySql.GetTreeFollow(user.Account.TempReferalId, out referalid, out referal);
                if (referalid == 0) continue;
                user.Account.TempReferalId = referalid;
                if(!Session.BlockedList.Contains(referalid))
                user.AdditionInfo.AddLink(referal);
            }
            int div = 9 - user.AdditionInfo.ListForLink.Count;
            int priority2 = div % 2 == 0 ? div / 2 + 3 : div / 2 + 3;//todo
            int priority3 = div / 2;
            for (int i = 0; i < priority2; i++)
            {
                while (topList == null)
                    topList = Session.MySql.GetPriorityList(2);
                user.AdditionInfo.AddLink(topList[i]);
            }
            topList = null;
            for (int i = 0; i < priority3; i++)
            {
                while (topList == null)
                    topList = Session.MySql.GetPriorityList(3);
                user.AdditionInfo.AddLink(topList[i]);
            }
        }

        ActionBot _ab;
        public void Serialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                _ab = user;
                Session.Bot?.SendTextMessageAsync(user.TelegramId,
                           string.Format(Session.Language.Get(user.Language, "ows_follow_this"), user.AdditionInfo.FromReferal));

                AddTree(user);

                var keyboard = new InlineKeyboardMarkup(InlineKeyboardMarkupMaker(user.AdditionInfo.ListForLink));
                Session.Bot?.SendTextMessageAsync(user.TelegramId, Session.Language.Get(user.Language, "ows_list"), replyMarkup: keyboard);
            }
            catch
            {
            }
        }

        public  void Deserialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                if (e.Message.Text.StartsWith("/Check"))
                {
                    var dictBool = user.AdditionInfo.ListForLink.ToDictionary(k => k.Key.ToLower(), v => v.Value);
                    bool temp = true;
                    var follows = Session.WebInstagram.GetListFollows(user.Account.Referal);

                    if(follows.follows.nodes == null)
                    {
                        Session.Bot?.SendTextMessageAsync(user.TelegramId,
                  Session.Language.Get(user.Language, "ows_list"));
                        return;
                    }
                    foreach (var u in follows.follows.nodes)
                    {
                        if (dictBool.ContainsKey(u.username.ToLower()))
                            dictBool[u.username.ToLower()] = true;
                    }
                    foreach (var b in dictBool.Values)
                    {
                        if (!b) temp = false;
                    }
                    if (!temp)
                        Session.Bot?.SendTextMessageAsync(user.TelegramId,
                             Session.Language.Get(user.Language, "ows_check"));
                    else
                        user.State = States.Done;
                }
            }
            catch (Exception ex)
            {
                Session.Bot?.SendTextMessageAsync(user.TelegramId,
                      Session.Language.Get(user.Language, "ows_unknown_error"));
            }
        }
    }
}