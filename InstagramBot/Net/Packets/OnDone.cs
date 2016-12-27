using System;
using InstagramBot.Data;

namespace InstagramBot.Net.Packets
{
   public class OnDone : ActionPacket
    {
        public Session Session { get; set; }
        public void Serialize(User user, StateEventArgs e)
        {
            Session.Bot?.SendTextMessageAsync(user.UID, "Спасибо за то, что пользуетесь нашим сервисом. Регистрация прошла успешно");
        }
        public void Deserialize(User user, StateEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
