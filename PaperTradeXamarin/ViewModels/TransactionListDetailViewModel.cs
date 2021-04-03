using MvvmHelpers;
using PaperTradeXamarin.Models;
using PaperTradeXamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PaperTradeXamarin.ViewModels
{
    [QueryProperty(nameof(Id), "id")]
    class TransactionListDetailViewModel : BaseViewModel
    {
        public string id;
        public string Id { get => id; set { SetProperty(ref id, value); LoadTransaction(Id); } }

        public Transaction currentTransaction;
        public ObservableRangeCollection<Transaction> TransactionList { get; set; }

        public Transaction CurrentTransaction { get => currentTransaction; set { SetProperty(ref currentTransaction, value);  } }

        public TransactionListDetailViewModel()
        {
            CurrentTransaction = new Transaction();

        }

        public async void LoadTransaction(string id)
        {
            List<Transaction> transactionList = new List<Transaction>();
            var transactions = await TransactionService.GetTransactions();
            transactionList.AddRange(transactions);

            var transaction = transactionList.Find(x => x.Id == Convert.ToInt32(id));
            CurrentTransaction = transaction;
        }
    }
}
