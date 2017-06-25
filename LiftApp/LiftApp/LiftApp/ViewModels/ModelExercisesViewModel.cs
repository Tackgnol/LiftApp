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
        public event EventHandler<Exercise> ExerciseAdded;


        public ObservableCollection<ModelExercise> ModelExercises { get; private set; } = new ObservableCollection<ModelExercise>();
        public ObservableCollection<Muscle> Muscles { get; set; } = new ObservableCollection<Muscle>();
        public Muscle SelectedMuscle { get; set; }
        public ModelExercise SelectedExercise
        {
            get { return _selectedExercise; }
            set { SetValue(ref _selectedExercise, value); }
        }
        private ModelExercise _selectedExercise = null;
        private readonly IPageService _pageService;
        private IWorkoutStore _workoutStore;
        private bool _isDataLoaded;
        private List<ModelExerciseViewModel> _ModelExercises = new List<ModelExerciseViewModel>();

        public ICommand RefreshDatabaseCommand { get; private set; }
        public ICommand LoadDataCommand { get; private set; }
        public ICommand DropModelExercisesCommand { get; private set; }
        public ICommand ReloadDataCommand { get; private set; }
        public ICommand SelectExerciseCommand { get; private set; }
        private IEqualityComparer<Muscle> _compareMuscle { get; set; } = new MuscleComparer();

        private class MuscleComparer : IEqualityComparer<Muscle>
        {
            public bool Equals(Muscle x, Muscle y)
            {
                return GetHashCode(x) == GetHashCode(y);
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
            ReloadDataCommand = new Command(async () => await ReloadData());
            DropModelExercisesCommand = new Command(async () => await DropModelExercises());
            SelectExerciseCommand = new Command<ModelExercise>(async (e) => await SelectExercise(e));
        }

        private async Task LoadData()
        {
            if (_isDataLoaded)
                return;
            ModelExercises.Clear();
            _isDataLoaded = true;

            var modelExercises = await _workoutStore.GetModelExercisesAsync();


            IEnumerable<Muscle> muscles = new List<Muscle>();
            if (SelectedMuscle != null)
            {
                var assosiatedExercises = await _workoutStore.GetMuscleExerciseAssosiations("Primary", SelectedMuscle.Id);
                foreach (var assosiation in assosiatedExercises)
                {
                    ModelExercises.Add(modelExercises.Where(e => e.Id == assosiation.ModelExerciseId).SingleOrDefault());
                }
            }
            else
            {
                muscles = await _workoutStore.GetMusclesAsync();

                foreach (var muscle in muscles)
                {
                    Muscles.Add(muscle);
                }
                foreach (var exercise in modelExercises)
                {
                    ModelExercises.Add(exercise);
                }

            }


        }
        private async Task ReloadData()
        {
            _isDataLoaded = false;
            await LoadData();
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

        private async Task SelectExercise(ModelExercise exercise)
        {
            _selectedExercise = exercise;
            await _pageService.PopAsync();
        }

    }
}
