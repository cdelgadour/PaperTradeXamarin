using Newtonsoft.Json;
using PaperTradeXamarin.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PaperTradeXamarin.Services
{
    class WalletService : BaseService
    {
        static HttpClient client;

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

        public static async Task<IEnumerable<Wallet>> GetUserWallets(int id)
        {
            // Make it work With UserId
            var res = await client.GetStringAsync($"api/Users/{id}/Wallets");
            var convertedWallets = JsonConvert.DeserializeObject<IEnumerable<Wallet>>(res);
            return convertedWallets;
        }

        public static async Task<IEnumerable<Transaction>> GetWalletTransactions()
        {
            var properties = Application.Current.Properties;
            string[] walletIds = properties["walletList"].ToString().Split(',');
            List<Transaction> transactions = new List<Transaction>();
            foreach (var id in walletIds)
            {
                var res = await client.GetStringAsync($"api/Wallets/{id}/Transactions");
                var convertedTransactions = JsonConvert.DeserializeObject<IEnumerable<Transaction>>(res);
                transactions.AddRange(convertedTransactions);
            }

            return transactions;
        }
    }

    
}
