﻿using LiftApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LiftApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutsOverview : ContentPage
    {
        public WorkoutsOverview()
        {
            BindingContext = new WorkoutsOverviewViewModel();
            InitializeComponent();
        }
    }
}