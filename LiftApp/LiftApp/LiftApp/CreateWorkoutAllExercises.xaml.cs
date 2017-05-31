using HelloWorld;
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
    public partial class CreateWorkoutAllExercises : ContentPage
    {
        public CreateWorkoutAllExercises()
        {
            var workoutStore = new SQLiteWorkoutStore(DependencyService.Get<ISQLiteDb>());
            var pageService = new PageService();
            ViewModel = new ModelExercisesViewModel(workoutStore, pageService);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ViewModel.LoadDataCommand.Execute(null);
            base.OnAppearing();
        }
        public ModelExercisesViewModel ViewModel
        {
            get { return BindingContext as ModelExercisesViewModel; }
            set { BindingContext = value; }
        }
        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewModel.ReloadDataCommand.Execute(null);
        }
    }
}
