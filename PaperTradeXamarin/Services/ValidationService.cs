using Newtonsoft.Json;
using PaperTradeXamarin.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PaperTradeXamarin.Services
{
    class ValidationService
    {
        static HttpClient client;
        static string BaseUrl = "http://10.0.0.9:45455";

        static ValidationService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public static async Task<bool> LoginUser(ValidateUser user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await client.PostAsync("api/users/validation", content);

            return res.IsSuccessStatusCode;
        }
    }
}
