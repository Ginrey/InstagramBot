using InstagramBot.Data;
using InstagramBot.Data.Accounts;

namespace InstagramBot.Net.Packets
{
    class OnGetFromReferal : ActionPacket
    {
        public Session Session { get; set; }
        public void Serialize(ActionBot user, StateEventArgs e)
        {
            Session.Bot?.SendTextMessageAsync(user.TelegramID, "Пожалуйста введите ник того кто вас пригласил или отправьте команду " +
                                                               "/Unknown если такого человека нет");
        }
        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            user.Account.FromReferal = e.Message.Text.StartsWith("/Unknown") ? "skew77" : e.Message.Text;
            user.Account.FromReferalId = Session.WebInstagram.GetAccount(user.Account.FromReferal).Uid;
            if (Session.MySql.IsPresentLicense(user.Account.FromReferalId))
                user.State++;
            else
                Session.Bot?.SendTextMessageAsync(user.TelegramID, "Такой пользователь не зарегистрирован. Повторите попытку");
        }
    }
}
