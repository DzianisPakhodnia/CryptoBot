using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace CryptoBot
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var config = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

            string? apiKey = config["TelegramBot:ApiKey"];

            var client = new Host(apiKey);

            client.Start();

            Console.ReadLine();
        }







    }
}