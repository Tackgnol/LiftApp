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
    public partial class AddExercise : ContentPage
    {
        public AddExercise()
        {
            InitializeComponent();
        }

        private void Set_Selected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}