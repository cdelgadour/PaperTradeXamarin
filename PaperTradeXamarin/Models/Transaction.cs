using System;
using System.Collections.Generic;
using System.Text;

namespace PaperTradeXamarin.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }

        public int DebitWalletId { get; set; }
        public Wallet Debit { get; set; }
        public int CreditWalletId { get; set; }
        public Wallet Credit { get; set; }
        public decimal Amount { get; set; }

        public decimal BuyingPrice { get; set; }

        public decimal TransactionTotal { get; set; }
    }
}
