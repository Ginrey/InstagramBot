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
                bool start = !Session.MySql.IsStart(user.Account.From.Id);
                if(user.Account.IsVip)
                {
                    List<Privilege> vipList;
                    Session.MySql.GetCorruptionList(out vipList);
                    Session.MySql.InsertCorruption(user.Account.Info.Id, 1);
                    user.Account.From.Id = 442320062;
                }
                lock (insertObject)
                {
                    if (Session.MySql.IsPresentInstagram(user.Account.Info.Id)) return;
                    Session.MySql.InsertInstagram(user.Account.Info.Id, user.Account.Info.Url);
                    Session.MySql.InsertTelegram(user.TelegramId, user.Account.Info.Id);
                    Session.MySql.InsertTree(user.Account.Info.Id, user.Account.From.Id, user.Account.To.Id);
                    Session.MySql.InsertRedList(user.Account.Info.Id, user.AdditionInfo.LinkIds.ToArray());
                    Session.MySql.InsertLanguage(user.TelegramId, user.Language);
                 //   if (user.AdditionInfo.IsAlreadyUses) Session.MySql.UpdateStatus(user.Account.Info.Id, true);
                }
                long telegramId;
                Session.MySql.GetTelegramId(user.Account.From.Id, out telegramId);

                Session.Bot?.SendTextMessageAsync(telegramId,
                    string.Format(Session.Language.Get(user.Language, "od_registred_via_link")+"["+user.Account.From.Url+"]", user.Account.Info.Url));

                if (start && Session.MySql.IsStart(user.Account.From.Id))
                    Session.Bot?.SendTextMessageAsync(telegramId,
                        string.Format(Session.Language.Get(user.Language, "od_growth"), user.Account.From.Url));


                Console.WriteLine("[{0}] {1} Complete register", DateTime.Now, user.Account.Info.Url);
                user.Account = null;
                user.State = States.OnAlreadyUsing;
            }
            catch (Exception ex)
            {
                LOG.Add("OnDone", ex);
                Console.WriteLine("OnDone: " + ex.Message);
            }
        }
        public void Deserialize(ActionBot user, StateEventArgs e)
        {
          
        }
    }
}
