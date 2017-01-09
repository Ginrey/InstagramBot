using InstagramBot.Data;
using InstagramBot.Data.Accounts;

namespace InstagramBot.Net.Packets
{
    class OnFindClients : ActionPacket
    {
       public Session Session { get; set; }

      public  void Deserialize(ActionBot user, StateEventArgs e)
      {
          string referal = e.Message.Text;
          if (string.IsNullOrEmpty(referal) || referal.Length > 20)
          {
              Session.Bot?.SendTextMessageAsync(user.TelegramID, "Неверные данные");
                return;
          }
          string present = Session.MySql.IsPresentReferal(referal) ? "уже" : "не";
          Session.Bot?.SendTextMessageAsync(user.TelegramID, "Данный человек " + present + " использует данный сервис");
          user.State = States.OnAlreadyUsing;
      }

      public  void Serialize(ActionBot user, StateEventArgs e)
      {
          Session.Bot?.SendTextMessageAsync(user.TelegramID, "Введите ник человека");
      }
    }
}
