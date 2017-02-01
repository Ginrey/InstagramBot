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
               new Thread(OnReceived).Start();
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
