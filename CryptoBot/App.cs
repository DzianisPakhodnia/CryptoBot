using Binance.Spot;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBot
{
    public class App
    {
        private readonly IConfiguration _config;
        private readonly BinanceService _binanceService;
        private readonly WebSocketService _webSocketService;

        public App(IConfiguration config, BinanceService binanceService, WebSocketService webSocketService)
        {
            _config = config;
            _binanceService = binanceService;
            _webSocketService = webSocketService;
        }

        public async Task RunAsync()
        {
            string? apiKey1 = _config["TelegramBot:ApiKey"];
            var client = new Host(apiKey1);
            client.Start();

            var market = new Market();
            string serverTime = await market.CheckServerTime();
            Console.WriteLine($"Server Time: {serverTime}");

            // запуск WebSocket
            _ = _webSocketService.StartAsync();

            await _binanceService.SellAsync();

            // держим приложение живым
            Console.WriteLine("Listening for WebSocket messages... Press Ctrl+C to exit.");
            await Task.Delay(Timeout.Infinite);
        }

    }

}
