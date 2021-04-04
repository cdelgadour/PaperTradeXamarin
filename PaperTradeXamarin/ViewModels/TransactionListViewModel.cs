using MvvmHelpers;
using MvvmHelpers.Commands;
using PaperTradeXamarin.Models;
using PaperTradeXamarin.Services;
using PaperTradeXamarin.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PaperTradeXamarin.ViewModels
{
    public class TransactionListViewModel : BaseViewModel
    {

        public Xamarin.Forms.Command LoadItemsCommand { get; }

        public ObservableRangeCollection<Transaction> Transactions { get; set; }

        public Xamarin.Forms.Command<Transaction> TransactionTapped { get; }

        public AsyncCommand RefreshCommand { get; set; }


        public TransactionListViewModel()
        {
            
            Transactions = new ObservableRangeCollection<Transaction>();
            TransactionTapped = new Xamarin.Forms.Command<Transaction>(Selected);
            RefreshCommand = new AsyncCommand(GetTransactionList);
            GetTransactionListInitial();
        }

        async void Selected(Transaction transaction)
        {
            await Shell.Current.GoToAsync($"{nameof(TransactionListDetailPage)}?id={transaction.Id}");
        }

        private async Task GetTransactionList()
        {
            IsBusy = true;
            Transactions.Clear();
            var transactions = await WalletService.GetWalletTransactions();
            Transactions.AddRange(transactions);
            IsBusy = false;
        }

        private async void GetTransactionListInitial()
        {
            Transactions.Clear();
            var transactions = await WalletService.GetWalletTransactions();
            Transactions.AddRange(transactions);
        }
    }
}
