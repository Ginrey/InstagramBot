using System;
using InstagramBot.Data;

namespace InstagramBot.Net.Packets
{
    public class OnWaitUrl : ActionPacket
    {
        public Session Session { get; set; }
        public void Serialize(User user, StateEventArgs e)
        {
            Session.Bot?.SendTextMessageAsync(user.UID, "Спасибо, вы внесены в базу!\nДля начала Вам необходимо подписаться на этих людей");
        }
        public void Deserialize(User user, StateEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
