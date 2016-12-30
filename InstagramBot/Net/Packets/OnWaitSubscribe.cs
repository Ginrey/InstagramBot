using System;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;

namespace InstagramBot.Net.Packets
{
   public class OnWaitSubscribe : ActionPacket
    {
        public Session Session { get; set; }
        public void Serialize(ActionBot user, StateEventArgs e)
        {
            Session.Bot?.SendTextMessageAsync(user.TelegramID, "Чтобы начать получать своих подписчиков Вам необходимо подписаться на следующих людей");
        }
        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
