using LiftApp.Models;
using LiftApp.Persistence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LiftApp.ViewModels
{
    public class ModelExercisesViewModel : BaseViewModel
    {
        public ObservableCollection<ModelExercise> ModelExercises { get; private set; } = new ObservableCollection<ModelExercise>();
        private readonly IPageService _pageService;
        private IWorkoutStore _workoutStore;
        private bool _isDataLoaded;
        
        public ICommand RefreshDatabaseCommand { get; private set; }
        public ICommand LoadDataCommand { get; private set; }
        public ICommand DropModelExercisesCommand { get; private set; }

        public ModelExercisesViewModel(IWorkoutStore workoutStore, IPageService pageService)
        {
            _pageService = pageService;
            _workoutStore = workoutStore;
            RefreshDatabaseCommand = new Command(async () => await RefreshDatabase());
            LoadDataCommand = new Command(async () => await LoadData());
            DropModelExercisesCommand = new Command(async () => await DropModelExercises());
        }

        private async Task LoadData()
        {
            if (_isDataLoaded)
                return;
            ModelExercises.Clear();
            _isDataLoaded = true;
            var modelExercises = await _workoutStore.GetModelExercisesAsync();
            foreach (var modelExercise in modelExercises)
            {
                ModelExercises.Add(modelExercise);
            }

        }

        private async Task RefreshDatabase()
        {
            AbstractSourceFactory factory = new wgerSourceFactory();
            await _workoutStore.FillExercisesFromDataStore(factory);
            _isDataLoaded = false;
            await LoadData();
        }

        private async Task DropModelExercises()
        {
            await _workoutStore.DropModelExercises();
        }

        private async Task GetDataFromAPI(AbstractSourceFactory factory)
        {
            await factory.ExercisesGet();
        }
    }
}
