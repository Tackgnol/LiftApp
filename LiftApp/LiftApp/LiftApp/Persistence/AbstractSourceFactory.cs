using LiftApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftApp.Persistence
{
    public abstract class AbstractSourceFactory
    {
        public abstract Task<List<ModelExercise>> ExercisesGet();
        public abstract Task<List<Muscle>> MuscleGet();
        public abstract List<MuscleExerciseAssosiation> AssosiationGet();
    }
}
