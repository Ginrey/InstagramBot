namespace InstagramBot.Data.Accounts
{
    public class AccountInstagram
    {
        public long Uid { get; set; }
        public string Name { get; set; }
        public string Referal { get; set; }
        public MiniInfo From { get; set; }
        public MiniInfo To { get; set; } 
        public MiniInfo Temp { get; set; }
        public int Posts { get; set; }
        public int Folowers { get; set; }
        public int Following { get; set; }
        public bool IsPrivate { get; set; }
    }
}
