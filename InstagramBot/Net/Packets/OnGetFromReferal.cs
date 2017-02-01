using System;
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
                user.AdditionInfo.ErrorCounter = 0;
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
            catch (Exception ex)
            { }
        }
       
        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                user.Account.From.URL = e.Message.Text.Replace("@", "");
                if (user.AdditionInfo.ErrorCounter >= 3)
                {
                    user.Account.From = new MiniInfo(442320062, "100lbov");
                    user.State = States.WaitSubscribe;
                }
                var acc = Session.WebInstagram.GetAccount(user.Account.From.URL);
                if (acc == null) goto error;
                user.Account.From.ID = acc.Uid;
              
                if (Session.MySql.IsPresentInstagram(user.Account.From.ID))
                {
                    user.State = States.WaitSubscribe;
                    return;
                }
                error:
                user.AdditionInfo.ErrorCounter++;
                Session.Bot?.SendTextMessageAsync(user.TelegramId,
                            string.Format(Session.Language.Get(user.Language, "ofgr_user_not_registred"), 3 - user.AdditionInfo.ErrorCounter));
           }
            catch (Exception ex)
            { }
        }
    }
}
