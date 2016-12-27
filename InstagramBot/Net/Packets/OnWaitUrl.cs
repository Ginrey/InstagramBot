using System;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;

namespace InstagramBot.Net.Packets
{
    public class OnWaitUrl : ActionPacket
    {
        public Session Session { get; set; }
        public void Serialize(ActionBot user, StateEventArgs e)
        {
            Session.Bot?.SendTextMessageAsync(user.UID, "Спасибо, вы внесены в базу!\nДля начала Вам необходимо подписаться на этих людей");
        }
        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
