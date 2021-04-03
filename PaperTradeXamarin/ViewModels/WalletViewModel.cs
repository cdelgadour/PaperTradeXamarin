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
        public AsyncCommand RefreshCommand { get; set; }

        public Xamarin.Forms.Command WalletTapped { get; set; }


        public WalletViewModel()
        {
            Wallets = new ObservableRangeCollection<Wallet>();
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
            var wallets = await WalletService.GetWallets();
            foreach (Wallet wallet in wallets)
            {
                wallet.CurrencyValue = Enum.GetName(typeof(Currency), wallet.Currency);
            }
            Wallets.AddRange(wallets);
        }

        async Task Refresh()
        {
            IsBusy = true;
            Wallets.Clear();
            var wallets = await WalletService.GetWallets();
            foreach (Wallet wallet in wallets)
            {
                wallet.CurrencyValue = Enum.GetName(typeof(Currency), wallet.Currency);
            }
            Wallets.AddRange(wallets);
            IsBusy = false;
        }
    }
}
