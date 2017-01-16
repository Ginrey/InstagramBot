using System.Collections.Generic;
using System.IO;
using System.Text;
using InstagramBot.IO;


namespace InstagramBot.Data
{
   public static class Config
   {
       static Dictionary<Language, string> orderDict = new Dictionary<Language, string>();
       
    
        public static string Order(Language lang)
        {
            {
                if(!orderDict.ContainsKey(lang))
                {
                    orderDict[lang] = File.ReadAllText(lang +"Order.txt", Encoding.Default);
                }
                return orderDict[lang];
            }
        }

        public static class MenuList
        {
            public const string PrivateOffice = "ml_private_office";
            public const string ReferalUrl = "ml_referal_url";
            public const string Order = "ml_order";
            public const string MyReferals = "ml_my_referals";
            public const string MyPrivateFollows = "ml_my_private_follows";
            public const string Status = "ml_status";
            public const string Struct = "ml_struct";
            public const string CheckUsersOnInstagram = "ml_check_users_on_instagram";
            public const string BackToMenu = "ml_back_to_menu";
            public const string PromoMaterials = "ml_promo_materials";
            public const string HowMachUsers = "ml_how_mach_users";
            public const string MyListUsers = "ml_my_list_users";
            public const string WhereReferals = "ml_where_referals";
            public const string MainMenu = "ml_main_menu";
            public const string LK = "ml_lk";
            public const string CompleteEnter = "login_success";
        }
    }
}
