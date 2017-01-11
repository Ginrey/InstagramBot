using InstagramBot.Data.Accounts;

namespace InstagramBot.Net.Web
{
   public class WebInstagram : Authorization
    {
        public WebInstagram(string login = "", string password = "") : base(login,password)
        {
            Auth();
        }
        public FollowedUser GetListFollowing(string referal)
        {
            return GetFollowingListById(GetAccount(referal).Uid);
        }
        public FollowedUser GetListFollows(string referal)
        {
            var acc = GetAccount(referal);
            if (acc == null) return null;
            return GetFollowsListById(acc.Uid);
        }
        public UserInfo GetUser(string referal)
        {
            try
            {
                return GetUserFromUrl("https://www.instagram.com/" + referal + "/?__a=1");
            }
            catch
            {
                return new UserInfo();
            }
        }
        public AccountInstagram GetAccount(string referal)
        {
            try
            {
                User userInfo = GetUser(referal).user;
                if (string.IsNullOrEmpty(userInfo.id)) return null;
                return new AccountInstagram
                {
                    Uid = long.Parse(userInfo.id),
                    Name = userInfo.full_name,
                    Referal = userInfo.username,
                    Following = userInfo.followed_by.count,
                    Folowers = userInfo.follows.count,
                    Posts = userInfo.media.count
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
