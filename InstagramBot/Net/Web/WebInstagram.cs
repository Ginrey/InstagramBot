using InstagramBot.Data.Accounts;

namespace InstagramBot.Net.Web
{
   public class WebInstagram : Authorization
    {
        public WebInstagram(string login = "", string password = "") : base(login,password)
        {
            Auth();
         //   var u = GetUser("therock");
      //    string fff =  instWC.UploadString("https://www.instagram.com/query/",
       //         "q=ig_user(232192182){followed_by.first(10){count,page_info{end_cursor,has_next_page},nodes{id,is_verified,followed_by_viewer,requested_by_viewer,full_name,profile_pic_url,username}}}&ref=relationships::follow_list&query_id=17845270936146575");
        }

        public User GetUser(string referal)
        {
            return GetUserFromUrl("https://www.instagram.com/kevin" + referal+ "/?a=1");
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
