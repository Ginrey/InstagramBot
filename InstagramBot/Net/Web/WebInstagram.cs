using System.Threading.Tasks;
using InstagramBot.Data.Accounts;

namespace InstagramBot.Net.Web
{
   public class WebInstagram : Authorization
    {
        public WebInstagram(string login = "", string password = "") : base(login,password)
        {
            Auth();
        }
        public async Task<FollowedUser> GetListFollowing(string referal)
        {
            return await GetFollowingListById((await GetAccount(referal)).Uid);
        }
        public async Task<FollowedUser> GetListFollows(string referal)
        {
            var acc = await GetAccount(referal);
            if (acc == null) return null;
            return await GetFollowsListById(acc.Uid);
        }
        public async Task<UserInfo> GetUser(string referal)
        {
            try
            {
                return await GetUserFromUrl("https://www.instagram.com/" + referal + "/?__a=1");
            }
            catch
            {
                return new UserInfo();
            }
        }
        public async Task<AccountInstagram> GetAccount(string referal)
        {
            try
            {
                User userInfo = (await GetUser(referal)).user;
                if (string.IsNullOrEmpty(userInfo?.id)) return null;
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
