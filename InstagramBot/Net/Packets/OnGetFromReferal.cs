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
                            string.Format(Session.Language.Get(user.Language, "ogfr_enter_inviter")));
                else
                {
                    Session.Bot?.SendTextMessageAsync(user.TelegramId,
                            string.Format(Session.Language.Get(user.Language, "ofgr_come_to_link"), user.AdditionInfo.FromReferal));
                    e.Message = new Telegram.Bot.Types.Message {Text = user.AdditionInfo.FromReferal};
                    Deserialize(user, e);
                }
            }
            catch { }
        }

        public void Deserialize(ActionBot user, StateEventArgs e)
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
                    user.State = States.WaitSubscribe;
                    return;
                }
                error:
                user.AdditionInfo.ErrorCounter++;
                Session.Bot?.SendTextMessageAsync(user.TelegramId,
                            string.Format(Session.Language.Get(user.Language, "ofgr_user_not_registred"), 3 - user.AdditionInfo.ErrorCounter));
           }
            catch { }
        }
    }
}
