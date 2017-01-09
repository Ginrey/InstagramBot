using System.Threading;
using InstagramBot.Net.Web;

namespace InstagramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Session Session = new Session("326705762:AAHtag0VT3_wk0Hc1GCyrIyOL_OXeg-CAHQ")
            {
                WebInstagram = new WebInstagram("vitaliirogoza", "qwerty2")
            };
            Session.Start();
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
