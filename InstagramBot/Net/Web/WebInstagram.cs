#region

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using InstagramBot.Data.Accounts;
using System;

#endregion

namespace InstagramBot.Net.Web
{
    public class WebInstagram : Authorization
    {
        public WebInstagram(string login = "", string password = "", string proxy = "", string domainpassword = "")
            : base(login, password, proxy, domainpassword)
        {
            Auth();
        }

        public Subscriptions.FollowedUser GetListFollowing(string referal)
        {
            return GetFollowingListById(GetAccount(referal).Info.Id);
        }

        public Subscriptions.FollowedUser GetFullListFollowing(string referal)
        {
            var account = GetAccount(referal);
            List<string> lst = new List<string>();
            var following = GetFollowingListById(account.Info.Id);

            while (following.data.user.edge_follow.page_info.has_next_page)
            {
                foreach (var f in following.data.user.edge_follow.edges)
                    if (!lst.Contains(f.node.id + " - " + f.node.username))
                        lst.Add(f.node.id + " - " + f.node.username);
                string next = following.data.user.edge_follow.page_info.end_cursor;
                following = GetFullFollowingListById(account.Info.Id, next);
                while (following.data.user.edge_follow == null)
                {
                    Thread.Sleep(20050);
                    following = GetFullFollowingListById(account.Info.Id, next);
                }
                File.WriteAllText(referal + ".txt", string.Join("\n", lst.ToArray()));
                Thread.Sleep(50);
            }

            return GetFollowingListById(GetAccount(referal).Info.Id);
        }

        public Subscriptions.FollowedUser GetListFollows(string referal)
        {
            var acc = GetAccount(referal);
            if ((acc == null) || acc.IsPrivate) return null;
            return GetListFollows(acc.Info.Id);
        }

        public Subscriptions.FollowedUser GetListFollows(long id)
        {
            return GetFollowsListById(id);
        }

        public Info.NewInfo GetUser(string referal)
        {
            try
            {
                if (referal.Any(wordByte => wordByte > 127) || referal.Contains(' ') || referal.Contains("/"))
                    return new Info.NewInfo();
                return GetUserFromUrl("https://www.instagram.com/" + referal + "/?__a=1");
            }
            catch
            {
                return new Info.NewInfo();
            }
        }

        public List<string> listTayga = new List<string>
        {
            "Т8",
 "Тайга8",
 "Tayga8",
 "Vilavi",
 "Дикорастущий",
 "Таёжный"
        };
        public List<string> listNL = new List<string>
        {
            "Создатели бренда",
 "Бизнес империя",
 "Ищешь работу",
 "Набираем команду",
 "Набираею команду",
  "Научу зарабатывать",
 "Научим зарабатывать",
 "Менеджер",
 "Партнер",
 "Наставник",
 "NL",
 "Выведу на доход",
  "Выведим на доход",
        };
        public void SertedList(List<string> urls)
        {
            int index = 0;          
            bool inlist = false;
           foreach(var url in urls)
            {
                Console.Title = $"[{++index}]/[{url.Length}]";
                inlist = false;
                UserInfo user = null;
                try
                {
                    start:
                    if(inlist)
                    {
                        continue;
                    }
                    user = GetUser(url);
                    if (user == null || user.graphql.user == null) { Thread.Sleep(5050); continue; }
                 /*   while (user.user == null || user.user.followed_by == null)
                    {
                        Thread.Sleep(20050);
                        user = GetUser(url);
                    }*/
                    if (user.graphql.user.biography == null) goto end;
                    foreach(var t8 in listTayga)
                    if(user.graphql.user.biography.ToString().ToLower().Contains(t8.ToLower()))
                        {
                            File.AppendAllText(@"T8.txt", $"{user.graphql.user.username}|{user.graphql.user.id}|{user.graphql.user.edge_followed_by.count}\r\n");
                            inlist = true;
                            goto start;
                        }
                    foreach (var nl in listNL)
                        if (user.graphql.user.biography != null && user.graphql.user.biography.ToString().ToLower().Contains(nl.ToLower()))
                        {
                            File.AppendAllText(@"NL.txt", $"{user.graphql.user.username}|{user.graphql.user.id}|{user.graphql.user.edge_followed_by.count}\r\n");
                            inlist = true;
                            goto start;
                        }
                    end:
                    File.AppendAllText(@"Users.txt", $"{user.graphql.user.username}|{user.graphql.user.id}|{user.graphql.user.edge_followed_by.count}\r\n");
                }
                catch { }
            }
        }

        public User GetUser(long id)
        {
            try
            {
                return GetUserFromUrl(id);
            }
            catch
            {
                return new User();
            }
        }

        public AccountInstagram GetAccount(string referal)
        {
            try
            {
                User userInfo = GetUser(referal).graphql.user;
                if (string.IsNullOrEmpty(userInfo?.id)) return null;
                return new AccountInstagram
                {
                    Info = new MiniInfo(long.Parse(userInfo.id), userInfo.username),
                    Name = userInfo.full_name,
                    Following = userInfo.edge_followed_by.count,
                    Folowers = userInfo.edge_follow.count,
                    Posts = userInfo.edge_media_collections.count,
                    IsPrivate = userInfo.is_private
                };
            }
            catch
            {
                return null;
            }
        }

        public AccountInstagram GetAccount(long id)
        {
            try
            {
                User userInfo = GetUser(id);
                if (string.IsNullOrEmpty(userInfo?.id)) return null;
                return new AccountInstagram
                {
                    Info = new MiniInfo(long.Parse(userInfo.id), userInfo.username),
                    Name = userInfo.full_name,
                    Following = userInfo.edge_followed_by.count,
                    Folowers = userInfo.edge_follow.count,
                    Posts = userInfo.edge_media_collections.count,
                    IsPrivate = userInfo.is_private
                };
            }
            catch
            {
                return null;
            }
        }
    }
}