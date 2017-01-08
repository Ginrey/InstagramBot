using InstagramBot.Data;
using InstagramBot.Data.Accounts;

namespace InstagramBot.Net.Packets
{
    class OnAlreadyUsing : ActionPacket
    {
        public Session Session { get; set; }
        public void Serialize(ActionBot user, StateEventArgs e)
        {
            string referal;
            if (Session.MySql.GetReferalByTelegramId(user.TelegramID, out referal))
            {
                user.Account = Session.WebInstagram.GetAccount(referal);
                Session.Bot?.SendTextMessageAsync(user.TelegramID, "Вход успешно выполнен");
            }
        }

        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Message.Text)) return;
            string[] commands = e.Message.Text.Split();
            if (commands.Length == 0) return;
            switch (commands[0])
            {
                case "/get_referal":
                    Session.Bot?.SendTextMessageAsync(user.TelegramID,
                        "Реферальная ссылка:" + "https://telegram.me/Hierarchy_Of_Instagram_Bot?start=" +
                        user.Account.Referal);
                    break;
                case "/count_follows":
                    int count;
                    Session.MySql.GetCountFollows(user.Account.FromReferalId, out count);
                    Session.Bot?.SendTextMessageAsync(user.TelegramID, "Количество людей зарегистрированных по вашей ссылке: " + count);
                    break;
                case "/need_follows":

                    break;
                case "/order":

                    break;
                default:

                    break;
            }
        }
    }
}
