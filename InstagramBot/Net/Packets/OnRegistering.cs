using System;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;

namespace InstagramBot.Net.Packets
{
    public class OnRegistering : IActionPacket
    {
        public Session Session { get; set; }

        public void Serialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                Session.Bot?.SendTextMessageAsync(user.TelegramId,
                    string.Format(Session.Language.Get(user.Language, "or_to_start")));

                Console.WriteLine("[{0}] {1} Starting register", DateTime.Now, user.TelegramId);
            }
            catch (Exception ex)
            {
            }
        }
        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                if (e.Message.Text.Contains("/")) return;
                user.Account =  Session.WebInstagram.GetAccount(e.Message.Text.Replace("@", ""));
                if (user.Account == null)
                {
                    Session.Bot?.SendTextMessageAsync(user.TelegramId,
                    string.Format(Session.Language.Get(user.Language, "or_account_not_exist")));
                    return;
                }
                user.State = States.WaitUrl;
            }
            catch { }
        }
    }
}
