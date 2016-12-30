using System;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;

namespace InstagramBot.Net.Packets
{
    public class OnBlocked : ActionPacket
    {
        public Session Session { get; set; }
        public void Serialize(ActionBot user, StateEventArgs e)
        {
            Session.Bot?.SendTextMessageAsync(user.TelegramID, "Вы заблокированы на этом сервисе");
        }
        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
