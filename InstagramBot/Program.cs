using System.Collections.Generic;
using System.Threading;
using InstagramBot.IO;
using InstagramBot.Net.Web;

namespace InstagramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            //310136073:AAHEP0i318aIkB8y3lAOTtwYxMf7jwtp51w - Hierarchy
            //326705762:AAHtag0VT3_wk0Hc1GCyrIyOL_OXeg-CAHQ - 110100
            Languages Language = new Languages();
            Session Session = new Session("326705762:AAHtag0VT3_wk0Hc1GCyrIyOL_OXeg-CAHQ")
            {
                ListWebInstagram = new List<WebInstagram> {
                    new WebInstagram("vitaliirogoza", "qwerty2"),
                    new WebInstagram("Instacc020", "36987412in", "5.188.209.33:3000", "mmGoRn:RdGbG1uN"),
                    new WebInstagram("Instacc021", "36987412in", "5.188.208.138:3000", "mmGoRn:RdGbG1uN"),
                    new WebInstagram("Instacc025", "36987412in", "91.243.63.55:3000", "mmGoRn:RdGbG1uN"),
                    new WebInstagram("Instacc032", "36987412in", "91.243.62.66:3000", "mmGoRn:RdGbG1uN"),
                    new WebInstagram("instacc039", "36987412in", "5.188.221.160:3000", "mmGoRn:RdGbG1uN"),
                    new WebInstagram("instacc038", "36987412in", "5.188.220.162:3000", "mmGoRn:RdGbG1uN"),
                }
            };
            Timer tmr = new Timer(restart);
            Session.Start();
            Thread.Sleep(Timeout.Infinite);
        }

       static void restart(object sender)
        {
           
        }
    }
}
