using System;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;

namespace InstagramBot.Net.Packets
{
    public class OnRegistering : IActionPacket
    {
        public Session Session { get; set; }
        public  void Serialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                
                    Session.Bot?.SendTextMessageAsync(user.TelegramId,
                        "Добро пожаловать!\nПосмотрите видео\nСсылка на видео: https://youtu.be/nWdoU7e1OxU \n");
                
                    Session.Bot?.SendTextMessageAsync(user.TelegramId,
                        "--------\nДля старта введите ваш ник в instagram\n--------");

                Console.WriteLine("[{0}] {1} Начинает регистрацию", DateTime.Now, user.TelegramId);
            }catch(Exception ex)
            {
                }
        }
        public async void Deserialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                if (e.Message.Text.Contains("/")) return;
                user.Account =  Session.WebInstagram.GetAccount(e.Message.Text.Replace("@", ""));
                if (user.Account == null)
                {
                    await
                        Session.Bot?.SendTextMessageAsync(user.TelegramId,
                            "Данного аккаунта не существует. Повторите попытку");
                    return;
                }
                user.SetState(States.WaitUrl);
            }
            catch { }
        }
    }
}
