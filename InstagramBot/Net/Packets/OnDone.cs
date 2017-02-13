using System;
using System.Collections.Generic;
using System.Linq;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;
using InstagramBot.IO;

namespace InstagramBot.Net.Packets
{
    public class OnDone : IActionPacket
    {
        public Session Session { get; set; }
        object insertObject = new object();
        public void Serialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                Session.Bot?.SendTextMessageAsync(user.TelegramId,
                    string.Format(Session.Language.Get(user.Language, "od_start")));
                bool start = !Session.MySql.IsStart(user.Account.From.ID);
                if(user.Account.IsVip)
                {
                    List<Privilege> vipList;
                    Session.MySql.GetCorruptionList(out vipList);
                    Session.MySql.InsertCorruption(user.Account.Id, 1);
                    user.Account.From.ID = 442320062;
                }
                lock (insertObject)
                {
                    if (Session.MySql.IsPresentInstagram(user.Account.Id)) return;
                    Session.MySql.InsertInstagram(user.Account.Id, user.Account.URL);
                    Session.MySql.InsertTelegram(user.TelegramId, user.Account.Id);
                    Session.MySql.InsertTree(user.Account.Id, user.Account.From.ID, user.Account.To.ID);
                    Session.MySql.InsertRedList(user.Account.Id, user.AdditionInfo.LinkIds.ToArray());
                    Session.MySql.InsertLanguage(user.TelegramId, user.Language);
                }
                long telegramId;
                Session.MySql.GetTelegramId(user.Account.From.ID, out telegramId);

                Session.Bot?.SendTextMessageAsync(telegramId,
                    string.Format(Session.Language.Get(user.Language, "od_registred_via_link"), user.Account.URL));

                if (start && Session.MySql.IsStart(user.Account.From.ID))
                    Session.Bot?.SendTextMessageAsync(telegramId,
                        string.Format(Session.Language.Get(user.Language, "od_growth"), user.Account.From.URL));


                Console.WriteLine("[{0}] {1} Complete register", DateTime.Now, user.Account.URL);
                user.Account = null;
                user.State = States.OnAlreadyUsing;
            }
            catch (Exception ex)
            {
                LOG.Add("OnDone", ex.Message);
                Console.WriteLine("OnDone: " + ex.Message);
            }
        }
        public void Deserialize(ActionBot user, StateEventArgs e)
        {
          
        }
    }
}
