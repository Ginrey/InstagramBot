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

      /*  InlineKeyboardButton[][] InlineKeyboardMarkupMaker(Dictionary<string, bool> items)
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
                    },
                    new InlineKeyboardButton("Закрытый аккаунт", "/Private " + value)
                };
            }
            iks[items.Count] = new[]
            {
                new InlineKeyboardButton(Session.Language.Get(tempBot.Language, "ows_check_subs"), "/Check")
            };
            
            return iks;
        }*/

        InlineKeyboardButton[][] OneKeyboardMarkupMaker(string name, ActionBot tempBot)
        {
            InlineKeyboardButton[][] iks = new InlineKeyboardButton[3][];
            iks[0] = new[]
            {
                new InlineKeyboardButton(name, "/Url")
                {
                    Url = "http://instagram.com/" + name
                }
            };
            iks[1] = new[]
            {
                new InlineKeyboardButton(Session.Language.Get(tempBot.Language, "ows_check_subs"), "/Check " + name)
            };
            iks[2] = new[]
            {
                new InlineKeyboardButton(Session.Language.Get(tempBot.Language, "ows_private"), "/Private " + name)
            };
            return iks;
        }

        void AddTree(ActionBot user)
        {
            if (user.AdditionInfo.ListForLink.Count >= 9) return;

            List<MiniInfo> topList;
            Session.MySql.GetPriority(1, out topList);
            foreach (var t in topList)
            {
                if (user.AdditionInfo.Full) return;
                if (string.Equals(t.URL, user.Account.Referal, StringComparison.OrdinalIgnoreCase)) continue;
                user.AdditionInfo.AddLink(t.URL);
            }
            if (user.AdditionInfo.Full) return;//todo Коррупционный список
            topList = null;

            MiniInfo info = new MiniInfo();
            while (string.IsNullOrEmpty(info.URL))
                Session.MySql.GetReferal(user.Account.From.ID, out info);
            if (!Session.BlockedList.Contains(info.ID))
                user.AdditionInfo.AddLink(info.URL);
            info.Reset();

            while (string.IsNullOrEmpty(info.URL))
                Session.MySql.GetBaseInstagram(user.Account.From.ID, out info);
            if (!Session.BlockedList.Contains(info.ID))
                user.AdditionInfo.AddLink(info.URL);
            user.Account.To = info;

            if (user.AdditionInfo.Full) return;

            user.Account.Temp = user.Account.To;
            while (user.Account.Temp.ID != 1647550018 && user.Account.Temp.ID != 442320062 && user.AdditionInfo.ListForLink.Count < 9)
            {
                Session.MySql.GetTreeInstagram(user.Account.Temp.ID, out info);
                if (info.ID == -1) continue;
                user.Account.Temp = info;
                if(!Session.BlockedList.Contains(info.ID))
                user.AdditionInfo.AddLink(info.URL);
            }
            if (user.AdditionInfo.Full) return;
         
            while (topList == null)
                 Session.MySql.GetPriority(2, out topList);
           
            foreach (MiniInfo t in topList)
            {
                if (user.AdditionInfo.Full) return;
                user.AdditionInfo.AddLink(t.URL);
            }
            topList = null;

            while (topList == null)
                Session.MySql.GetPriority(3, out topList);

            foreach (MiniInfo t in topList)
            {
                if (user.AdditionInfo.Full) return;
                user.AdditionInfo.AddLink(t.URL);
            }

            if (!user.AdditionInfo.Full) AddTree(user);
        }

        public async void Serialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                await Session.Bot?.SendTextMessageAsync(user.TelegramId,
                    string.Format(Session.Language.Get(user.Language, "ows_follow_this"), user.AdditionInfo.FromReferal));

                AddTree(user);

                ShowLink(user);
            }
            catch(Exception ex)
            {
            }
        }

       
        async void ShowLink(ActionBot tempBot)
        {
            foreach (var item in tempBot.AdditionInfo.ListForLink.Keys)
            {
                string key = item.ToLower();
                if (tempBot.AdditionInfo.Uses.Contains(key)) continue;
                tempBot.AdditionInfo.Uses.Add(key);
                tempBot.AdditionInfo.TempLink = key;
                var keyboard = new InlineKeyboardMarkup(OneKeyboardMarkupMaker(key,tempBot));
                await Session.Bot?.SendTextMessageAsync(tempBot.TelegramId, key, replyMarkup: keyboard);
                return;
            }
            tempBot.AdditionInfo.Complete = true;
            tempBot.State = States.Done;
        }

       
       
        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                if (e.Message.Text.StartsWith("/Check"))
                {
                    string[] command = e.Message.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (command.Length < 2)
                    {
                        throw new Exception("Error command");
                    }
                    Check(user);
                }
                if (e.Message.Text.StartsWith("/Private"))
                {
                    string[] command = e.Message.Text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    if (command.Length < 2)
                    {
                        throw new Exception("Error private command");
                    }
                    Block(command, user);
                }
            }
            catch (Exception ex)
            {
                Session.Bot?.SendTextMessageAsync(user.TelegramId,
                    Session.Language.Get(user.Language, "ows_unknown_error"));
            }
        }

       async void Check(ActionBot tempBot)
        {
            bool temp = false;
            var follows = Session.WebInstagram.GetListFollows(tempBot.Account.Referal);

            if (follows?.follows.nodes == null)
            {
               await Session.Bot?.SendTextMessageAsync(tempBot.TelegramId, Session.Language.Get(tempBot.Language, "ows_nodes_null"));
                return;
            }
            foreach (var u in follows.follows.nodes)
            {
                if (u.username.ToLower() == tempBot.AdditionInfo.TempLink)
                    temp = true;
            }

            if (temp)
            {
               // tempBot.AdditionInfo.ListForLink[command[1].ToLower()] = true;
                if (tempBot.AdditionInfo.Complete)
                {
                    tempBot.State = States.Done;
                    return;
                }else
                ShowLink(tempBot);
               
            }
            else
               await Session.Bot?.SendTextMessageAsync(tempBot.TelegramId, Session.Language.Get(tempBot.Language, "ows_check"));
        }

        void Block(string[] command, ActionBot tempBot)
        {
            var account = Session.WebInstagram.GetAccount(command[1]);

            if (account == null)
            {
                if (tempBot.AdditionInfo.ListForLink.ContainsKey(command[1]))
                    tempBot.AdditionInfo.ListForLink.Remove(command[1]);
                AddTree(tempBot);
                ShowLink(tempBot);
                return;
            }
            if (account.IsPrivate)
            {
                Session.BlockedList.Add(account.Uid);
                if (tempBot.AdditionInfo.ListForLink.ContainsKey(command[1]))
                    tempBot.AdditionInfo.ListForLink.Remove(command[1]);
                AddTree(tempBot);
                ShowLink(tempBot);
                return;
            }
             Session.Bot?.SendTextMessageAsync(tempBot.TelegramId, Session.Language.Get(tempBot.Language, "ows_error_not_private"));
        }
    }
}