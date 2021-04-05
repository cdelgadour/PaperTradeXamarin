using MvvmHelpers;
using Newtonsoft.Json;
using PaperTradeXamarin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PaperTradeXamarin.Services
{
    class MarketService 
    {
        static HttpClient client;

        static MarketService()
        {
            client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(20);
        }

        public static async Task<IEnumerable<Market>> GetMarket()
        {
            var response = await client.GetStringAsync("https://ftx.com/api/markets");
            Response responseObj = JsonConvert.DeserializeObject<Response>(response);
            ObservableRangeCollection<Market> markets = responseObj.Result;
            return markets;
        } 

        public static async Task<Market> GetSingleMarket(string market)
        {
            var response = await client.GetStringAsync($"https://ftx.com/api/markets/{market}");
            SingleResponse responseObj = JsonConvert.DeserializeObject<SingleResponse>(response);
            Market marketResult = responseObj.Result;
            return marketResult;
        }
    }

    class Response
    {
        public bool Success { get; set; }
        public ObservableRangeCollection<Market> Result { get; set; }
    }

    class SingleResponse
    {
        public bool Success { get; set; }
        public Market Result { get; set; }
    }
}
