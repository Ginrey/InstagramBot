using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InstagramBot.IO
{
   public class Multithreading
   {
       Queue<Task> quequ = new Queue<Task>();
       object addlock = new object();
       int maxThreads;
       bool isWork;

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
            List<System.Timers.Timer> timers = new List<System.Timers.Timer>();
            for(int i = 0; i < 10; i++)
            {
                System.Timers.Timer timer = new System.Timers.Timer(1);
                timer.Elapsed += Timer_Elapsed;
                timers.Add(timer);
            }
            foreach (var t in timers) t.Start();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
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
