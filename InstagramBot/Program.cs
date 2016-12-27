using System.Threading;
using InstagramBot.Parsing;

namespace InstagramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            WebInstagram instagram = new WebInstagram("vitaliirogoza", "qwerty2");
            Session Session = new Session("310136073:AAHEP0i318aIkB8y3lAOTtwYxMf7jwtp51w");
            Session.Start();
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
