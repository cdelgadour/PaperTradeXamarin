using System;
using System.Collections.Generic;
using System.Text;

namespace PaperTradeXamarin.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<Wallet> Wallets { get; set; }
    }
}
