using PaperTradeXamarin.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PaperTradeXamarin.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}