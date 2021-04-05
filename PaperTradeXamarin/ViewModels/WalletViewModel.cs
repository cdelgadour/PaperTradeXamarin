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
    class WalletViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Wallet> Wallets { get; set; }
        public ObservableRangeCollection<ExtendedWallet> ExtendedWallets { get; set; }
        public ObservableRangeCollection<Market> Markets {get; set;}
        public AsyncCommand RefreshCommand { get; set; }

        public Xamarin.Forms.Command WalletTapped { get; set; }

        public int UserId { get; set; }


        public WalletViewModel()
        {
            var properties = Xamarin.Forms.Application.Current.Properties;
            UserId = (int)properties["userId"];
            Wallets = new ObservableRangeCollection<Wallet>();
            ExtendedWallets = new ObservableRangeCollection<ExtendedWallet>();
            RefreshCommand = new AsyncCommand(Refresh);
            WalletTapped = new Xamarin.Forms.Command<Wallet>(Selected);
            RefreshInitial();
        }

        async void Selected(Wallet wallet)
        {
            await Shell.Current.GoToAsync($"{nameof(WalletDetailPage)}?id={wallet.Id}");
        }

        async void RefreshInitial()
        {
            Wallets.Clear();
            Markets = (ObservableRangeCollection<Market>)await MarketService.GetMarket();
            var wallets = await WalletService.GetUserWallets(UserId);
            /*var wallets = await WalletService.GetWallets();*/
            var extendedWallets = createExtended(wallets);
            LoadWalletOnProperties(wallets);
            //Wallets.AddRange(wallets);
            ExtendedWallets.AddRange(extendedWallets);
        }

        List<ExtendedWallet> createExtended(IEnumerable<Wallet> wallets)
        {
            List<ExtendedWallet> extendedWallets = new List<ExtendedWallet>();

            foreach (Wallet wallet in wallets)
            {
                ExtendedWallet extended = new ExtendedWallet
                {
                    Balance = wallet.Balance,
                    Currency = wallet.Currency,
                    Id = wallet.Id,
                    UserId = wallet.UserId
                };

                if (extended.Currency == 0)
                {
                    Market market = Markets.Where(x => x.Name == "BTC/USD").FirstOrDefault();
                    extended.ValueInUsd = extended.Balance * (decimal)market.Price;
                }
                else if (extended.Currency == (Currency)1)
                {
                    Market market = Markets.Where(x => x.Name == "ETH/USD").FirstOrDefault();
                    extended.ValueInUsd = extended.Balance * (decimal)market.Price;
                }
                else
                {
                    extended.ValueInUsd = extended.Balance;
                }

                extended.Variation = String.Format("{0:P2}", extended.ValueInUsd / 2000);
                extended.Color = (extended.ValueInUsd / 2000) > 0 ? "Green" : "Red";
                extendedWallets.Add(extended);
            }
            return extendedWallets;
        }


        public void LoadWalletOnProperties(IEnumerable<Wallet> wallets)
        {
            var properties = Application.Current.Properties;
            List<string> walletIdList = new List<string>();
            string joinedList;
            foreach (var wallet in wallets)
            {
                walletIdList.Add(Convert.ToString(wallet.Id));
            }
            joinedList = string.Join(",", walletIdList);

            if (!properties.ContainsKey("walletList"))
            {
                properties.Add("walletList", joinedList);
            } else
            {
                properties["walletList"] = joinedList;
            }

        }

        async Task Refresh()
        {
            IsBusy = true;
            Wallets.Clear();
            var wallets = await WalletService.GetUserWallets(UserId);
            /*var wallets = await WalletService.GetWallets();*/
            var extendedWallets = createExtended(wallets);
            LoadWalletOnProperties(wallets);
            //Wallets.AddRange(wallets);
            ExtendedWallets.AddRange(extendedWallets);
            IsBusy = false;
        }
    }
}
