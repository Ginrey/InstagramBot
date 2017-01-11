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

        public static string TextUpVideo => "Данное видео служит вам помощником в поиске ваших рефералов. Разместите вашу реферальный ссылку в ваш Instagram под аватарку. Опубликуйте данное видео. Текст в комментарий скопируйте ниже. " +
                                            "Обратите внимание! Если вы размещаете вашу реферальный ссылку в Instagram, то оставьте в комментарии данную строчку: \"Ссылка для регистрации в моем профиле \" А эту строчку \" Переходите по ссылке под аватаркой в данном аккаунте @scs110100bot и при регистрации укажите мой ник в Instagram ))\" - удалите. " +
                                            "Если вы не размещаете вашу реферальную ссылку, то воспользуйтесь нижней строчкой в комментариях, а верхнюю - удалите.";

        public static string TextDownVideo
            =>
            "Всё гениальное - просто! Когда кажется, что в этом мире уже придумано всё, этот парень в очередной раз удивляет! На свет появился абсолютно новый по своей концепции проект для роста подписчиков. Живых, нормальных аккаунтов, которыми управляют люди, а не программы. Благодаря системе, реализованной на базе Telegram, scs110100bot раздаёт трафик подписчиков. Гибрид сетевого маркетинга и социальных сетей даёт возможность каждому пользователю Instagram развить свой аккаунт в самые кратчайшие сроки и набрать сотни тысяч реальных подписок. Всё, что нужно сделать - подключиться к Telegram bot, выполнить простейшие действия в течение нескольких минут и наблюдать за стремительным ростом своего аккаунта. Друзья, я уже в теме, тестирую бота по полной и приглашаю вас.Ссылка для регистрации в моем профиле. Переходите по ссылке под аватаркой в данном аккаунте @scs110100bot и при регистрации укажите мой ник в Instagram.";

        public static class MenuList
        {
            public const string PrivateOffice = "🕹 Личный кабинет";
            public const string ReferalUrl = "📤 Реферальная ссылка";
            public const string Order = "📝 Правила";
            public const string MyReferals = "😀 Мои рефералы";
            public const string MyPrivateFollows = "📕 Мои личные подписки";
            public const string Status = "💼 Статус";
            public const string Struct = "🏟 Структура";
            public const string CheckUsersOnInstagram = "❓ Проверить пользователя Инстаграм на участие в 110100bot";
            public const string BackToMenu = "🔙 Главное меню";
            public const string PromoMaterials = "💼 ПРОМО МАТЕРИАЛЫ";
            public const string HowMachUsers = "Количество пользователей";
            public const string WhereReferals = "🌳 Откуда подписчики";
        }
    }
}
