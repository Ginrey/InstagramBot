using System;
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
                string referal;
                if (user.Account != null) return;
                if (Session.MySql.GetReferalByTelegramId(user.TelegramId, out referal))
                {
                    user.Account = Session.WebInstagram.GetAccount(referal);

                    if (user.Account == null)
                    {
                        user.Language = Language.Russian;
                        Session.Bot?.SendTextMessageAsync(user.TelegramId,
                            string.Format(Session.Language.Get(user.Language, "oau_login_failed")));
                    }
                    else
                    {
                        Language language;
                        Session.MySql.GetLanguage(user.Account.Uid, out language);
                        user.Language = language;
                        Console.WriteLine("[{0}] {1} Enter to account", DateTime.Now, user.Account.Referal);
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
                    case Config.MenuList.CheckUsersOnInstagram:
                        user.State = States.OnFindClients;
                        break;
                    case Config.MenuList.PromoMaterials:
                        Menu.ShowPromo(user);
                        break;
                    case Config.MenuList.WhereReferals:
                        Session.Bot?.SendTextMessageAsync(user.TelegramId, "https://youtu.be/nWdoU7e1OxU");
                        break;
                    case Config.MenuList.HowMachUsers:
                        Menu.ShowCountUsers(user);
                        break;
                    default:
                        break;
                }
            }
            catch(Exception ex)
            {
            }
        }
    }
}
