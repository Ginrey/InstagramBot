﻿using System.Linq;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;

namespace InstagramBot.Net.Packets
{
    public class OnWaitSubscribe : ActionPacket
    {
        public Session Session { get; set; }

        public void Serialize(ActionBot user, StateEventArgs e)
        {
            Session.Bot?.SendTextMessageAsync(user.TelegramID, "Чтобы начать получать своих подписчиков Вам необходимо подписаться на следующих людей");
            long referalid;
            string referal;
            Session.MySql.GetNeedReferalForFollow(user.Account.FromReferalId, out referalid, out referal);
            user.Account.ToReferalId = referalid;
            user.NeedFollows.Add(referal, false);
            Session.Bot?.SendTextMessageAsync(user.TelegramID, user.NeedFollows.Keys.Aggregate("", (current, r) => current + r + "\n"));
            Session.Bot?.SendTextMessageAsync(user.TelegramID, "После того как подпишитесь отправьте команду /Check");
        }
        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            if(e.Message.Text.StartsWith("/Check"))
            {
                var dict = user.NeedFollows.ToDictionary(k => k.Key, v => v.Value);
                bool temp=true;
                foreach(var u in user.NeedFollows.Keys)
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
