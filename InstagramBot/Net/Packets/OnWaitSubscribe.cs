using System;
using System.Collections.Generic;
using System.Linq;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;
using InstagramBot.IO;
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
            try
            {
                if (user.AdditionInfo.Full) return;

                List<MiniInfo> topList;
                Session.MySql.GetPriority(1, out topList);
                foreach (var t in topList)
                {
                    if (user.AdditionInfo.Full) return;
                    if (string.Equals(t.Url, user.Account.Info.Url, StringComparison.OrdinalIgnoreCase)) continue;
                    user.AdditionInfo.AddLink(t);
                }
                if (Session.PrivilegeList.Contains(user.Account.Info.Url.ToLower()))
                {
                    user.Account.IsVip = true;
                    return;
                }
                var blogger = Bloggers.Next();
                if (!Session.BlockedList.Contains(blogger.Id))
                    user.AdditionInfo.AddLink(blogger);

                if (user.AdditionInfo.Full) return;
                topList = null;

                MiniInfo info = new MiniInfo();
                while (string.IsNullOrEmpty(info.Url))
                    Session.MySql.GetReferal(user.Account.From.Id, out info);
                if (!Session.BlockedList.Contains(info.Id))
                    user.AdditionInfo.AddLink(info);
                info.Reset();

                while (string.IsNullOrEmpty(info.Url))
                    Session.MySql.GetBaseInstagram(user.Account.From.Id, out info);
                if (!Session.BlockedList.Contains(info.Id))
                    user.AdditionInfo.AddLink(info);
                user.Account.To = info.Copy;

                if (user.AdditionInfo.Full) return;

                user.Account.Temp = user.Account.To.Copy;
                while (user.Account.Temp.Id != 1647550018 && user.Account.Temp.Id != 442320062 &&
                       user.AdditionInfo.ListForLink.Count < 9)
                {
                    Session.MySql.GetTreeInstagram(user.Account.Temp.Id, out info);
                    if (info.Id == -1) continue;
                    user.Account.Temp = info.Copy;
                    if (!Session.BlockedList.Contains(info.Id))
                        user.AdditionInfo.AddLink(info);
                }
                if (user.AdditionInfo.Full) return;

                var corruption = Corruption.Next();
                if (!Session.BlockedList.Contains(corruption.Id))
                    user.AdditionInfo.AddLink(corruption);

                if (user.AdditionInfo.Full) return;

                while (topList == null)
                    Session.MySql.GetPriority(2, out topList);
                user.AdditionInfo.AddLink(topList[0]);

                while (!user.AdditionInfo.Full)
                {
                    blogger = Bloggers.Next();
                    if (!Session.BlockedList.Contains(blogger.Id))
                        user.AdditionInfo.AddLink(blogger);
                }

                if (!user.AdditionInfo.Full) AddTree(user);
            } catch (Exception ex)
            { LOG.Add("OWS tree", ex); }
        }

        public async void Serialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                await Session.Bot?.SendTextMessageAsync(user.TelegramId,
                    string.Format(Session.Language.Get(user.Language, "ows_follow_this"), user.AdditionInfo.FromReferal));
                user.AdditionInfo.ListForLink.Clear();
                user.AdditionInfo.LinkIds.Clear();
                AddTree(user);

                ShowLink(user);
            }
            catch(Exception ex)
            {
                LOG.Add("OWS S", ex);
            }
        }
       
        async void ShowLink(ActionBot tempBot)
        {
            try
            {
                foreach (var item in tempBot.AdditionInfo.ListForLink.Keys)
                {
                    string key = item.ToLower();
                    if (tempBot.AdditionInfo.Uses.Contains(key)) continue;
                    tempBot.AdditionInfo.Uses.Add(key);
                    var keyboard = new InlineKeyboardMarkup(OneKeyboardMarkupMaker(key, tempBot));
                    await Session.Bot?.SendTextMessageAsync(tempBot.TelegramId, key, replyMarkup: keyboard);
                    return;
                }
                tempBot.AdditionInfo.Complete = true;
                tempBot.State = States.Done;
            }catch(Exception ex) { LOG.Add("OWS Link", ex); }
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
                LOG.Add("OWS D", ex);
                Session.Bot?.SendTextMessageAsync(user.TelegramId,
                    Session.Language.Get(user.Language, "ows_unknown_error")+ex.Message);
            }
        }

       async void Check(ActionBot tempBot)
       {
           try
           {
               if (!tempBot.AdditionInfo.Full) AddTree(tempBot);
               bool temp = false;
               var follows = Session.WebInstagram.GetListFollows(tempBot.Account.Info.Url);

               if (follows?.follows.nodes == null)
               {
                   await
                       Session.Bot?.SendTextMessageAsync(tempBot.TelegramId,
                           Session.Language.Get(tempBot.Language, "ows_nodes_null"));
                   return;
               }
               foreach (var u in follows.follows.nodes)
               {
                   if (u.username.ToLower() == tempBot.AdditionInfo.Uses.Last())
                       temp = true;
               }

               if (temp)
               {
                   if (tempBot.AdditionInfo.Complete)
                   {
                       tempBot.State = States.Done;
                       return;
                   }
                   ShowLink(tempBot);
               }
               else
                   await
                       Session.Bot?.SendTextMessageAsync(tempBot.TelegramId,
                           Session.Language.Get(tempBot.Language, "ows_check"));
           }
           catch (Exception ex)
           {
               LOG.Add("OWS Check",  ex);
           }
       }

        void Block(string[] command, ActionBot tempBot)
        {
            try
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
                    Session.BlockedList.Add(account.Info.Id);
                    if (tempBot.AdditionInfo.ListForLink.ContainsKey(command[1]))
                        tempBot.AdditionInfo.ListForLink.Remove(command[1]);
                    AddTree(tempBot);
                    ShowLink(tempBot);
                    return;
                }
                Session.Bot?.SendTextMessageAsync(tempBot.TelegramId,
                    Session.Language.Get(tempBot.Language, "ows_error_not_private"));
            }
            catch (Exception ex) { LOG.Add("OWS Block", ex); }
        }
    }
}