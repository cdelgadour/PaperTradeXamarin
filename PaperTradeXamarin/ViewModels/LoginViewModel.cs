using PaperTradeXamarin.Models;
using PaperTradeXamarin.Services;
using PaperTradeXamarin.Views;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace PaperTradeXamarin.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public string email;
        public string password;
        public ValidateUser LoginUser { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked, ValidateLogin);
            LoginUser = new ValidateUser();
            this.PropertyChanged +=
                (_, __) => LoginCommand.ChangeCanExecute();
            
        }

        private bool ValidateLogin(object arg)
        {
            return !String.IsNullOrWhiteSpace(email)
                && !String.IsNullOrWhiteSpace(password);
        }

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }
        private async void OnLoginClicked(object obj)
        {
            ValidateUser user = new ValidateUser { Email = email, Password = password };
            var res = await ValidationService.LoginUser(user);

            if (res)
            {
                Application.Current.MainPage = new AppShell();
            } else
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error", "No es valido el email/password.", "OK");
            }           
        }
    }
}
