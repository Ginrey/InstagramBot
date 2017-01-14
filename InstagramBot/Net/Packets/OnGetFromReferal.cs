using InstagramBot.Data;
using InstagramBot.Data.Accounts;

namespace InstagramBot.Net.Packets
{
    class OnGetFromReferal : IActionPacket
    {
        public Session Session { get; set; }
        public void Serialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(user.AdditionInfo.FromReferal))
                    Session.Bot?.SendTextMessageAsync(user.TelegramId,
                        "Ввидите ник того, кто вас пригласил. У вас есть 3 попытки.");
                else
                {
                    Session.Bot?.SendTextMessageAsync(user.TelegramId,
                        "Вы пришли по ссылке [" + user.AdditionInfo.FromReferal + "].");
                    e.Message = new Telegram.Bot.Types.Message {Text = user.AdditionInfo.FromReferal};
                    Deserialize(user, e);
                }
            }
            catch { }
        }

        public async void Deserialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                user.Account.FromReferal = e.Message.Text.Replace("@", "");
                if (user.AdditionInfo.ErrorCounter == 3) user.Account.Referal = "100lbov";
                var acc = Session.WebInstagram.GetAccount(user.Account.FromReferal);
                if (acc == null) goto error;
                user.Account.FromReferalId = acc.Uid;
                if (Session.MySql.IsPresentLicense(user.Account.FromReferalId))
                {
                    user.SetState(States.WaitSubscribe);
                    return;
                }
                error:
                user.AdditionInfo.ErrorCounter++;
                await Session.Bot?.SendTextMessageAsync(user.TelegramId,
                    "Пользователь не зарегистрирован!\n[Попыток осталось: " + (3 - user.AdditionInfo.ErrorCounter) + "]");
            }
            catch { }
        }
    }
}
