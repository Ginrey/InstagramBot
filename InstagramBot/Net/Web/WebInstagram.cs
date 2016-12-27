using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramBot.Parsing
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
