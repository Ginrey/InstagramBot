using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramBot.Data
{
   public static class Config
    {
        static string order = null;

        public static string Order
        {
            get
            {
                if (string.IsNullOrEmpty(order))
                {
                    if(File.Exists(@"Order.txt"))
                    order = File.ReadAllText(@"Order.txt",Encoding.Default);
                }
                return order;
            }
        }

        public static class MenuList
        {
            public const string PrivateOffice = "🕹 Личный кабинет";
            public const string ReferalUrl = "📤 Реферальная ссылка";
            public const string Order = "📝 Правила";
            public const string MyReferals = "😀 Мои рефералы";
            public const string MyPrivateFollows = "📕 Мои личные подписки";
            public const string Status = "💼 Статус";
            public const string Struct = "🏟 Структура";
            public const  string CheckUsersOnInstagram = "❓ Проверить пользователя Инстаграм на участие в 110100bot";
            public const string BackToMenu = "🔙 Главное меню";
        }
    }
}
