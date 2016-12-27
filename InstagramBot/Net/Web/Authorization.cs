using System;
using System.Collections.Generic;
using System.Xml;

namespace InstagramBot.Parsing
{
    public class Authorization
    {
        public string Login { get; set; }
        public string Password { get; set; }
        InstWebClient instWC = new InstWebClient();
        public bool IsDone { get; set; }

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

        protected string GetUserNamefromURL(string url)
        {
            string data = instWC.UploadString(url);
            data = data.Substring(data.IndexOf("<script type=\"text/javascript\">window._sharedData"));
            data = data.Substring(0, data.IndexOf("</script>") + "</script>".Length);
            var d = new XmlDocument();
            d.LoadXml(data);

         //   JsonObject root = Windows.Data.Json.JsonValue.Parse(jsonString).GetObject();
            //rData.BaseAccount = d["Auth"].Attributes["PersId"].Value;
            return "";
        }
    }
}
