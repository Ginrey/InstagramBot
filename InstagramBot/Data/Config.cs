using System.Collections.Generic;
using System.IO;
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
                    orderDict[lang] = File.ReadAllText(lang +"Order.txt");
                }
                return orderDict[lang];
            }
        }

        public const string MyListUser = "oau_my_list_users";
        public const string MyNullListUser = "oau_my_nulllist_users";
        public static class MenuBloggers
        {
            public const string Members = "mb_members";
            public const string Limit = "mb_limit";
            public const string Registering = "mb_registering";
            public const string Chat = "mb_chat";
            public const string Channel = "mb_channel";
            public const string MyInfo = "mb_my_info";

            public const string ListMembers = "/List_members";
            public const string ListNew = "/List_new";
            public const string ListStatistics = "/Statistics";
            public const string ListOverall = "/List_overall";
            public const string ListUpLine = "/List_upline";
        }
        public static class MenuMultiClients
        {
            public const string Current = "mmc_current";
            public const string Add = "mmc_add";
            public const string Change = "mmc_change";
            public const string EditNick = "mmc_editnick";
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
            public const string MultiClients = "ml_multi_clients";
            public const string BackToMenu = "ml_back_to_menu";
            public const string PromoMaterials = "ml_promo_materials";

            public const string HowMachUsers = "ml_how_mach_users";
            public const string PrivilegeList = "ml_new_privilege";

            public const string MyRevolver = "ml_revolver";

            public const string MyListUsers = "ml_my_list_users";
            public const string WhereReferals = "ml_where_referals";
            public const string MainMenu = "ml_main_menu";
            public const string LK = "ml_lk";
            public const string ChangeLanguage = "ml_change_language";
            public const string CompleteEnter = "login_success";
        }
    }
}
