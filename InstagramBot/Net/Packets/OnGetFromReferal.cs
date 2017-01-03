using InstagramBot.Data;
using InstagramBot.Data.Accounts;

namespace InstagramBot.Net.Packets
{
    class OnGetFromReferal : ActionPacket
    {
        public Session Session { get; set; }
        public void Serialize(ActionBot user, StateEventArgs e)
        {
            Session.Bot?.SendTextMessageAsync(user.TelegramID, "Пожалуйста введите ник того кто вас пригласил");
        }
        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            if (e.Message.Text.Contains("/")) return;
            user.Account.FromReferal = e.Message.Text;
            user.Account.FromReferalId = Session.WebInstagram.GetAccount(e.Message.Text).Uid;
            if (Session.MySql.IsPresentLicense(user.Account.FromReferalId))
                user.State++;
            else
                Session.Bot?.SendTextMessageAsync(user.TelegramID, "Такой пользователь не зарегистрирован. Повторите попытку");
        }
    }
}
