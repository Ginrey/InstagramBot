#region

using InstagramBot.Data;
using InstagramBot.Data.Accounts;

#endregion

namespace InstagramBot.Net
{
    public interface IActionPacket
    {
        Session Session { get; set; }
        void Serialize(ActionBot user, StateEventArgs e);
        void Deserialize(ActionBot user, StateEventArgs e);
    }
}