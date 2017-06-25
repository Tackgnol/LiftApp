using LiftApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftApp.ViewModels 
{
    class AddExerciseViewModel : BaseViewModel
    {
        public ObservableCollection<Set> Sets { get; private set; }
        public int SetElementsCount { get; set; }
        private readonly IPageService _pageService;
        private Set _selectedSet;
        private IWorkoutStore _workoutStore;
        private bool _isDataLoaded;



    }
}
