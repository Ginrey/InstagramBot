using System;
using System.Collections.Generic;
using System.Net;
using InstagramBot.Data.Accounts;
using Newtonsoft.Json;

namespace InstagramBot.Net.Web
{
    public class Authorization
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public WebProxy Proxy { get; set; }
        public bool IsDone { get; set; }
        InstWebClient instWC { get; set; }

        protected Authorization(string login = "", string password = "", string proxy = "", string passwordProxy = "")
        {
            Login = login;
            Password = password;
            if (!string.IsNullOrEmpty(proxy))
            {
                Proxy = new WebProxy(proxy)
                {
                    Credentials = new NetworkCredential(passwordProxy.Split(':')[0], passwordProxy.Split(':')[1])
                };

            }
            instWC = new InstWebClient(proxy : Proxy);
        }

        protected void Auth()
        {
            if (IsDone) return;
             instWC.UploadString("https://www.instagram.com/");

            Dictionary<string, string> code = new Dictionary<string, string>
            {
                {"username", Login},
                {"password", Password}
            };

            instWC.ResetHeaders(GetTokenFromCookie());
            var data =  instWC.UploadString("https://www.instagram.com/accounts/login/ajax/", code);
            instWC.ResetHeaders(GetTokenFromCookie());
            IsDone = data.Contains("true");
            Console.WriteLine(IsDone ? "Auth complete" : "Auth error");
        }

        string GetTokenFromCookie()
        {
            var headers = instWC.ResponseHeaders;
            var cookies = headers.Get("Set-Cookie").Split(';', ',');
            foreach (var cook in cookies)
            {
                if (cook.StartsWith("csrftoken"))
                {
                    return cook.Substring("csrftoken".Length + 1);
                }
            }
            return string.Empty;
        }

        protected FollowedUser GetFollowingListById(long referalId)
        {
            try
            {
                Dictionary<string, string> code = new Dictionary<string, string>
                {
                    {"q", "ig_user(" + referalId + "){followed_by.first(20){page_info{end_cursor,has_next_page},nodes{id,full_name,username}}}"}
                };
                instWC.ResetHeaders();
                var data = instWC.UploadString("https://www.instagram.com/query/", code);
                FollowedUser info = JsonConvert.DeserializeObject<FollowedUser>(data);
                return info;
            }
            catch
            {
                return new FollowedUser();
            }
        }

        protected FollowedUser GetFullFollowingListById(long referalId, string after)
        {
            try
            {
                Dictionary<string, string> code = new Dictionary<string, string>
                {
                  {"q","ig_user("+referalId+"){followed_by.after("+after+",20){page_info{end_cursor,has_next_page},nodes{id,full_name,username}}}"}
                };
                instWC.ResetHeaders();
                var data = instWC.UploadString("https://www.instagram.com/query/", code);
                FollowedUser info = JsonConvert.DeserializeObject<FollowedUser>(data);
                return info;
            }
            catch
            {
                return new FollowedUser();
            }
        }


        protected  FollowedUser GetFollowsListById(long referalId)
        {
            try
            {
                Dictionary<string, string> code = new Dictionary<string, string>
                {
                    {"q", "ig_user(" + referalId + "){follows.first(20){nodes{id,full_name,username}}}"}
                };
                instWC.ResetHeaders();
                var data = instWC.UploadString("https://www.instagram.com/query/", code);
                FollowedUser info = JsonConvert.DeserializeObject<FollowedUser>(data);
                return info;
            }
            catch
            {
                return new FollowedUser();
            }
        }

        protected  UserInfo GetUserFromUrl(string url)
        {
            try
            {
                string data = instWC.UploadString(url);
                UserInfo info = JsonConvert.DeserializeObject<UserInfo>(data);
                return info;
            }
            catch
            {
                return new UserInfo();
            }
        }

        protected User GetUserFromUrl(long id)
        {
            try
            {
                Dictionary<string, string> code = new Dictionary<string, string>
                {
                    {"q", "ig_user(" + id + "){id,username,full_name,followed_by{count},follows{count},media{count},is_private}"}
                };
                instWC.ResetHeaders();
                var data = instWC.UploadString("https://www.instagram.com/query/", code);
                User info = JsonConvert.DeserializeObject<User>(data);
                return info;
            }
            catch
            {
                return new User();
            }
        }
    }
}
