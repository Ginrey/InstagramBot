using System.Collections.Generic;

namespace InstagramBot.Data.Accounts
{
   public class AdditionInfo
    {
        public string TempLink { get; set; }
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
            if (ListForLink.Count >= 9) return;
            if (!ListForLink.ContainsKey(info.URL))
            {
                ListForLink.Add(info.URL, false);
                LinkIds.Add(info.ID);
            }
        }
    }
}
