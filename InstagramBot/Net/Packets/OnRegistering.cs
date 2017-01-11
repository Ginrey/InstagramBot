using System;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;

namespace InstagramBot.Net.Packets
{
    public class OnRegistering : IActionPacket
    {
        public Session Session { get; set; }
        public async void Serialize(ActionBot user, StateEventArgs e)
        {
          await  Session.Bot?.SendTextMessageAsync(user.TelegramID, "Добро пожаловать!\nПосмотрите видео\nСсылка на видео: https://youtu.be/MVG4pY-3Sls \n");
          await  Session.Bot?.SendTextMessageAsync(user.TelegramID, "--------\nДля старта введите ваш ник в instagram\n--------");

            Console.WriteLine("[{0}] {1} Начинает регистрацию", DateTime.Now, user.TelegramID);
        }
        public async void Deserialize(ActionBot user, StateEventArgs e)
        {
            if (e.Message.Text.Contains("/")) return;
            user.Account = await Session.WebInstagram.GetAccount(e.Message.Text.Replace("@",""));
            if(user.Account == null)
            {
                await Session.Bot?.SendTextMessageAsync(user.TelegramID, "Данного аккаунта не существует. Повторите попытку");
                return;
            }
            user.State++;
        }
    }
}
