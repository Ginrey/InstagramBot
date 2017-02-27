#region

using System;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;
using InstagramBot.IO;
using Telegram.Bot.Types;

#endregion

namespace InstagramBot.Net.Packets
{
    internal class OnGetFromReferal : IActionPacket
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
                        string.Format(Session.Language.Get(user.Language, "ofgr_come_to_link"),
                            user.AdditionInfo.FromReferal));
                    e.Message = new Message {Text = user.AdditionInfo.FromReferal};
                    Deserialize(user, e);
                }
            }
            catch (Exception ex)
            {
                LOG.Add("OGFS", ex);
            }
        }

        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                user.Account.From.Set(e.Message.Text.Replace("@", ""));
                if (user.AdditionInfo.ErrorCounter >= 3)
                {
                    user.Account.From = new MiniInfo(442320062, "100lbov");
                    user.State = States.WaitSubscribe;
                }
                var acc = Session.WebInstagram.GetAccount(user.Account.From.Url);
                if (acc == null) goto error;
                user.Account.From.Set(acc.Info.Id);

                if (Session.MySql.IsPresentInstagram(user.Account.From.Id))
                {
                    user.State = States.WaitSubscribe;
                    return;
                }
                error:
                user.AdditionInfo.ErrorCounter++;
                Session.Bot?.SendTextMessageAsync(user.TelegramId,
                    string.Format(Session.Language.Get(user.Language, "ofgr_user_not_registred"),
                        3 - user.AdditionInfo.ErrorCounter));
            }
            catch (Exception ex)
            {
                LOG.Add("OGFD", ex);
            }
        }
    }
}