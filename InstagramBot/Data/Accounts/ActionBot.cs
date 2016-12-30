using Telegram.Bot.Types;

namespace InstagramBot.Data.Accounts
{
    public class ActionBot
    {
        public int TelegramID { get; set; }
        public AccountInstagram Account { get; set; }
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
        public ActionBot(int tid, Session session, States state = States.Registering)
        {
            TelegramID = tid;
            this.session = session;
            State = state;
        }

        public void NextStep(Message message)
        {
            session.Connection.PacketsRegistry.GetPacketType(State)
                                   .Deserialize(this, new StateEventArgs(message, State));
        }
    }
}
