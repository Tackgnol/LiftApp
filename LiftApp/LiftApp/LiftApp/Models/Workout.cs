using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftApp.Models
{
    class Workout
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Comments { get; set; }
        public string ImageDirectory { get; set; }
        public virtual WorkOutType Type { get; set; }
        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
