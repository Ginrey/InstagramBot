#region

using System.Collections.Generic;
using System.Linq;

#endregion

namespace InstagramBot.Data.Accounts
{
    public class AdditionInfo
    {
        public Dictionary<string, bool> ListForLink = new Dictionary<string, bool>();
        public List<string> Uses = new List<string>();
        public int ErrorCounter { get; set; }
        public bool IsAlreadyUses { get; set; }
        public bool Complete { get; set; }
        public bool Full => ListForLink.Count >= 9;
        public List<long> LinkIds { get; set; } = new List<long>();
        public string FromReferal { get; set; }

        public void AddLink(MiniInfo info)
        {
            info.Set(info.Url.ToLower());
            if (ListForLink.Count >= 9)
            {
                int index = 0;
                Dictionary<string, bool> tempdict =
                    ListForLink.TakeWhile(item => ++index < 9).ToDictionary(item => item.Key, item => item.Value);
                ListForLink = tempdict;
                return;
            }
            if (!ListForLink.ContainsKey(info.Url))
            {
                ListForLink.Add(info.Url, false);
                LinkIds.Add(info.Id);
            }
        }
    }
}