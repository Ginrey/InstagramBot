namespace InstagramBot.Data.Accounts
{
    public class AccountInstagram
    {
        public int Uid { get; set; }
        public string Name { get; set; }
        public string Referal { get; set; }
        public int FromReferalId { get; set; }
        public string FromReferal { get; set; }
        public int Posts { get; set; }
        public int Folowers { get; set; }
        public int Following { get; set; }
    }
}
