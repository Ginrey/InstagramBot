using System.Threading;
using InstagramBot.Net.Web;

namespace InstagramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Session Session = new Session("310136073:AAHEP0i318aIkB8y3lAOTtwYxMf7jwtp51w")
            {
                WebInstagram = new WebInstagram("vitaliirogoza", "qwerty2")
            };
            Session.Start();
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
