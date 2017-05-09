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
        public ObservableCollection< ModelExerciseViewModel> ModelExercises { get; private set; } = new ObservableCollection<ModelExerciseViewModel>();
        public ObservableCollection<ExerciseGroup> GroupedExercises { get; private set; } = new ObservableCollection<ExerciseGroup>();
        public ObservableCollection<Muscle> Muscles { get; set; } = new ObservableCollection<Muscle>();
        public string SelectedMuscle { get; set; } = null;
        private readonly IPageService _pageService;
        private IWorkoutStore _workoutStore;
        private bool _isDataLoaded;
        
        public ICommand RefreshDatabaseCommand { get; private set; }
        public ICommand LoadDataCommand { get; private set; }
        public ICommand DropModelExercisesCommand { get; private set; }
        private IEqualityComparer<Muscle> _compareMuscle { get; set; } = new MuscleComparer();

        private class MuscleComparer : IEqualityComparer<Muscle>
        {
            public bool Equals(Muscle x, Muscle y)
            {
                bool id=false;
                bool name=false;
                if (x.Id == y.Id)
                    id = true;
                if (x.Name == y.Name)
                    name = true;
                return id && name;
            }

            public int GetHashCode(Muscle muscle)
            {
                int hCode = muscle.Id ^ muscle.Name.Length;
                return hCode.GetHashCode();
            }
        }

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
                ModelExercises.Add(await
                    ModelExerciseViewModel.CreateAsync(modelExercise, _workoutStore)
                );

            }

            var muscles = await _workoutStore.GetMusclesAsync();

            foreach (var muscle in muscles)
            {
                Muscles.Add(muscle);
            }

            if (SelectedMuscle != null)
            {
                var selectedMuscle = from m in Muscles
                                     where m.Name == SelectedMuscle
                                     select m;
                Muscles.Clear();
                Muscles.Add(selectedMuscle.SingleOrDefault());
            }

            

            foreach (var muscle in Muscles)
            {
                var currentMuscle = new ExerciseGroup(muscle.Name, muscle.Name);
  
                var foundMuscles = from e in ModelExercises
                                   where e.PrimaryMuscles.Contains(muscle, _compareMuscle)
                                   select e;
                currentMuscle.AddRange(foundMuscles);
                GroupedExercises.Add(currentMuscle);
            }

        }

        private async Task RefreshDatabase()
        {
            AbstractSourceFactory factory = new wgerSourceFactory();
            await _workoutStore.BuildMuscleDatabase(factory);
            _isDataLoaded = false;
            await LoadData();
        }

        private async Task DropModelExercises()
        {
            await _workoutStore.DropModelExercises();
            _isDataLoaded = false;
            await LoadData();
        }

    }
}
