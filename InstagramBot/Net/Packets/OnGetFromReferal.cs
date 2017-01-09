using InstagramBot.Data;
using InstagramBot.Data.Accounts;

namespace InstagramBot.Net.Packets
{
    class OnGetFromReferal : ActionPacket
    {
        public Session Session { get; set; }
        public void Serialize(ActionBot user, StateEventArgs e)
        {
            if(string.IsNullOrEmpty(user.FromReferal))
            Session.Bot?.SendTextMessageAsync(user.TelegramID, "Ввидите ник того, кто вас пригласил. У вас есть 3 попытки.");
            else
            {
                Session.Bot?.SendTextMessageAsync(user.TelegramID, "Вы пришли по ссылке ["+user.FromReferal+"]. Если он не подойдет, введите другую ссылку");
                e.Message = new Telegram.Bot.Types.Message {Text = user.FromReferal};
                Deserialize(user, e);
            }
        }
        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            user.Account.FromReferal = e.Message.Text.Replace("@","");
            if (user.ErrorCounter == 3) user.Account.Referal = "100lbov";
            var acc = Session.WebInstagram.GetAccount(user.Account.FromReferal);
            if (acc == null) goto error;
            user.Account.FromReferalId = acc.Uid;
            if (Session.MySql.IsPresentLicense(user.Account.FromReferalId))
            {
                user.State++;
                return;
            }
            error:
            user.ErrorCounter++;
            Session.Bot?.SendTextMessageAsync(user.TelegramID,
                "Пользователь не зарегистрирован!\n[Попыток осталось: " + (3-user.ErrorCounter)+"]");
        }
    }
}
