using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftApp.Models
{
    public class Exercise
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int ModelExerciseId { get; set; }
        public int WorkoutId { get; set; }
        //public virtual ICollection<Set> Sets { get; set; }
        //public virtual ModelExercise Excercise { get; set; }
    }
}
