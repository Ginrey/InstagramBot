using InstagramBot.Data;
using InstagramBot.Data.Accounts;

namespace InstagramBot.Net.Packets
{
    public class OnRegistering : ActionPacket
    {
        public Session Session { get; set; }
        public void Serialize(ActionBot user, StateEventArgs e)
        {
            Session.Bot?.SendTextMessageAsync(user.TelegramID, "Добро пожаловать!\nПожалуйста введите свой ник из Instagram");
        }
        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            if (e.Message.Text.Contains("/")) return;
            user.Account = Session.WebInstagram.GetAccount(e.Message.Text);
            Session.Bot?.SendTextMessageAsync(user.TelegramID, "Это ваш аккаунт?\n"+user.Account.Name);
        }
    }
}
