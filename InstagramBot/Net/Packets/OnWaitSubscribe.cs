using System;
using System.Collections.Generic;
using System.Linq;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace InstagramBot.Net.Packets
{
    public class OnWaitSubscribe : ActionPacket
    {
        public Session Session { get; set; }
        Random rnd = new Random();
        InlineKeyboardButton[] InlineKeyboardMarkupMaker(Dictionary<string, bool> items)
        {
            InlineKeyboardButton[] ik = items.Select(item =>new InlineKeyboardButton(item.Key, "/Url") {Url = "http://instagram.com/"+item.Key }).ToArray();
            return ik;
        }

        public void Serialize(ActionBot user, StateEventArgs e)
        {
            Session.Bot?.SendTextMessageAsync(user.TelegramID,
                "Подпишитесь в Instagram на данные аккаунты");
            long referalid;
            string referal;

            Session.MySql.GetNeedReferalForFollow(user.Account.FromReferalId, out referalid, out referal);
            var  topList = Session.MySql.GetPriorityList(1);
            foreach(var t in topList)
            {
                if (user.NeedFollows.Count < 3)
                    if(!user.NeedFollows.ContainsKey(t))
                        user.NeedFollows.Add(t, false);
            }

            user.Account.ToReferalId = referalid;
            if (!user.NeedFollows.ContainsKey(referal))
                user.NeedFollows.Add(referal, false);

            if (user.NeedFollows.Count < 3)
            {
                topList = Session.MySql.GetPriorityList(2);
                for (int i = 0; i < topList.Count; i++)
                {
                    if (user.NeedFollows.Count >= 3) break;
                    var t = topList[rnd.Next(topList.Count)];
                    if (!user.NeedFollows.ContainsKey(t))
                        user.NeedFollows.Add(t, false);
                }
            }

            var keyboard = new InlineKeyboardMarkup(new[]
                {
                    InlineKeyboardMarkupMaker(user.NeedFollows), new[] {new InlineKeyboardButton("Подтвердить подписку", "/Check")}
                }
            );
            Session.Bot?.SendTextMessageAsync(user.TelegramID, "Список:", replyMarkup: keyboard);
        }

        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            if (e.Message.Text.StartsWith("/Check"))
            {
                var dict = user.NeedFollows.ToDictionary(k => k.Key, v => v.Value);
                bool temp = true;
                foreach (var u in user.NeedFollows.Keys)
                {
                    var follows = Session.WebInstagram.GetListFollowing(u);
                    if (follows.followed_by.nodes.Any(f => f.username == user.Account.Referal))
                    {
                        dict[u] = true;
                    }
                }
                foreach (var b in dict.Values)
                {
                    if (!b) temp = false;
                }
                if (!temp)
                    Session.Bot?.SendTextMessageAsync(user.TelegramID,
                        "Вы не подписались. Подпишитесь и повторите попытку, отправив команду /Check");
                else
                    user.State++;
            }
        }
    }
}