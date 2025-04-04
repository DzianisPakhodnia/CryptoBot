using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace CryptoBot
{
    public class Host
    {
        public Action<ITelegramBotClient, Update>? onMessage;

        private TelegramBotClient _bot;



        public Host(string token)
        {
            _bot = new TelegramBotClient(token);

        }

        public void Start()
        {
            _bot.StartReceiving(UpdateHandler, ErrorHandler);
        }

        private async Task ErrorHandler(ITelegramBotClient client, Exception exception, HandleErrorSource source, CancellationToken token)
        {
            throw new NotImplementedException();

        }

        private async Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken token)
        {
            var message = update.Message;
            var chatId = message.Chat.Id;

            if (message.Text != null)
            {

                Console.WriteLine($"{message.Chat.Username ?? "Анон"} | {message.Text}");
                try
                {
                    if (message.Text.ToLower().Contains("привет"))
                    {
                        await _bot.SendMessage(chatId, "Ку и тебе");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при отправке сообщения: {ex.Message}");
                }

                return;
            }


            if (message.Photo != null)
            {
                await _bot.SendMessage(chatId, "Норм фото но скинь файлом");
                return;
            }

            try
            {
                if (message.Text.ToLower().Contains("привет"))
                {
                    await _bot.SendMessage(chatId, "Ку и тебе");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при отправке сообщения: {ex.Message}");
            }
        }

    }





}