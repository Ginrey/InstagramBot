#region

using System;
using System.Collections.Generic;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;
using InstagramBot.IO;

#endregion

namespace InstagramBot.Net.Packets
{
    internal class OnAlreadyUsing : IActionPacket
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
                        Info = info[0]
                    };

                    user.Account.IsVip = Bloggers.Contains(user.Account.Info.Id);

                    if (Session.BlockedList.Contains(user.Account.Info.Id))
                        Session.Bot?.SendTextMessageAsync(user.TelegramId,
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
                        Session.MySql.GetLanguage(user.Account.Info.Id, out language);
                        user.Language = language;
                        Console.WriteLine("[{0}] {1} Enter to account", DateTime.Now, user.Account.Info.Url);
                        user.ShowMainMenu();
                    }
                }
            }
            catch (Exception ex)
            {
                LOG.Add("OAUS", ex);
            }
        }

        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(e.Message.Text) || (user.Account == null)) return;
                string command = Session.Language.GetReverse(e.Message.Text);
                switch (command)
                {
                    case Config.MenuList.ReferalUrl:
                        user.ShowMyUrl();
                        break;
                    case Config.MenuList.MyReferals:
                        Menu.ShowMyReferals(user);
                        break;
                    case Config.MenuList.MyListUsers:
                        user.ShowStruct(false);
                        break;
                    case Config.MenuList.MyPrivateFollows:
                        user.ShowMyRedList();
                        break;
                    case Config.MenuList.Order:
                        Session.Bot?.SendTextMessageAsync(user.TelegramId, Config.Order(user.Language));
                        break;
                    case Config.MenuList.PrivateOffice:
                        user.ShowLK();
                        break;
                    case Config.MenuList.ChangeLanguage:
                        user.State = States.ChangeLanguage;
                        break;
                    case Config.MenuList.BackToMenu:
                        user.ShowMainMenu(Config.MenuList.MainMenu);
                        break;
                    case Config.MenuList.Status:
                        user.ShowStatus();
                        break;
                    case Config.MenuList.Struct:
                        user.ShowStruct(true);
                        break;
                    case Config.MyListUser:
                        user.ShowListStruct(1);
                        break;
                    case Config.MyNullListUser:
                        user.ShowListStruct(0);
                        break;
                    case Config.MenuList.CheckUsersOnInstagram:
                        user.State = States.OnFindClients;
                        break;
                    case Config.MenuList.PromoMaterials:
                        user.ShowPromo();
                        break;
                    case Config.MenuList.WhereReferals:
                        Session.Bot?.SendTextMessageAsync(user.TelegramId, "https://youtu.be/nWdoU7e1OxU");
                        break;

                    case Config.MenuList.PrivilegeList:
                        user.NewPrivilegeInstagram();
                        break;
                    case Config.MenuList.HowMachUsers:
                        user.ShowCountUsers();
                        break;

                    case Config.MenuList.MyRevolver:
                        user.ShowBloggersMenu();
                        break;
                    case Config.MenuBloggers.Members:
                        user.ShowMembersCount();
                        break;
                    case Config.MenuBloggers.Limit:
                        user.ShowStatistics();
                        break;
                    case Config.MenuBloggers.Registering:
                        user.ShowRegistering();
                        break;
                    case Config.MenuBloggers.Chat:
                        user.ShowChat();
                        break;
                    case Config.MenuBloggers.Channel:
                        user.ShowChannel();
                        break;
                    case Config.MenuBloggers.MyInfo:
                        user.ShowMyInfo();
                        break;
                    case Config.MenuBloggers.ListMembers:
                        user.ShowListMembers();
                        break;
                    case Config.MenuBloggers.ListNew:
                        user.ShowListNew();
                        break;
                    case Config.MenuBloggers.ListStatistics:
                        user.ShowListStatistics();
                        break;
                    case Config.MenuBloggers.ListOverall:
                        user.ShowListOverall();
                        break;
                    case Config.MenuBloggers.ListUpLine:
                        user.ShowListUpline();
                        break;

                    case Config.MenuList.MultiClients:
                        user.State = States.Multiaccount;
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LOG.Add("OAU", ex);
            }
        }
    }
}