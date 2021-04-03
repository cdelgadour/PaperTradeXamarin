using PaperTradeXamarin.Models;
using PaperTradeXamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PaperTradeXamarin.ViewModels
{
    [QueryProperty(nameof(Name), "id")]
    class MarketDetailViewModel : BaseViewModel
    {
        public string name;
        public string Name { get => name; set { SetProperty(ref name, value); LoadMarket(Name);  } }

        public Market currentMarket;

        public Market CurrentMarket { get => currentMarket; set => SetProperty(ref currentMarket, value); }

        public MarketDetailViewModel()
        {
            CurrentMarket = new Market();
        }
        public async void LoadMarket(string name)
        {
            List<Market> marketList = new List<Market>();
            var markets = await MarketService.GetMarket();
            marketList.AddRange(markets);

            var market = marketList.Find(x => x.Name == name);
           CurrentMarket = market;
        }
    }
}
