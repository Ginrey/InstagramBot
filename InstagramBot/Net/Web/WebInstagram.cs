namespace InstagramBot.Net.Web
{
   public class WebInstagram : Authorization
    {
        public WebInstagram(string login = "", string password = "") : base(login,password)
        {
            Auth();
            GetUserNamefromURL("https://www.instagram.com/andrey.v2/");
        }

       
    }
}
