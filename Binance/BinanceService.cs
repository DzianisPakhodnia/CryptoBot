using Binance.Common;
using Binance.Spot.Models;
using Binance.Spot;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBot
{
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
