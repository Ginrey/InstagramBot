using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;

namespace InstagramBot.Data.Accounts
{
   public static class Refresher
   {
       public static Session Session { get; set; }
        static TimeSpan dateStart;
        static Timer timerRefresh = new Timer(1000);
        static Refresher()
        {
            timerRefresh = new Timer(1000);
            dateStart = DateTime.Now.Date.AddDays(1) - DateTime.Now;
            timerRefresh.Elapsed += TimerRefresh_Elapsed;
            timerRefresh.Start();
        }
        private static void TimerRefresh_Elapsed(object sender, ElapsedEventArgs e)
        {
            dateStart -= TimeSpan.FromSeconds(1);
            if (dateStart.TotalSeconds <= 0)
            {
                if (File.Exists("RefreshDone"))
                {
                    dateStart = DateTime.Now.Date.AddDays(1) - DateTime.Now;
                    Refresh();

                    File.Delete("RefreshDone");
                }
            }
        }
        public static void Refresh()
        {
            List<Privilege> list;
            Session.MySql.GetCorruptionList(out list);
            Bloggers.Set(list);
            Session.MySql.GetCorruptionTimeList(out list);
            Corruption.Set(list);
            Console.WriteLine("Refresh complete");
        }
    }
   public static class Corruption
   {
      static Queue<Privilege> privilegeList;
        static Queue<Privilege> privilegeListCopy;
        public static void Set(List<Privilege> list)
        {
            lock (lockObject)
            {
                privilegeList = new Queue<Privilege>(list.ToArray());
                privilegeListCopy = new Queue<Privilege>(list.ToArray());
            }
        }

       static object lockObject = new object();
        public static Privilege Next()
        {
            lock (lockObject)
            {
                foreach (var item in privilegeListCopy)
                {
                    var queue = privilegeList.Dequeue();
                    privilegeList.Enqueue(queue);
                    if (queue.Balance > 0)
                    {
                        queue.Balance--;
                        Refresher.Session?.MySql.UpdateCountCorruptionTime(queue.ID, queue.Balance);
                        return queue;
                    }
                }
                return new Privilege(1647550018, "skew77");
            }
        }

        public static void Add(Privilege user, int count)
        {
            lock (lockObject)
            {
                user.Balance = count;
                Refresher.Session?.MySql.InsertCorruptionTime(user.ID, count);
                privilegeList.Enqueue(user);
                privilegeListCopy.Enqueue(user);
            }
        }

        static void Reset()
        {
            double arraySum = privilegeList.Sum(item => item.Coefficient);
            foreach(var item in privilegeList)
            {
                item.Balance = (int)(100 / arraySum * item.Coefficient);
            }
        }
    }

    public static class Bloggers 
    {
        static Queue<Privilege> privilegeList;
        public static Queue<Privilege> privilegeListCopy;
        public static void Set(List<Privilege> list)
        {
            lock (lockObject)
            {
                privilegeList = new Queue<Privilege>(list.ToArray());
                privilegeListCopy = new Queue<Privilege>(list.ToArray());
            }
            Reset();
        }

        public static bool Contains(long id)
        {
            return privilegeListCopy.Any(item => item.ID == id);
        }

        static object lockObject = new object();
        public static Privilege Next()
        {
            lock (lockObject)
            {
                foreach (var item in privilegeListCopy)
                {
                    var queue = privilegeList.Dequeue();
                    privilegeList.Enqueue(queue);
                    if (queue.Balance > 0)
                    {
                        queue.Balance--;
                        return queue;
                    }
                }
                Reset();
                return Next();
            }
        }

        static void Reset()
        {
            double arraySum = privilegeListCopy.Sum(item => item.Coefficient);
            foreach (var item in privilegeList)
            {
                item.Balance = (int)(100 / arraySum * item.Coefficient);
                item.Balance = item.Balance == 0 ? 1 : item.Balance;
            }
        }
    }

    public class Privilege : MiniInfo
    {
        public int Balance { get; set; }
        public int BonusPlace { get; set; }
        public double Coefficient { get; set; }
        public Privilege(long id = -1, string url = "", double coefficient = 0, object bonus = null) : base(id, url)
        {
            Coefficient = coefficient;
            int b;
            BonusPlace = bonus != null && int.TryParse(bonus.ToString(), out b) ? b : 0;
        }
    }
    public class BloggerStatistics : Privilege
    {
        public DateTime Date { get; set; }
        public BloggerStatistics(long id = -1, string url = "", double coefficient = 0, DateTime date = new DateTime()) : base(id, url, coefficient)
        {
            Date = date;
        }
    }
}
