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
            this.session = session;
        }
        void Wait()
        {
            if (Task != null && Task.Status != TaskStatus.RanToCompletion)
            {
                Task.Wait();
                Task.Dispose();
            }
        }
        public void SetState(States state)
        {
            Wait();
            Task = new Task(() => { State = state; });
            Task.Start();
           // Wait();
        }

        public void NextStep(Message message)
        {
            Wait();
            Task = new Task(() =>
            {
                session.Connection.PacketsRegistry.GetPacketType(State)
                    .Deserialize(this, new StateEventArgs(message, State));
            });
            Task.Start();
           // Wait();
        }
    }
}
