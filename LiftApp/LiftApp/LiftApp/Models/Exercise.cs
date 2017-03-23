using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftApp.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public virtual ModelExercise Excercise { get; set; }
        public virtual ICollection<Set> Sets { get; set; }
    }
}
