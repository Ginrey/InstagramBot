#region

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

#endregion

namespace InstagramBot.IO
{
    public class Multithreading
    {
        readonly object addlock = new object();
        bool isWork;
        readonly int maxThreads;
        readonly Queue<Task> quequ = new Queue<Task>();

        public Multithreading(int countThread)
        {
            maxThreads = countThread;
        }

        public void Start()
        {
            isWork = true;
            for (int i = 0; i < maxThreads; i++)
                new Thread(CreateTimers).Start();
        }

        void CreateTimers()
        {
            List<Timer> timers = new List<Timer>();
            for (int i = 0; i < 10; i++)
            {
                Timer timer = new Timer(1);
                timer.Elapsed += Timer_Elapsed;
                timers.Add(timer);
            }
            foreach (var t in timers) t.Start();
        }

        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                Task task = Dequeue();
                task?.Start();
            }
            catch
            {
            }
        }

        public void Stop()
        {
            isWork = false;
        }

        public void Add(Task task)
        {
            lock (addlock)
                quequ.Enqueue(task);
        }

        Task Dequeue()
        {
            lock (addlock)
            {
                return quequ.Count > 0 ? quequ.Dequeue() : null;
            }
        }

        void OnReceived()
        {
            while (isWork)
            {
                try
                {
                    Task task = Dequeue();
                    task?.Start();
                }
                catch
                {
                }
            }
        }
    }
}