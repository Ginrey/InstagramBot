using System;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;

namespace InstagramBot.Net.Packets
{
   public class OnDone : ActionPacket
    {
        public Session Session { get; set; }
        public void Serialize(ActionBot user, StateEventArgs e)
        {
            Session.Bot?.SendTextMessageAsync(user.TelegramID, "Поздравляем! Вы прошли первый этап: ваш статус \"СТАРТ\"");
            Session.MySql.InsertNewAccount(user.Account.Uid, user.Account.Referal, user.TelegramID, user.Account.ToReferalId, States.OnAlreadyUsing);
            Session.Bot?.SendTextMessageAsync(user.TelegramID,
                "Пригласите минимум 3 пользователей Instagram для получения статуса \"РОСТ\"");
            int count;
            Session.MySql.GetCountFollows(user.Account.FromReferalId, out count);

            if (count == 1)
            {
                Session.MySql.UpdateStatus(user.Account.FromReferalId, true);
                long telegramId;
                Session.MySql.GetTelegramId(user.Account.FromReferalId, out telegramId);
                if(telegramId != 0) Session.Bot?.SendTextMessageAsync(telegramId,
                    "Поздравляем! Вы получили статус \"РОСТ\"\n" +
                    "С этого момента каждый приглашенный вами человек будет приносить вам новых подписчиков ");
            }

            Session.MySql.UpdateCountFollows(user.Account.FromReferalId, count + 1);
            Console.WriteLine("[{0}] {1} Успешно зарегистрировался",DateTime.Now,user.Account.Referal);
        }
        public void Deserialize(ActionBot user, StateEventArgs e)
        {
          
        }
    }
}
