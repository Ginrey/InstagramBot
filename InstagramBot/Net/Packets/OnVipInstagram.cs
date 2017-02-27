#region

using System;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;
using InstagramBot.IO;

#endregion

namespace InstagramBot.Net.Packets
{
    public class OnVipInstagram : IActionPacket
    {
        public Session Session { get; set; }

        public void Serialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                Session.Bot?.SendTextMessageAsync(user.TelegramId, "Введите точные ники Vip-пользователей через пробел");
            }
            catch (Exception ex)
            {
            }
        }

        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                if (e.Message.Text.StartsWith("/")) return;
                string[] array = e.Message.Text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                string infoText = "";
                foreach (var item in array)
                {
                    if (!Session.MySql.IsPresentURL(item))
                    {
                        infoText += item + " добавлен на регистрацию \n";
                        Session.PrivilegeList.Add(item.ToLower());
                        continue;
                    }
                    MiniInfo info;
                    Session.MySql.GetReferal(item, out info);
                    if (info.Id != -1)
                    {
                        infoText += item + " добавлен в револьвер \n";
                        Session.MySql.InsertCorruption(info.Id, 1);
                    }
                }

                user.State = States.OnAlreadyUsing;
                Menu.ShowMainMenu(user, infoText);
                Refresher.Refresh();
            }
            catch (Exception ex)
            {
                Session.Bot?.SendTextMessageAsync(user.TelegramId, "Произошла ошибка " + ex.Message);
            }
        }
    }
}