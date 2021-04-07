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
            set {
                SetProperty(ref email, value);
            }
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }
        private async void OnLoginClicked(object obj)
        {
            ValidateUser user = new ValidateUser { Email = email, Password = password };
            User userRes = await ValidationService.LoginUser(user);

            if (userRes.Id != 0)
            {
                var properties = Xamarin.Forms.Application.Current.Properties;
                if (!properties.ContainsKey("userId"))
                {
                    properties.Add("userId", 1);
                } else
                {
                    properties["userId"] = 1;
                }
                Application.Current.MainPage = new AppShell();
            } else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No es valido el email/password.", "OK");
            }           
        }
    }
}
