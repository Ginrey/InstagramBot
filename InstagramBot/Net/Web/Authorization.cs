using System;
using System.Collections.Generic;
using InstagramBot.Data.Accounts;
using Newtonsoft.Json;

namespace InstagramBot.Net.Web
{
    public class Authorization
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsDone { get; set; }
        InstWebClient instWC = new InstWebClient();

        protected Authorization(string login = "", string password = "")
        {
            Login = login;
            Password = password;
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
            var data = instWC.UploadString("https://www.instagram.com/accounts/login/ajax/", code);
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

        protected FollowedUser GetFoloowingListById(long referalId)
        {
            try
            {
                Dictionary<string, string> code = new Dictionary<string, string>
                {
                    {"q", "ig_user("+referalId+"){followed_by.first(20){nodes{id,full_name,username}}}"}
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

        protected UserInfo GetUserFromUrl(string url)
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
    }
}
