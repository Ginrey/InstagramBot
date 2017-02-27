namespace InstagramBot.Data.Accounts
{
   public class MiniInfo
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public MiniInfo(long id = -1, string url = "")
        {
            Id = id;
            Url = url;
        }
        public void Reset()
        {
            Id = -1;
            Url = "";
        }
        public void Set(long id)
        {
            Id = id;
        }
        public void Set(string url)
        {
            Url = url;
        }
        public MiniInfo Copy => new MiniInfo(Id, Url);

    }
}
