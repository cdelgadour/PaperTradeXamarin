﻿using PaperTradeXamarin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PaperTradeXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionList : ContentPage
    {
        public TransactionList()
        {
            InitializeComponent();
        }
    }
}