using System.Collections.Generic;
using System.Linq;

namespace InstagramBot.Data.Accounts
{
   public class AdditionInfo
    {
        public int ErrorCounter { get; set; }
        public List<string> Uses = new List<string>();
        public bool Complete { get; set; }
        public bool Full => ListForLink.Count >= 9;
        public List<long> LinkIds { get; set; } = new List<long>();
        public string FromReferal { get; set; }
        public Dictionary<string, bool> ListForLink = new Dictionary<string, bool>();
        public void AddLink(MiniInfo info)
        {
            info.Set(info.URL.ToLower());
            if (ListForLink.Count >= 9)
            {
                int index = 0;
                Dictionary<string, bool> tempdict = ListForLink.TakeWhile(item => ++index < 9).ToDictionary(item => item.Key, item => item.Value);
                ListForLink = tempdict;
                return;
            }
            if (!ListForLink.ContainsKey(info.URL))
            {
                ListForLink.Add(info.URL, false);
                LinkIds.Add(info.ID);
            }
        }
    }
}
