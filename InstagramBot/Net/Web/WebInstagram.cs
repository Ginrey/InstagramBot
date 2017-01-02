using InstagramBot.Data.Accounts;

namespace InstagramBot.Net.Web
{
   public class WebInstagram : Authorization
    {
        public WebInstagram(string login = "", string password = "") : base(login,password)
        {
            Auth();
        }
        public Subscriptions.FollowedBy GetListFollowing(long referalId)
        {
            return GetFoloowingListBuId(referalId);
        }
        public User GetUser(string referal)
        {
            return GetUserFromUrl("https://www.instagram.com/" + referal+ "/?a=1");
        }
        public AccountInstagram GetAccount(string referal)
        {
            User user = GetUser(referal);
            if (string.IsNullOrEmpty(user.id)) return null;
            return new AccountInstagram
            {
                Uid = long.Parse(user.id),
                Name = user.full_name,
                Referal = user.username,
                Following = user.followed_by.count,
                Folowers = user.follows.count,
                Posts = user.media.count
            };
        }
    }
}
