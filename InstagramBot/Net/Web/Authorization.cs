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

        protected User GetUserFromUrl(string url)
        {
            try
            {
                string data = instWC.UploadString(url);
                data = data.Substring(data.IndexOf("window._sharedData = ") + "window._sharedData = ".Length);
                data = data.Substring(0, data.IndexOf("</script>") - 1);
                FullInfo info = JsonConvert.DeserializeObject<FullInfo>(data);
                return info.EntryData.ProfilePage[0].user;
            }
            catch
            {
                return new User();
            }
        }
    }
}
