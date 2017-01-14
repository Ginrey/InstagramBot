using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InstagramBot.Net.Web
{
    public class InstWebClient : WebClient
    {
        public string UserAgent { get; private set; }
       
        public CookieContainer CookieContainer { get; set; }
          protected override WebRequest GetWebRequest(Uri address)
          {
              WebRequest request = base.GetWebRequest(address);

              if (request is HttpWebRequest)
              {
                  (request as HttpWebRequest).CookieContainer = this.CookieContainer;
              }
              HttpWebRequest httpRequest = (HttpWebRequest)request;
              httpRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
              return httpRequest;
          }
     
      /*  protected override WebResponse GetWebResponse(WebRequest request)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)request;
            httpRequest.AllowAutoRedirect = false;
            return httpRequest.GetResponse();
        }*/
        
        public InstWebClient(string userAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko", IWebProxy proxy = null)
        {
            if (proxy != null)
            {               
                Proxy = proxy;
            }
            UserAgent = userAgent;
            CookieContainer = new CookieContainer();
            ResetHeaders("");
            
            ServicePointManager.ServerCertificateValidationCallback += (o, certificate, chain, errors) => true;
        }

        public WebHeaderCollection GetResponseHeaders()
        {
            return ResponseHeaders;
        }

        void SetToket(string token)
        {
            if(!string.IsNullOrEmpty(token))
            Headers["X-CSRFToken"] = token;
        }
        public void ResetHeaders(string token="")
        {
            Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
            Headers[HttpRequestHeader.Host] = "www.instagram.com";
            Headers["Accept"] = "*/*";
            Headers["Accept-Language"] = "en-US,en;q=0.5";
            Headers["Referer"] = "https://www.instagram.com/";
            SetToket(token);
            Headers["Content-Type"] = "application/x-www-form-urlencoded";
            Headers["X-Requested-With"] = "XMLHttpRequest";
            Headers["X-Instagram-AJAX"] = "1";
        }

        object start = new object();
        public string UploadString(string address)
        {
            try
            {
                lock (start)
                {
                    if (address.Any(wordByte => wordByte > 127) || address.Contains(' ')) return "";
                    return DownloadString(GetUri(address));
                }
            }catch(Exception ex)
            {

                if (ex.Message.Contains("404")) return "";// UploadString(address);
                throw ex;
            }
        }

        public  string UploadString(string address, string data)
        {
            return  UploadString(GetUri(address), data);
        }
        
        public string UploadString(string address, Dictionary<string, string> data)
        {
            lock (start)
            {
                var sb = new StringBuilder();
                var p = new List<string>();
                foreach (KeyValuePair<string, string> pair in data)
                {
                    sb.Clear();
                    sb.Append(pair.Key).Append("=").Append(pair.Value);
                    p.Add(sb.ToString());
                }
                var pp = string.Join("&", p);
                return UploadString(address, pp);
            }
        }

        private static Uri GetUri(string str)
        {
            var u = new Uri(str);
            var servicePoint = ServicePointManager.FindServicePoint(u);
            servicePoint.Expect100Continue = false;
            return u;
        }
    }
}
