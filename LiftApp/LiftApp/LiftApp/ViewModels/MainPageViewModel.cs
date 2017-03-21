using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LiftApp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public ICommand GoToWorkoutsCommand { get; private set; }
        private readonly IPageService _pageService;
        public MainPageViewModel(IPageService pageService)
        {
            _pageService = pageService;
            GoToWorkoutsCommand = new Command(async () => await GoToWorkouts());
        }

        private async Task GoToWorkouts()
        {
            var newWorkoutPage = new WorkoutsOverview();
            await _pageService.PushAsync(newWorkoutPage);
        }
    }
}
