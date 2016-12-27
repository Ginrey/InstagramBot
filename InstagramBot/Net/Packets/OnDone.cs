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
            Session.Bot?.SendTextMessageAsync(user.UID, "Спасибо за то, что пользуетесь нашим сервисом. Регистрация прошла успешно");
        }
        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
