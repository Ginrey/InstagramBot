#region

using InstagramBot.Data;
using InstagramBot.Data.Accounts;
using InstagramBot.IO;

#endregion

namespace InstagramBot.Net.Packets
{
    public class OnBlocked : IActionPacket
    {
        public Session Session { get; set; }

        public void Serialize(ActionBot user, StateEventArgs e)
        {
        }

        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            Session.Bot?.SendTextMessageAsync(user.TelegramId,
                string.Format(Session.Language.Get(user.Language, "ob_banned")));
            Menu.ShowMyRedList(user);
        }
    }
}