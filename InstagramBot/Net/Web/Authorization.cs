#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using InstagramBot.Data.Accounts;
using Newtonsoft.Json;

#endregion

namespace InstagramBot.Net.Web
{
    public class Authorization
    {
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
            instWC = new InstWebClient(proxy: Proxy);
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public WebProxy Proxy { get; set; }
        public bool IsDone { get; set; }
        InstWebClient instWC { get; }

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

        protected Subscriptions.FollowedUser GetFollowingListById(long referalId)
        {
            try
            {    
                instWC.ResetHeaders();
                var data = instWC.UploadString("https://www.instagram.com/graphql/query/?query_hash=37479f2b8209594dde7facb0d904896a&variables=" + JsonConvert.SerializeObject(new {id = referalId, first = "50"}));
                Subscriptions.FollowedUser info = JsonConvert.DeserializeObject<Subscriptions.FollowedUser>(data);
                return info;
            }
            catch
            {
                return new Subscriptions.FollowedUser();
            }
        }

        protected Subscriptions.FollowedUser GetFullFollowingListById(long referalId, string afterKey)
        {
            try
            {  
                instWC.ResetHeaders();
                var data = instWC.UploadString(
                    "https://www.instagram.com/graphql/query/?query_hash=37479f2b8209594dde7facb0d904896a&variables=" +
                    JsonConvert.SerializeObject(new {id = referalId, first = "50", after = afterKey}));
                Subscriptions.FollowedUser info = JsonConvert.DeserializeObject<Subscriptions.FollowedUser>(data);
                return info;
            }
            catch
            {
                return new Subscriptions.FollowedUser();
            }
        }


        protected Subscriptions.FollowedUser GetFollowsListById(long referalId)
        {
            try
            {    
                instWC.ResetHeaders();
                var data = instWC.UploadString("https://www.instagram.com/graphql/query/?query_hash=58712303d941c6855d4e888c5f0cd22f&variables=" + JsonConvert.SerializeObject(new {id = referalId, first = "50"}));
                Subscriptions.FollowedUser info = JsonConvert.DeserializeObject<Subscriptions.FollowedUser>(data);
                return info;
            }
            catch
            {
                return new Subscriptions.FollowedUser();
            }
        }

        protected UserInfo GetUserFromUrl(string url)
        {
            try
            {
                instWC.ResetHeaders();
                var split = url.Split('/');
                instWC.Headers["Referer"] = "https://www.instagram.com/"+split[split.Length -1];
                string data = instWC.UploadString(url);
                UserInfo info = JsonConvert.DeserializeObject<UserInfo>(data);
                return info;
            }
            catch(Exception e)
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
                    {
                        "q",
                        "ig_user(" + id +
                        "){id,username,full_name,followed_by{count},follows{count},media{count},is_private}"
                    }
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