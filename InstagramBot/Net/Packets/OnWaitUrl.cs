using InstagramBot.Data;
using InstagramBot.Data.Accounts;

namespace InstagramBot.Net.Packets
{
    public class OnWaitUrl : ActionPacket
    {
        public Session Session { get; set; }
        public void Serialize(ActionBot user, StateEventArgs e)
        {
            Session.Bot?.SendTextMessageAsync(user.TelegramID, "Это ваш аккаунт?\n" + user.Account.Name);
        }
        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            if (e.Message.Text.ToLower() == "да")
            {
                user.State++;
            }
            else
            {
                user.State = States.Registering;
            }
        }
    }
}
