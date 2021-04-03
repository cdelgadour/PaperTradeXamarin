using PaperTradeXamarin.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PaperTradeXamarin.ViewModels
{
    class TransactionViewModel
    {
        public ICommand SelectTransaction { get; }
        public TransactionViewModel()
        {
            SelectTransaction = new Command<string>((x) => Select(x));
        }

        async void Select(string id)
        {
            //Application.Current.MainPage.Navigation.PushAsync(new TransactionExecution());
            await Shell.Current.GoToAsync($"{nameof(TransactionExecution)}?id={id}");
        }
    }
}
