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

        public int UserId { get; set; }


        public WalletViewModel()
        {
            var properties = Xamarin.Forms.Application.Current.Properties;
            //UserId = (int)properties["userId"];
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
            /*var wallets = await WalletService.GetUserWallets(UserId);*/
            var wallets = await WalletService.GetWallets();
            LoadWalletOnProperties(wallets);
            foreach (Wallet wallet in wallets)
            {
                wallet.CurrencyValue = Enum.GetName(typeof(Currency), wallet.Currency);
            }
            Wallets.AddRange(wallets);
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
            /*var wallets = await WalletService.GetUserWallets(UserId);*/
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
