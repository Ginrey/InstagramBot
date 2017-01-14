using InstagramBot.Data;
using InstagramBot.Data.Accounts;

namespace InstagramBot.Net.Packets
{
    class OnFindClients : IActionPacket
    {
       public Session Session { get; set; }

      public  void Deserialize(ActionBot user, StateEventArgs e)
      {
          try
          {
              string referal = e.Message.Text;
              if (string.IsNullOrEmpty(referal) || referal.Length > 40)
              {
                  Session.Bot?.SendTextMessageAsync(user.TelegramId, "Неверные данные");
                  return;
              }
              string present = Session.MySql.IsPresentReferal(referal) ? "уже" : "не";
              Session.Bot?.SendTextMessageAsync(user.TelegramId,
                  "Данный человек " + present + " использует данный сервис");
              user.State = States.OnAlreadyUsing;
            }
            catch { }
      }

      public  void Serialize(ActionBot user, StateEventArgs e)
      {
          try
          {
              Session.Bot?.SendTextMessageAsync(user.TelegramId, "Введите ник человека");
            }
            catch { }
      }
    }
}
