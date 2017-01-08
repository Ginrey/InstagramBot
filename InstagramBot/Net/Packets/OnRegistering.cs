using System;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;

namespace InstagramBot.Net.Packets
{
    public class OnRegistering : ActionPacket
    {
        public Session Session { get; set; }
        public void Serialize(ActionBot user, StateEventArgs e)
        {
            Session.Bot?.SendTextMessageAsync(user.TelegramID, "Добро пожаловать!\nПосмотрите видео\nСсылка на видео \nДля старта ввидите ваш ник в instagram");
            Console.WriteLine("[{0}] {1} Начинает регистрацию", DateTime.Now, user.TelegramID);
        }
        public void Deserialize(ActionBot user, StateEventArgs e)
        {
            if (e.Message.Text.Contains("/")) return;
            user.Account = Session.WebInstagram.GetAccount(e.Message.Text.Replace("@",""));
            if(user.Account == null)
            {
                Session.Bot?.SendTextMessageAsync(user.TelegramID, "Данного аккаунта не существует. Повторите попытку");
                return;
            }
            user.State++;
        }
    }
}
