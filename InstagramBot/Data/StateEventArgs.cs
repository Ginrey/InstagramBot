using System;
using Telegram.Bot.Types;

namespace InstagramBot.Data
{
    public class StateEventArgs: EventArgs
    {
        public StateEventArgs(Message message, States state)
        {
            Message = message;
            State = state;
        }
        public Message Message { get; private set; }
        public States State { get; private set; }
    }
}
