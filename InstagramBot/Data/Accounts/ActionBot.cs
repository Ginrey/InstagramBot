using System;
using System.Threading.Tasks;
using InstagramBot.IO;
using Telegram.Bot.Types;

namespace InstagramBot.Data.Accounts
{
    public class ActionBot
    {
        public AccountInstagram Account { get; set; }
        public Message Message { get; set; }
        public AdditionInfo AdditionInfo { get; set; }
        public Language Language { get; set; }
        public long TelegramId { get; set; }
        Session session;
        States _state;
        Task Task;
        DateTime lastMessageTime;
        public  States State
        {
            get { return _state; }
             set
            {
                _state = value;
                    session.Connection.PacketsRegistry.GetPacketType(value)
                        .Serialize(this, new StateEventArgs(Message, value));
            }
        }
        public  ActionBot(long tid, Session session, string fromreferal = "")
        {
            AdditionInfo = new AdditionInfo
            {
                FromReferal = fromreferal
            };
            TelegramId = tid;
            lastMessageTime = DateTime.Now;
            this.session = session;
        }
        bool Wait()
        {
            if (lastMessageTime.Subtract(DateTime.Now) > TimeSpan.Zero) return false;
            lastMessageTime = DateTime.Now + TimeSpan.FromSeconds(1);
            return true;
        }
        public void SetState(States state)
        {
            if (!Wait()) return;
            Task = new Task(() => { State = state; });
            session.Multithreading.Add(Task);
        }

        public void NextStep(Message message)
        {
            if (!Wait()) return;
            Task = new Task(() =>
            {
                session.Connection.PacketsRegistry.GetPacketType(State)
                    .Deserialize(this, new StateEventArgs(message, State));
            });
            session.Multithreading.Add(Task);
        }
    }
}
