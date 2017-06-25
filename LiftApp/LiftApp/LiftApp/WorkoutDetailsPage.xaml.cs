using HelloWorld;
using LiftApp.Models;
using LiftApp.Persistence;
using LiftApp.ViewModels;
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
    public partial class WorkoutDetailsPage : ContentPage
    {
        public Workout _workout;
        public WorkoutDetailsPage()
        {
            _workout = new Workout();
            InitializeComponent();
        }
        public WorkoutDetailsPage(Workout workout)
        {
            _workout = workout;
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var workoutStore = new SQLiteWorkoutStore(DependencyService.Get<ISQLiteDb>());
            var pageService = new PageService();
            if(ViewModel == null)
                ViewModel = await WorkoutViewModel.CreateAsync(_workout, workoutStore, pageService);

            base.OnAppearing();
        }

        public WorkoutViewModel ViewModel
        {
            get { return BindingContext as WorkoutViewModel; }
            set { BindingContext = value; }
        }
    }
}
