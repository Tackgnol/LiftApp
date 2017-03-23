using HelloWorld;
using LiftApp.Models;
using LiftApp.Persistence;
using LiftApp.ViewModels;
using SQLite;
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
            var workoutStore = new SQLiteWorkoutStore(DependencyService.Get<ISQLiteDb>());
            var pageService = new PageService();
            ViewModel = new WorkoutsOverviewViewModel(workoutStore, pageService);
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            ViewModel.LoadDataCommand.Execute(null);
            base.OnAppearing();
        }
        public WorkoutsOverviewViewModel ViewModel
        {
            get { return BindingContext as WorkoutsOverviewViewModel; }
            set { BindingContext = value; }
        }


    }
}
