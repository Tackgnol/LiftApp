﻿using LiftApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LiftApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            ViewModel = new MainPageViewModel(new PageService());
            InitializeComponent();
        }
        public MainPageViewModel ViewModel
        {
            get { return BindingContext as MainPageViewModel; }
            set { BindingContext = value; }
        }

    }
}
