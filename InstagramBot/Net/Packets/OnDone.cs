using System;
using System.Collections.Generic;
using System.Linq;
using InstagramBot.Data;
using InstagramBot.Data.Accounts;

namespace InstagramBot.Net.Packets
{
   public class OnDone : IActionPacket
    {
        public Session Session { get; set; }
        
        public void Serialize(ActionBot user, StateEventArgs e)
        {
            try
            {
                Session.Bot?.SendTextMessageAsync(user.TelegramId,
                    "Поздравляем! Вы прошли первый этап: ваш статус \"СТАРТ\".");
             
                Session.MySql.InsertNewAccount(user.Account.Uid, user.Account.Referal, user.TelegramId,
                    user.Account.ToReferalId, States.OnAlreadyUsing, DateTime.Now);
                Session.Bot?.SendTextMessageAsync(user.TelegramId,
                    "Пригласите минимум 3 пользователей Instagram для получения статуса \"РОСТ\".\nДля приглашения воспользуйтесь вашей реферальный ссылкой.");
                int count;
                Session.MySql.GetCountFollows(user.Account.FromReferalId, out count);
                long telegramId;
                Session.MySql.GetTelegramId(user.Account.FromReferalId, out telegramId);
                Session.Bot?.SendTextMessageAsync(telegramId,
                    "По вашей ссылке зарегистрировался " + user.Account.Referal);
                if (count == 1)
                {
                    Session.MySql.UpdateStatus(user.Account.FromReferalId, true);
                    if (telegramId != 0)
                        Session.Bot?.SendTextMessageAsync(telegramId,
                            "Поздравляем! Вы получили статус \"РОСТ\"\n" +
                            "С этого момента каждый приглашенный вами человек будет приносить вам новых подписчиков \n" +
                            "Для получения новых подписчиков в ваш Instagram, пригласите друзей в scs110100bot по вашей реферальный ссылке: https://t.me/scs110100bot?start=" +
                            user.Account.FromReferal);
                }

                Session.MySql.UpdateCountFollows(user.Account.FromReferalId, count + 1);
                List<string> redlist = user.AdditionInfo.ListForLink.Keys.ToList();
                Session.MySql.InsertRedList(user.Account.Uid, redlist[0], redlist[1], redlist[2]);
                Console.WriteLine("[{0}] {1} Успешно зарегистрировался", DateTime.Now, user.Account.Referal);
                user.Account = null;
                user.SetState(States.OnAlreadyUsing);
            }
            catch { }
        }
        public void Deserialize(ActionBot user, StateEventArgs e)
        {
          
        }
    }
}
