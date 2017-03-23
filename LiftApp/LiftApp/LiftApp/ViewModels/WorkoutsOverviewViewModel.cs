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

        public WorkoutsOverviewViewModel(IWorkoutStore workoutStore ,IPageService pageService)
        {
            _pageService = pageService;
            _workoutStore = workoutStore;
            LoadDataCommand = new Command(async () => await LoadData());
            //AddWorkoutCommand = new Command(AddWorkout);
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
            _isDataLoaded = true;
            var workouts = await _workoutStore.GetWorkoutsAsync();
            foreach (var c in workouts)
            {
                Workouts.Add(c);
            }
            

        }
        private void AddWorkout()
        {

        }
    }
}
