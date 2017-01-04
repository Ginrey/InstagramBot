using InstagramBot.Data;
using InstagramBot.Data.Accounts;

namespace InstagramBot.Net.Packets
{
   public class OnDone : ActionPacket
    {
        public Session Session { get; set; }
        public void Serialize(ActionBot user, StateEventArgs e)
        {
            Session.Bot?.SendTextMessageAsync(user.TelegramID, "Спасибо за то, что пользуетесь нашим сервисом. Регистрация прошла успешно");
            Session.MySql.InsertNewAccount(user.Account.Uid, user.Account.Referal, user.Account.ToReferalId, States.Done);
            Session.Bot?.SendTextMessageAsync(user.TelegramID, 
                "Чтобы начать получать своих подписчиков Вам необходимо привести по своей реферальной ссылке(Вашему нику) 3 человека");
            int count;
            Session.MySql.GetCountFollows(user.Account.FromReferalId, out count);

            if (count == 1) Session.MySql.UpdateStatus(user.Account.FromReferalId, true);

            Session.MySql.UpdateCountFollows(user.Account.FromReferalId, count + 1);
        }
        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            string[] commands = user.Message.Text.Split();
            if (commands.Length == 0) return;
            switch(commands[0])
            {
                case "/Count_follows":
                    int count;
                    Session.MySql.GetCountFollows(user.Account.FromReferalId, out count);
                    Session.Bot?.SendTextMessageAsync(user.TelegramID, "Количество людей зарегистрировавшихся по вашей ссылке: " + count);
                    break;
                default:
                    Session.Bot?.SendTextMessageAsync(user.TelegramID,
               "Спасибо, что пользуетесь нашим сервисом. Список команд:" +
               "/Count_follows - показывает число людей пришедших по вашей ссылке");
                    break;
            }
        }
    }
}
