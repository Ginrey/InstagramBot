using System;
using InstagramBot.Data;

namespace InstagramBot.Net.Packets
{
    public class OnBlocked : ActionPacket
    {
        public Session Session { get; set; }
        public void Serialize(User user, StateEventArgs e)
        {
            Session.Bot?.SendTextMessageAsync(user.UID, "Вы заблокированы на этом сервисе");
        }
        public void Deserialize(User user, StateEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
