using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

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
            if (update.Type != UpdateType.Message || update.Message == null)
                return;
            var chatId = message.Chat.Id;
            
            if (message.Text != null)
            {

                Console.WriteLine($"{message.Chat.Username ?? "Анон"} | {message.Text}");
                try
                {
                    if (message.Text.ToLower().Contains("привет"))
                    {
                        var _message = await _bot.SendMessage(chatId, "Trying <b>all the parameters</b> of <code>sendMessage</code> method",
                        ParseMode.Html,
                        protectContent: true,
                        replyParameters: update.Message.Id,
                        replyMarkup: new InlineKeyboardButton("Check sendMessage method", "https://core.telegram.org/bots/api#sendmessage"));
                        Console.WriteLine(
                                            $"{_message.From.FirstName} sent message {_message.Id} " +
                                            $"to chat {_message.Chat.Id} at {_message.Date.ToLocalTime()}. " +
                                            $"It is a reply to message {_message.ReplyToMessage.Id} " +
                                            $"and has {_message.Entities.Length} message entities.");
                    }

                    else if (message.Text.ToLower().Contains("sol"))
                    {
                        await _bot.SendMessage(chatId, "E1cdU7FnKRHSaPw5KobTi8S1zQiECWgw8SQL1ceaGgrg");

                    }

                    else
                    {
                        //var __message = await _bot.SendPhoto(chatId, "https://telegrambots.github.io/book/docs/photo-ara.jpg",
                        //            "<b>Ara bird</b>. <i>Source</i>: <a href=\"https://pixabay.com\">Pixabay</a>", ParseMode.Html);

                        //var message1 = await _bot.SendSticker(chatId, "https://telegrambots.github.io/book/docs/sticker-fred.webp");
                        //var message2 = await _bot.SendSticker(chatId, message1.Sticker!.FileId);

                        //var pollMessage = await _bot.SendPoll(chatId: chatId,
                        //"Did you ever hear the tragedy of Darth Plagueis The Wise?",
                        //[
                        //    "Yes for the hundredth time!",
                        //    "No, who`s that?"
                        //]);

                        //Poll poll = await _bot.StopPoll(pollMessage.Chat, pollMessage.Id);

                        // using Telegram.Bot.Types.ReplyMarkups;

                        //var sent = await _bot.SendMessage(chatId, "Choose a response",
                        //                                 replyMarkup: new string[] { "Help me", "Call me ☎️" });

                        var sent = await _bot.SendMessage(chatId, "Choose a response", replyMarkup: new string[][]
                                    {
                                        ["Help me"],
                                        ["Problem? ✉️"]
                                    });
                        var sent1 = await _bot.SendMessage(chatId, "A message with an inline keyboard markup",
                            replyMarkup: new InlineKeyboardButton[][]
                            {
                                [("1.1", "11"), ("1.2", "12")], // two buttons on first row
                                [("2.1", "21"), ("2.2", "22")]  // two buttons on second row
                            });

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