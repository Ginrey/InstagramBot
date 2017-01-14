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
                new InlineKeyboardButton("Проверить подписку", "/Check")
            };

            return iks;
        }

        void AddTree(ActionBot user)
        {

        }

        public void Serialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                Session.Bot?.SendTextMessageAsync(user.TelegramId,
                    "Подпишитесь в Instagram на данные аккаунты.\n" +
                    "Если вы уже были подписаны на указанные аккаунты: отпишитесь и заново подпишитесь.");
                long referalid = 0;
                string referal = "";

                var topList = Session.MySql.GetPriorityList(1);
                foreach (var t in topList)
                {
                    if (user.AdditionInfo.ListForLink.Count >= 9) break;
                    if (string.Equals(t, user.Account.Referal, StringComparison.OrdinalIgnoreCase)) continue;
                    user.AdditionInfo.AddLink(t);
                }
                topList = null;

                while (referal == "")
                    Session.MySql.GetNeedReferalForFollow(user.Account.FromReferalId, out referalid, out referal);

                user.Account.ToReferalId = referalid;
                user.AdditionInfo.AddLink(referal);
                user.AdditionInfo.AddLink(user.Account.FromReferal);

                user.Account.TempReferalId = user.Account.FromReferalId;
                while(user.Account.TempReferalId != 1647550018 && user.Account.TempReferalId != 442320062)
                {
                    Session.MySql.GetTreeFollow(user.Account.TempReferalId, out referalid, out referal);
                    if (referalid == 0) continue;
                    user.Account.TempReferalId = referalid;
                    user.AdditionInfo.AddLink(referal);
                }

                if (user.AdditionInfo.ListForLink.Count < 9)
                {
                    while (topList == null)
                        topList = Session.MySql.GetPriorityList(2);
                    foreach (var t in topList)
                    {
                        if (user.AdditionInfo.ListForLink.Count >= 9) break;
                        if (string.Equals(t, user.Account.Referal, StringComparison.OrdinalIgnoreCase)) continue;
                        user.AdditionInfo.AddLink(t);
                    }
                }
               
                var keyboard = new InlineKeyboardMarkup(InlineKeyboardMarkupMaker(user.AdditionInfo.ListForLink));
                Session.Bot?.SendTextMessageAsync(user.TelegramId, "Список:", replyMarkup: keyboard);
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

                    if(follows.follows == null)
                    {
                        Session.Bot?.SendTextMessageAsync(user.TelegramId,
                   "Ошибка, повторите попытку.Ваш аккаунт не должен быть приватным");
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
                            "Вы не подписались. Подпишитесь и повторите попытку, отправив команду /Check");
                    else
                        user.SetState(States.Done);
                }
            }
            catch (Exception ex)
            {
                Session.Bot?.SendTextMessageAsync(user.TelegramId,
                    "Неизвестная ошибка, пожалуйста сообщите о ней");
            }
        }
    }
}