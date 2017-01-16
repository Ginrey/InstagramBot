using System;
using System.Collections.Generic;
using System.Linq;
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

                Session.MySql.InsertNewAccount(user.Account.Uid, user.Account.Referal, user.TelegramId,
                    user.Account.ToReferalId, States.OnAlreadyUsing, DateTime.Now);

                Session.Bot?.SendTextMessageAsync(user.TelegramId,
                     string.Format(Session.Language.Get(user.Language, "od_invite")));

                int count;
                Session.MySql.GetCountFollows(user.Account.FromReferalId, out count);
                long telegramId;
                Session.MySql.GetTelegramId(user.Account.FromReferalId, out telegramId);

                Session.Bot?.SendTextMessageAsync(user.TelegramId,
                    string.Format(Session.Language.Get(user.Language, "od_registred_via_link"), user.Account.Referal));
               
                if (count == 1)
                {
                    Session.MySql.UpdateStatus(user.Account.FromReferalId, true);
                    if (telegramId != 0)
                        Session.Bot?.SendTextMessageAsync(user.TelegramId,
                            string.Format(Session.Language.Get(user.Language, "od_growth")));
                }

                Session.MySql.UpdateCountFollows(user.Account.FromReferalId, count + 1);
                List<string> redlist = user.AdditionInfo.ListForLink.Keys.ToList();
                Session.MySql.InsertRedList(user.Account.Uid, redlist.ToArray());
                Console.WriteLine("[{0}] {1} Complete register", DateTime.Now, user.Account.Referal);
                user.Account = null;
                user.State = States.OnAlreadyUsing;
            }
            catch { }
        }
        public void Deserialize(ActionBot user, StateEventArgs e)
        {
          
        }
    }
}
