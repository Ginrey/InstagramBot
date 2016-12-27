using Telegram.Bot.Types;

namespace InstagramBot.Data.Accounts
{
    public class ActionBot
    {
        public int UID { get; set; }
        public string InstagramNick { get; set; }
        public Message Message { get; set; }
        Session session;
        States _state;
        public States State
        {
            get { return _state; }
            set
            {
                _state = value;
                session.Connection.PacketsRegistry.GetPacketType(value)
                    .Serialize(this, new StateEventArgs(Message, value));
            }
        }
        public ActionBot(int uid, Session session, States state = States.Registering)
        {
            this.session = session;
            UID = uid;
            State = state;
        }

        public void NextStep(Message message)
        {
            session.Connection.PacketsRegistry.GetPacketType(State)
                                   .Deserialize(this, new StateEventArgs(message, State));
        }
    }
}
