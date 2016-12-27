using InstagramBot.Data;

namespace InstagramBot.Net
{
    public interface ActionPacket
    {
        Session Session { get; set; }
        void Serialize(User user, StateEventArgs e);
        void Deserialize(User user, StateEventArgs e);
    }
}
