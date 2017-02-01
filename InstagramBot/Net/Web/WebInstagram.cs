using System.Collections.Generic;
using System.IO;
using System.Linq;
using InstagramBot.Data.Accounts;

namespace InstagramBot.Net.Web
{
   public class WebInstagram : Authorization
    {
        public WebInstagram(string login = "", string password = "", string proxy = "", string domainpassword = "") : base(login,password, proxy, domainpassword)
        {
            Auth();
        }
        public  FollowedUser GetListFollowing(string referal)
        {
            return  GetFollowingListById(GetAccount(referal).Uid);
        }

        public FollowedUser GetFullListFollowing(string referal)
        {
            var account = GetAccount(referal);
            List<string> lst = new List<string>();
            var following = GetFollowingListById(account.Uid);
            
                while (following.followed_by.page_info.has_next_page)
                {
                    foreach (var f in following.followed_by.nodes)
                        if (!lst.Contains(f.id + " - " + f.username))
                            lst.Add(f.id + " - " + f.username);
                    string next = following.followed_by.page_info.end_cursor;
                    following = GetFullFollowingListById(account.Uid, next);
                    while(following.followed_by == null)
                    {
                        System.Threading.Thread.Sleep(20050);
                    following = GetFullFollowingListById(account.Uid, next);
                }
                    File.WriteAllText(referal + ".txt", string.Join("\n",lst.ToArray()));
                    System.Threading.Thread.Sleep(50);
                }
           
            return GetFollowingListById(GetAccount(referal).Uid);
        }
        public  FollowedUser GetListFollows(string referal)
        {
            var acc =  GetAccount(referal);
            if (acc == null || acc.IsPrivate) return null;
            return  GetFollowsListById(acc.Uid);
        }
        public UserInfo GetUser(string referal)
        {
            try
            {
                if (referal.Any(wordByte => wordByte > 127) || referal.Contains(' ') || referal.Contains("/"))  return new UserInfo();
                return  GetUserFromUrl("https://www.instagram.com/" + referal + "/?__a=1");
            }
            catch
            {
                return new UserInfo();
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
                User userInfo =  GetUser(referal).user;
                if (string.IsNullOrEmpty(userInfo?.id)) return null;
                return new AccountInstagram
                {
                    Uid = long.Parse(userInfo.id),
                    Name = userInfo.full_name,
                    Referal = userInfo.username,
                    Following = userInfo.followed_by.count,
                    Folowers = userInfo.follows.count,
                    Posts = userInfo.media.count,
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
                    Uid = long.Parse(userInfo.id),
                    Name = userInfo.full_name,
                    Referal = userInfo.username,
                    Following = userInfo.followed_by.count,
                    Folowers = userInfo.follows.count,
                    Posts = userInfo.media.count,
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
