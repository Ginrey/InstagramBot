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
                  Session.Bot?.SendTextMessageAsync(user.TelegramId,
                      string.Format(Session.Language.Get(user.Language, "ofc_invalid_data")));
                  return;
              }
              Session.Bot?.SendTextMessageAsync(user.TelegramId,
                  Session.MySql.IsPresentURL(referal)
                      ? string.Format(Session.Language.Get(user.Language, "ofc_already_used"))
                      : string.Format(Session.Language.Get(user.Language, "ofc_not_used")));
              user.State = States.OnAlreadyUsing;
          }
          catch
          {
          }
      }

      public  void Serialize(ActionBot user, StateEventArgs e)
      {
          try
          {
              Session.Bot?.SendTextMessageAsync(user.TelegramId,
                  string.Format(Session.Language.Get(user.Language, "ofc_enter_username")));
          }
          catch
          {
          }
      }
    }
}
