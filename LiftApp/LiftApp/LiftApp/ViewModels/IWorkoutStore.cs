using LiftApp.Models;
using LiftApp.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftApp.ViewModels
{
    public interface IWorkoutStore
    {
        Task<IEnumerable<Workout>> GetWorkoutsAsync();
        Task<Workout> GetWorkout(int id);
        Task<WorkOutType> GetWorkoutType(int typeId);
        Task<IEnumerable<Exercise>> GetRelatedExercises(int workoutId);
        Task<List<Muscle>> GetMusclesForModelExercise(int exerciseId, string muscleType);
        Task AddWorkout(Workout workout);
        Task UpdateWorkout(Workout workout);
        Task DeleteWorkout(Workout workout);
        Task BuildMuscleDatabase(AbstractSourceFactory factory);
        Task<IEnumerable<ModelExercise>> GetModelExercisesAsync();
        Task<IEnumerable<Muscle>> GetMusclesAsync();
        Task<IEnumerable<MuscleExerciseAssosiation>> GetMuscleExerciseAssosiations(string muscleType, int? muscleID);
        Task DropModelExercises();
    }
}
