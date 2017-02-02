namespace InstagramBot.Data.Accounts
{
   public class MiniInfo
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
        public void Set(long id)
        {
            ID = id;
        }
        public void Set(string url)
        {
            URL = url;
        }
        public MiniInfo Copy => new MiniInfo(ID, URL);

    }
}
