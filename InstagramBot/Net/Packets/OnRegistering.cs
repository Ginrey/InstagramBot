using System;

using InstagramBot.Data;

namespace InstagramBot.Net.Packets
{
    public class OnRegistering : ActionPacket
    {
        public Session Session { get; set; }
        public void Serialize(User user, StateEventArgs e)
        {
            Session.Bot?.SendTextMessageAsync(user.UID, "Добро пожаловать!\nПожалуйста введите свой ник из Instagram");
        }
        public void Deserialize(User user, StateEventArgs e)
        {
            if (e.Message.Text.Contains("/")) return;
            user.InstagramNick = e.Message.Text;


        }
    }
}
