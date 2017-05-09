using LiftApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftApp.Models
{
    public class ExerciseGroup : List<ModelExerciseViewModel>
    {
        public ExerciseGroup(string shortTitle, string title)
        {
            Title = title;
            ShortTitle = shortTitle;
        }
        public string Title { get; set; }
        public string ShortTitle { get; set; }
    }
}
