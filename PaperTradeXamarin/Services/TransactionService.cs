using MvvmHelpers;
using Newtonsoft.Json;
using PaperTradeXamarin.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PaperTradeXamarin.Services
{
    class TransactionService
    {
        static HttpClient client;
        static string BaseUrl = "http://10.0.0.9:45455";

        static TransactionService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public static async Task<IEnumerable<Transaction>> GetTransactions()
        {
            // Make it work With UserId
            var res = await client.GetStringAsync("api/transactions");
            var convertedTransactions = JsonConvert.DeserializeObject<IEnumerable<Transaction>>(res);
            return convertedTransactions;
        }

        public static async Task<HttpResponseMessage> PostTransaction(Transaction transaction)
        {
            var json = JsonConvert.SerializeObject(transaction);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/transactions", content);

            return response;
        }
    }
}
