using System;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;

namespace InstagramBot.Net.Packets
{
    public class OnDone : IActionPacket
    {
        public Session Session { get; set; }

        public void Serialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                Session.Bot?.SendTextMessageAsync(user.TelegramId,
                    string.Format(Session.Language.Get(user.Language, "od_start")));
                bool start = false;
                if (Session.MySql.IsStart(user.Account.From.ID)) start = false;
                Session.MySql.InsertInstagram(user.Account.Uid, user.Account.Referal);
                Session.MySql.InsertTelegram(user.Account.Uid, user.TelegramId);
                Session.MySql.InsertTree(user.Account.Uid, user.Account.From.ID, user.Account.To.ID);
                Session.MySql.InsertRedList(user.Account.Uid, user.AdditionInfo.LinkIds.ToArray());
                Session.MySql.InsertLanguage(user.Account.Uid, user.Language);

                long telegramId;
                Session.MySql.GetTelegramId(user.Account.From.ID, out telegramId);

                Session.Bot?.SendTextMessageAsync(telegramId,
                    string.Format(Session.Language.Get(user.Language, "od_registred_via_link"), user.Account.Referal));

                if (start && Session.MySql.IsStart(user.Account.From.ID))
                    Session.Bot?.SendTextMessageAsync(telegramId,
                        string.Format(Session.Language.Get(user.Language, "od_growth"), user.Account.From.URL));


                Console.WriteLine("[{0}] {1} Complete register", DateTime.Now, user.Account.Referal);
                user.Account = null;
                user.State = States.OnAlreadyUsing;
            }
            catch (Exception ex)
            {
            }
        }
        public void Deserialize(ActionBot user, StateEventArgs e)
        {
          
        }
    }
}
