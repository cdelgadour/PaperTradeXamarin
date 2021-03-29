using System;
using System.Collections.Generic;
using System.Text;

namespace PaperTradeXamarin.Models
{
    class Market
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }

        public bool PostOnly { get; set; }

        public float PriceIncrement { get; set; }

        public string SizeIncrement { get; set; }

        public string MinProvideSize { get; set; }

        public string Last { get; set; }

        public string  Bid { get; set; }
        public string Ask { get; set; }
        public float Price { get; set; }

        public string Type { get; set; }

        public string BaseCurrency { get; set; }

        public string QuoteCurrency { get; set; }

        public string Underlying { get; set; }

        public bool Restricted { get; set; }

        public string HighLeverageFeeExempt { get; set; }

        public string Change1h { get; set; }

        public string Change24h { get; set; }

        public string ChangeBod { get; set; }

        public string QuoteVolume24h { get; set; }

        public string VolumeUsd24h { get; set; }
    }
}
