namespace InstagramBot.Data.Accounts
{
   public struct MiniInfo
    {
        public long ID { get; set; }
        public string URL { get; set; }
        public MiniInfo(long id = -1, string url = "")
        {
            ID = id;
            URL = url;
        }
        public void Reset()
        {
            ID = -1;
            URL = "";
        }
    }
}
