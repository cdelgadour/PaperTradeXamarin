using PaperTradeXamarin.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Threading.Tasks;
using PaperTradeXamarin.Services;
using System.Linq;
using Xamarin.Forms;
using PaperTradeXamarin.Views;

namespace PaperTradeXamarin.ViewModels
{
    class MarketsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Market> Markets { get; set; }
        public AsyncCommand RefreshCommand { get; set; }

        public Xamarin.Forms.Command MarketTapped { get; set; }

        public ObservableRangeCollection<Market> FilteredMarkets { get; set; }

        public string[] MarketList { get; set; }

        Market Filtered { get; set; }

        public MarketsViewModel()
        {
            Markets = new ObservableRangeCollection<Market>();
            FilteredMarkets = new ObservableRangeCollection<Market>();
            RefreshCommand = new AsyncCommand(GetMarketListCommand);
            MarketList = new string[] { "BTC/USD", "ETH/USD", "ETH/BTC", "DOGE/BTC", "TSLA/BTC", "AAPL/USD", "AAPL-0625" };
            MarketTapped = new Xamarin.Forms.Command<Market>(Selected);
            GetMarketList();
        }

        async void Selected(Market market)
        {
            await Shell.Current.GoToAsync($"{nameof(MarketDetailView)}?id={market.Name}");
        }

        private async void GetMarketList()
        {
            Markets.Clear();
            var markets = await MarketService.GetMarket();
            foreach (var marketName in MarketList)
            {
                Filtered = markets.Where(x => x.Name == marketName).FirstOrDefault();
                FilteredMarkets.Add(Filtered);
            }
            Markets.AddRange(FilteredMarkets);
            FilteredMarkets.Clear();
        }

        private async Task GetMarketListCommand()
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
