using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftApp.Models
{
    public class MuscleExerciseAssosiation
    {
        public int MuscleId { get; set; }
        public int ModelExerciseId { get; set; }
        public string MuslceType { get; set; }
    }
}
