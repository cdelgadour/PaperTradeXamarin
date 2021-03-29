using PaperTradeXamarin.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Threading.Tasks;
using PaperTradeXamarin.Services;
using System.Linq;

namespace PaperTradeXamarin.ViewModels
{
    class MarketsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Market> Markets { get; set; }
        public AsyncCommand RefreshCommand { get; set; }

        public ObservableRangeCollection<Market> FilteredMarkets { get; set; }

        public string[] MarketList { get; set; }

        Market Filtered { get; set; }

        public MarketsViewModel()
        {
            Markets = new ObservableRangeCollection<Market>();
            RefreshCommand = new AsyncCommand(Refresh);
            FilteredMarkets = new ObservableRangeCollection<Market>();
            MarketList = new string[] { "BTC/USD", "ETH/USD", "ETH/BTC", "DOGE/BTC", "TSLA/BTC", "AAPL/USD", "AAPL-0625" };
        }

        async Task Refresh()
        {
            IsBusy = true;
            Markets.Clear();
            var markets = await MarketService.GetMarket();
            foreach (var marketName in MarketList)
            {
                Filtered = markets.Where(x => x.Name == marketName).FirstOrDefault();
                FilteredMarkets.Add(Filtered);
            }
            Markets.AddRange(FilteredMarkets);
            FilteredMarkets.Clear();
            IsBusy = false;
        }
    }
}
