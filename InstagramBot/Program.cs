using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using InstagramBot.Net.Web;

namespace InstagramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            //310136073:AAHEP0i318aIkB8y3lAOTtwYxMf7jwtp51w - Hierarchy
            //326705762:AAHtag0VT3_wk0Hc1GCyrIyOL_OXeg-CAHQ - 110100
            string connectionString = "326705762:AAHtag0VT3_wk0Hc1GCyrIyOL_OXeg-CAHQ";
#if DEBUG
            connectionString = "310136073:AAHEP0i318aIkB8y3lAOTtwYxMf7jwtp51w";
#endif
            Session Session = new Session(connectionString)
            {
                ListWebInstagram = new List<WebInstagram> {
                    new WebInstagram("Instacc020", "36987412in", "146.185.208.35:3000", "mmGoRn:RdGbG1uN"),
                    new WebInstagram("Instacc021", "36987412in", "146.185.208.46:3000", "mmGoRn:RdGbG1uN"),
                    new WebInstagram("Instacc025", "36987412in", "146.185.209.184:3000", "mmGoRn:RdGbG1uN"),
                 //   new WebInstagram("Instacc032", "36987412in", "146.185.209.216:3000", "mmGoRn:RdGbG1uN"),
                    new WebInstagram("Instacc038", "36987412in", "91.243.62.218:3000", "mmGoRn:RdGbG1uN"),
                    new WebInstagram("Instacc039", "36987412in", "5.188.230.49:3000", "mmGoRn:RdGbG1uN")
                }
            };
            Session.Start();
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
