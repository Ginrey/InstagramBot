using System;
using System.Collections.Generic;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;
using InstagramBot.IO;


namespace InstagramBot.Net.Packets
{
    class OnAlreadyUsing : IActionPacket
    {
        public Session Session { get; set; }
       
        public void Serialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                List<MiniInfo> info;
                if (user.Account != null) return;
                if (Session.MySql.GetIdByTelegramId(user.TelegramId, out info))
                {
                    user.Account = new AccountInstagram
                    {
                        Id = info[0].ID,
                        URL = info[0].URL
                    };
                    
                    user.Account.IsVip = Bloggers.Contains(user.Account.Id);

                    if(Session.BlockedList.Contains(user.Account.Id)) Session.Bot?.SendTextMessageAsync(user.TelegramId,
                             string.Format(Session.Language.Get(user.Language, "ob_banned")));
                    if (user.Account == null)
                    {
                        user.Language = Language.Russian;
                        Session.Bot?.SendTextMessageAsync(user.TelegramId,
                            string.Format(Session.Language.Get(user.Language, "oau_login_failed")));
                    }
                    else
                    {
                        Language language;
                        Session.MySql.GetLanguage(user.Account.Id, out language);
                        user.Language = language;
                        Console.WriteLine("[{0}] {1} Enter to account", DateTime.Now, user.Account.URL);
                        Menu.ShowMainMenu(user);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(e.Message.Text) || user.Account == null) return;
                string command = Session.Language.GetReverse(e.Message.Text);
                switch (command)
                {
                    case Config.MenuList.ReferalUrl:
                        Menu.ShowMyUrl(user);
                        break;
                    case Config.MenuList.MyReferals:
                        Menu.ShowMyReferals(user);
                        break;
                    case Config.MenuList.MyListUsers:
                        Menu.ShowStruct(user, false);
                        break;
                    case Config.MenuList.MyPrivateFollows:
                        Menu.ShowMyRedList(user);
                        break;
                    case Config.MenuList.Order:
                        Session.Bot?.SendTextMessageAsync(user.TelegramId, Config.Order(user.Language));
                        break;
                    case Config.MenuList.PrivateOffice:
                        Menu.ShowLK(user);
                        break;
                    case Config.MenuList.ChangeLanguage:
                        user.State = States.ChangeLanguage;
                        break;
                    case Config.MenuList.BackToMenu:
                        Menu.ShowMainMenu(user, Config.MenuList.MainMenu);
                        break;
                    case Config.MenuList.Status:
                        Menu.ShowStatus(user);
                        break;
                    case Config.MenuList.Struct:
                        Menu.ShowStruct(user, true);
                        break;
                    case Config.MyListUser:
                        Menu.ShowListStruct(user, 1);
                        break;
                    case Config.MyNullListUser:
                        Menu.ShowListStruct(user, 0);
                        break;
                    case Config.MenuList.CheckUsersOnInstagram:
                        user.State = States.OnFindClients;
                        break;
                    case Config.MenuList.PromoMaterials:
                        Menu.ShowPromo(user);
                        break;
                    case Config.MenuList.WhereReferals:
                        Session.Bot?.SendTextMessageAsync(user.TelegramId, "https://youtu.be/nWdoU7e1OxU");
                        break;

                    case Config.MenuList.PrivilegeList:
                        Menu.NewPrivilegeInstagram(user);
                        break;
                    case Config.MenuList.HowMachUsers:
                        Menu.ShowCountUsers(user);
                        break;

                    case Config.MenuList.MyRevolver:
                        Menu.ShowBloggersMenu(user);
                        break;
                    case Config.MenuBloggers.Members:
                        MenuBloggers.ShowMembersCount(user);
                        break;
                    case Config.MenuBloggers.Limit:
                        MenuBloggers.ShowStatistics(user);
                        break;
                    case Config.MenuBloggers.Registering:
                        MenuBloggers.ShowRegistering(user);
                        break;
                    case Config.MenuBloggers.Chat:
                        MenuBloggers.ShowChat(user);
                        break;
                    case Config.MenuBloggers.Channel:
                        MenuBloggers.ShowChannel(user);
                        break;
                    case Config.MenuBloggers.MyInfo:
                        MenuBloggers.ShowMyInfo(user);
                        break;
                    case Config.MenuBloggers.ListMembers:
                        MenuBloggers.ShowListMembers(user);
                        break;
                    case Config.MenuBloggers.ListNew:
                        MenuBloggers.ShowListNew(user);
                        break;
                    case Config.MenuBloggers.ListStatistics:
                        MenuBloggers.ShowListStatistics(user);
                        break;
                    case Config.MenuBloggers.ListOverall:
                        MenuBloggers.ShowListOverall(user);
                        break;
                    case Config.MenuBloggers.ListUpLine:
                        MenuBloggers.ShowListUpline(user);
                        break;

                    case Config.MenuList.MultiClients:
                        user.State = States.Multiaccount;
                        break;

                    default:
                        break;
                }
            }
            catch(Exception ex)
            {
                LOG.Add("OAU", ex.Message);
            }
        }
    }
}
