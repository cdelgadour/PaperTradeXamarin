using MvvmHelpers;
using Newtonsoft.Json;
using PaperTradeXamarin.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PaperTradeXamarin.Services
{
    class CandleService
    {
        static HttpClient client;

        static CandleService()
        {
            client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(20);
        }

        public static async Task<IEnumerable<Candle>> GetMarketData(string market)
        {
            var response = await client.GetStringAsync($"https://ftx.com/api/markets/{market}/candles?resolution=15&limit=10");
            CandleResponse responseObj = JsonConvert.DeserializeObject<CandleResponse>(response);
            ObservableRangeCollection<Candle> candleList = responseObj.Result;
            return candleList;
        }
    }

    class CandleResponse
    {
        public bool Success { get; set; }
        public ObservableRangeCollection<Candle> Result { get; set; }
    }
}
