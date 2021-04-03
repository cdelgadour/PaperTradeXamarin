using Newtonsoft.Json;
using PaperTradeXamarin.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PaperTradeXamarin.Services
{
    class WalletService
    {
        static HttpClient client;
        static string BaseUrl = "http://10.0.0.9:45455";

        static WalletService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public static async Task<IEnumerable<Wallet>> GetWallets()
        {
            // Make it work With UserId
            var res = await client.GetStringAsync("api/wallets");
            var convertedWallets = JsonConvert.DeserializeObject<IEnumerable<Wallet>>(res);
            return convertedWallets;
        }
    }

    
}
