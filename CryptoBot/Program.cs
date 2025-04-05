using Binance.Common;
using Binance.Spot;
using Binance.Spot.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoBot
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            var app = serviceProvider.GetRequiredService<App>();
            await app.RunAsync();
        }

        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            IConfiguration config = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            services.AddSingleton(config);
            services.AddSingleton<HttpClient>();
            services.AddTransient<App>();
            services.AddTransient<BinanceService>();

            return services;
        }
    }

    public class App
    {
        private readonly IConfiguration _config;
        private readonly BinanceService _binanceService;

        public App(IConfiguration config, BinanceService binanceService)
        {
            _config = config;
            _binanceService = binanceService;
        }

        public async Task RunAsync()
        {
            string? apiKey1 = _config["TelegramBot:ApiKey"];
            var client = new Host(apiKey1);
            client.Start();

            var market = new Market();
            string serverTime = await market.CheckServerTime();
            Console.WriteLine($"Server Time: {serverTime}");

            await _binanceService.SellAsync();
        }
    }

    public class BinanceService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public BinanceService(IConfiguration config, HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        public async Task SellAsync()
        {
            try
            {
                string? apiKey = _config["Binance:ApiKey"];
                string? apiSecret = _config["Binance:SecretKey"];

                var spotAccountTrade = new SpotAccountTrade(_httpClient, apiKey: apiKey, apiSecret: apiSecret);
                var result = await spotAccountTrade.TestNewOrder("SLPUSDT", Side.SELL, OrderType.MARKET, quantity: 4500);

                Console.WriteLine($"Sell order executed: {result}");
            }

            catch (BinanceClientException ex)
            {
                Console.WriteLine($"Error {ex.Message}");

            }
        }
    }
}
