using HelloWorld;
using LiftApp.Models;
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
    public class WorkoutsOverviewViewModel : BaseViewModel
    {


        public ObservableCollection<Workout> Workouts { get; private set; } = new ObservableCollection<Workout>();
        private readonly IPageService _pageService;
        private Workout _selectedWorkout;
        private IWorkoutStore _workoutStore;
        private bool _isDataLoaded;

        public ICommand LoadDataCommand { get; private set; }
        public ICommand AddWorkoutCommand { get; private set; }
        public ICommand DeleteWorkoutCommand { get; private set; }
        public ICommand SelectWorkoutCommand { get; private set; }
        public ICommand EditWorkoutCommand { get; private set; }

        public WorkoutsOverviewViewModel(IWorkoutStore workoutStore ,IPageService pageService)
        {
            _pageService = pageService;
            _workoutStore = workoutStore;
            LoadDataCommand = new Command(async () => await LoadData());
            AddWorkoutCommand = new Command(async () => await AddWorkout());
            EditWorkoutCommand = new Command(async () => await EditWorkout());
            DeleteWorkoutCommand = new Command(async () => await DeleteWorkout());
            SelectWorkoutCommand = new Command<Workout>(w=>SelectWorkout(w));
        }
        public Workout selectedWorkout
        {
            get { return _selectedWorkout; }
            set { SetValue(ref _selectedWorkout, value); }
        }
        private async Task LoadData()
        {
            if (_isDataLoaded)
            {
                return;
            }
            Workouts.Clear();
            _isDataLoaded = true;
            var workouts = await _workoutStore.GetWorkoutsAsync();
            foreach (var w in workouts)
            {
                Workouts.Add(w);
            }
            

        }
        private async Task AddWorkout()
        {
            await _pageService.PushAsync(new WorkoutDetailsPage());
            _isDataLoaded = false;
        }
        private void SelectWorkout(Workout workout)
        {
            _selectedWorkout = workout;
        }
        private async Task DeleteWorkout()
        {
            if (_selectedWorkout == null)
            {
                return;
            }
            if (await _pageService.DisplayAlert("Delete workout", "You are about to permamently delete a workout!", "Delete", "Cancel"))
            {
                await _workoutStore.DeleteWorkout(_selectedWorkout);
                Workouts.Remove(_selectedWorkout);
                _selectedWorkout = null;
            }

        }
        private async Task EditWorkout()
        {
            if (_selectedWorkout == null)
            {
                return;
            }
            await _pageService.PushAsync(new WorkoutDetailsPage(_selectedWorkout));
            _isDataLoaded = false;
            _selectedWorkout = null;
        }
    }
}
