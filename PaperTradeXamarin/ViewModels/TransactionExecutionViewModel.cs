using PaperTradeXamarin.Models;
using PaperTradeXamarin.Services;
using PaperTradeXamarin.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PaperTradeXamarin.ViewModels
{
    [QueryProperty(nameof(Id), "id")]
    public class TransactionExecutionViewModel : BaseViewModel
    {
        public string id { get; set; }
        public string Id { get => id; set { id = value; LoadTransaction(value); } }

        Wallet walletBuy;
        public Wallet WalletBuy
        {
            get => walletBuy;
            set => SetProperty(ref walletBuy, value);
        }
        Wallet walletSell;
        public Wallet WalletSell
        {
            get => walletSell;
            set => SetProperty(ref walletSell, value);
        }

        public Transaction currentTransaction;
        public Transaction CurrentTransaction
        {
            get => currentTransaction;
            set => SetProperty(ref currentTransaction, value);
        }

        public float buyingPrice;
        public float BuyingPrice { get => buyingPrice; set => SetProperty(ref buyingPrice, value); }

        public List<Wallet> Wallets { get; set; }

        public ICommand TransactionElection { get; set; }

        public decimal amount;
        public decimal Amount { get => amount; set { SetProperty(ref amount, value); TransactionTotal = value * (decimal)buyingPrice; CompleteTransaction.ChangeCanExecute(); } }

        public decimal transactionTotal;
        public decimal TransactionTotal { get => transactionTotal; set => SetProperty(ref transactionTotal, value); }

        public Command CompleteTransaction { get; set; }

        public TransactionExecutionViewModel()
        {
            try
            {
                WalletSell = new Wallet();
                CurrentTransaction = new Transaction();
                TransactionElection = new Command(GoToElection);
                Wallets = new List<Wallet>();
                CompleteTransaction = new Command(MakeTransaction, ValidTransaction);
            }
            catch (Exception e)
            {
                Console.WriteLine("IN catch");
                Console.WriteLine(e.InnerException);
                throw;
            }

        }

        private async void MakeTransaction()
        {
            BuildTransactionObject();
            HttpResponseMessage response = await TransactionService.PostTransaction(CurrentTransaction);

            if (!response.IsSuccessStatusCode)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ocurrio un error", "OK");
            } else
            {
                await Application.Current.MainPage.DisplayAlert("Transaccion Completada", "Su transaccion se realizo con exito.", "OK");
            }
            await Application.Current.MainPage.Navigation.PushAsync(new TransactionElection());
        }

        private void BuildTransactionObject()
        {
            Transaction sendTransaction = new Transaction {
                DebitWalletId = WalletSell.Id,
                CreditWalletId = WalletBuy.Id,
                Amount = Amount,
                BuyingPrice = (decimal)BuyingPrice,
                TransactionTotal = TransactionTotal
            };
            CurrentTransaction = sendTransaction;
        }

        private bool ValidTransaction()
        {
            if (Id == "1" || Id == "3")
            {
                return WalletSell.Balance >= TransactionTotal;
            }
            return WalletSell.Balance >= Amount;
        }

        public void LoadTransaction(string id)
        {
            try
            {
                switch (id)
                {
                    case "1":
                        AssignWallet(0, (Currency)2);
                        GetMarket(1);
                        break;
                    case "2":
                        AssignWallet((Currency)2, (Currency)0);
                        GetMarket(2);
                        break;
                    case "3":
                        AssignWallet((Currency)1, (Currency)2);
                        GetMarket(3);
                        break;
                    case "4":
                        AssignWallet((Currency)2, (Currency)1);
                        GetMarket(4);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public async void AssignWallet(Currency WalletIn, Currency WalletOut)
        {
            Wallets = (List<Wallet>)await WalletService.GetWallets();

            WalletBuy = Wallets.Find(x => x.Currency == WalletIn);
            WalletBuy.CurrencyValue = Enum.GetName(typeof(Currency), WalletBuy.Currency);
            WalletSell = Wallets.Find(x => x.Currency == WalletOut);
            WalletSell.CurrencyValue = Enum.GetName(typeof(Currency), WalletSell.Currency);
        }

        void GoToElection()
        {
            Application.Current.MainPage.Navigation.PushAsync(new TransactionElection());
        }

        public async void GetMarket(int id)
        {
            Market market = new Market { Price = (float)0.00m  } ;
            if (id == 1 || id == 2)
            {
                market = await MarketService.GetSingleMarket("BTC/USD");
            } else
            {
                market = await MarketService.GetSingleMarket("ETH/USD");
            }

            BuyingPrice = market.Price;
        }
        
    }
}
