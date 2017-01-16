using System.Collections.Generic;
using System.Threading;

namespace InstagramBot.Data
{
    public class BlackList
    {
        Dictionary<long, ActionTimer> List = new Dictionary<long, ActionTimer>();

        public void Add(long state)
        {
            lock (List)
            {
                if (!List.ContainsKey(state))
                {
                    ActionTimer tmr = new ActionTimer(Delete, state);
                    List.Add(state, tmr);
                    tmr.Start(3500, -1);
                }
            }
        }

        public bool Contains(long id)
        {
            lock (List)
            {
                return List.ContainsKey(id);
            }
        }

        void Delete(object state)
        {
            lock (List)
            {
                if (List.ContainsKey((long) state))
                {
                    List[(long) state].Stop();
                    List.Remove((long) state);
                }
            }
        }
    }
}
    public class ActionTimer
    {
        Timer timer;
        public ActionTimer(TimerCallback callBack, object state)
        {
            timer = new Timer(callBack, state, Timeout.Infinite, Timeout.Infinite);
        }

        public void Start(int dueTime, int period)
        {
            timer.Change(dueTime, period);
        }
        public void Stop()
        {
            timer.Change(Timeout.Infinite, Timeout.Infinite);
        }
    }

