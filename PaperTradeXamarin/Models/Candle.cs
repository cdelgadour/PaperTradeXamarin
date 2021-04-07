using System;
using System.Collections.Generic;
using System.Text;

namespace PaperTradeXamarin.Models
{
    public class ExtendedCandle : Candle {

        public DateTime OpenTimeDate { get; set; }
        public DateTime CloseTimeDate { get; set; }
    }
    public class Candle
    {
        public string OpenTime { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
        public string CloseTime { get; set; }
        public double Asset_Volume { get; set; }
        public int Trades { get; set; }
        public double Base_Asset_Volume { get; set; }
        public double Quote_Asset_Volume { get; set; }
        public double Ignore { get; set; }
    }
}
