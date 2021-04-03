using PaperTradeXamarin.Models;
using PaperTradeXamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PaperTradeXamarin.ViewModels
{
    [QueryProperty(nameof(Id), "id")]
    class WalletDetailViewModel : BaseViewModel
    {
        public string id;
        public string Id { get => id; set { SetProperty(ref id, value); LoadMarket(Id); } }

        public Wallet currentWallet;

        public Wallet CurrentWallet { get => currentWallet; set => SetProperty(ref currentWallet, value); }

        public WalletDetailViewModel()
        {
            CurrentWallet = new Wallet();
        }
        public async void LoadMarket(string id)
        {
            List<Wallet> walletList = new List<Wallet>();
            var wallets = await WalletService.GetWallets();
            walletList.AddRange(wallets);

            var wallet = walletList.Find(x => x.Id == Convert.ToInt32(id));
            CurrentWallet = wallet;
        }
    }
}
