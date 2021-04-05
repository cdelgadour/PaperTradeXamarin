using Newtonsoft.Json;
using PaperTradeXamarin.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PaperTradeXamarin.Services
{
    class ValidationService : BaseService
    {
        static HttpClient client;

        static ValidationService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public static async Task<User> LoginUser(ValidateUser user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await client.PostAsync("api/users/validation", content);
            User returnUser = new User { Id = 0 };

            if (res.IsSuccessStatusCode)
            {
                returnUser = JsonConvert.DeserializeObject<User>(res.Content.ReadAsStringAsync().Result);
            }

            return returnUser;
        }
    }
}
