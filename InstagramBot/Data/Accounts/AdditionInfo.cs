using System.Collections.Generic;

namespace InstagramBot.Data.Accounts
{
   public class AdditionInfo
    {
        public string TempLink { get; set; }
        public int ErrorCounter { get; set; }
        public List<string> Uses = new List<string>();
        public bool Complete { get; set; }
        public List<StructInfo> StructsInfo { get; set; }
        public string FromReferal { get; set; }
        public Dictionary<string, bool> ListForLink = new Dictionary<string, bool>();
        public void AddLink(string text)
        {
            text = text.ToLower();
            if (ListForLink.Count >= 9) return;
            if (!ListForLink.ContainsKey(text))
                ListForLink.Add(text, false);
        }
    }
}
