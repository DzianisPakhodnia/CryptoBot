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
            services.AddTransient<WebSocketService>();
            return services;
        }
    }



    public class WebSocketService
    {
        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            var websocket = new MarketDataWebSocket("btcusdt@aggTrade");

            websocket.OnMessageReceived(
    (data) =>
    {
        Console.WriteLine($"[WebSocket Raw] {data}");
        return Task.CompletedTask;
    }, cancellationToken);

            await websocket.ConnectAsync(cancellationToken);
        }
    }



}
