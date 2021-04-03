using System;
using System.Collections.Generic;
using System.Text;

namespace PaperTradeXamarin.Models
{
    public enum Currency
    {
        Bitcoin,
        Ethereum,
        Dollar
    }
    public class Wallet
    {
        public int Id { get; set; }
        public Currency Currency { get; set; }

        public string CurrencyValue { get; set; }

        public decimal Balance { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public List<Transaction> Credits { get; set; }

        public List<Transaction> Debits { get; set; }
    }
}
