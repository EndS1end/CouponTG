using CouponsGetBot.CouponHandlers;
using CouponsGetBot.DBHandlers;
using CouponsGetBot.Models;
using Npgsql;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CouponsGetBot
{
    internal class Program
    {
        public static KFC_CouponHandler KFC;
        public static BotDBContext BotDBContext;
        public static DataAccessor DataAccessor;
        static async Task Main()
        {
            string botToken = "6706177093:AAEwQQfr_pqmtnukAog6bTqom1hbHDRdLts";
            TelegramBotClient client = new TelegramBotClient(botToken);

            BotDBContext = new BotDBContext();
            DataAccessor = new DataAccessor(BotDBContext);
            DataAccessor.InitFacilities();
            
            foreach(var Facility in DataAccessor.GetAllFacilities())
            {
                Console.WriteLine(Facility.Title);
            }

            KFC = new KFC_CouponHandler();

            CancellationToken cts = new CancellationToken();

            client.StartReceiving(Update, Error);

            Console.WriteLine("Бот запущен!");

            Console.ReadKey();
        }

        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            Message msg = update.Message;

            //KFC.GetTextMessage(BotDBContext)

            await botClient.SendTextMessageAsync(msg.Chat.Id, KFC.GetTextMessage(DataAccessor));
        }

        async static Task Error(ITelegramBotClient botClient, Exception exc, CancellationToken token)
        {

        }
    }
}