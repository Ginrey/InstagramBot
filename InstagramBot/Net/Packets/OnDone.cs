using System;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;

namespace InstagramBot.Net.Packets
{
   public class OnDone : ActionPacket
    {
        public Session Session { get; set; }
        public void Serialize(ActionBot user, StateEventArgs e)
        {
            Session.Bot?.SendTextMessageAsync(user.TelegramID, "Спасибо за то, что пользуетесь нашим сервисом. Регистрация прошла успешно");
            Session.MySql.InsertNewAccount(user.Account.Uid, user.Account.Referal, user.Account.FromReferalId,States.Done);
        }
        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            
        }
    }
}
