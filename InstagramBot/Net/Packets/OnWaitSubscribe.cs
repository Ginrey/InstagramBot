using System;
using InstagramBot.Data;

namespace InstagramBot.Net.Packets
{
   public class OnWaitSubscribe : ActionPacket
    {
        public Session Session { get; set; }
        public void Serialize(User user, StateEventArgs e)
        {
            Session.Bot?.SendTextMessageAsync(user.UID, "ждем как подпишется");
        }
        public void Deserialize(User user, StateEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
