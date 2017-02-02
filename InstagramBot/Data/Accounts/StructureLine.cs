namespace InstagramBot.Data.Accounts
{
   public class StructureLine
    {
        public long ID { get; set; }
        public int CountFromMyURL { get; set; }
        public int CountFromFriends { get; set; }
        public int CountUlimited { get; set; }
        public int CountMinimum { get; set; }
        public int CountBlock { get; set; }
        public int CountNull { get; set; }
    }
}
