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
        static string BaseUrl = "https://ftx.com/api";

        static MarketService()
        {
            client = new HttpClient();
        }

        public static async Task<IEnumerable<Market>> GetMarket()
        {
            var response = await client.GetStringAsync("https://ftx.com/api/markets");
            Response responseObj = JsonConvert.DeserializeObject<Response>(response);
            ObservableRangeCollection<Market> markets = responseObj.Result;
            return markets;
        } 
    }

    class Response
    {
        public bool Success { get; set; }
        public ObservableRangeCollection<Market> Result { get; set; }
    }
}
