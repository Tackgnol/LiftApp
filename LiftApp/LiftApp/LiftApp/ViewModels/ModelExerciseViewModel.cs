using LiftApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftApp.ViewModels
{
    public class ModelExerciseViewModel
    {

        private static IWorkoutStore _workoutStore;
        private static int _id;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public List<Muscle> PrimaryMuscles { get; set; }
        public List<Muscle> SecondaryMuscles { get; set; }
        private ModelExerciseViewModel(ModelExercise exercise)
        {
            Id = exercise.Id;
            _id = Id;
            Name = exercise.Name;
            Description = exercise.Description;
            Image = exercise.Image;

        }

        private static async Task<List<Muscle>> GetMuscles(string muscleType)
        {
            return await _workoutStore.GetMusclesForModelExercise(_id, muscleType);
        }

        public static async Task<ModelExerciseViewModel> CreateAsync(ModelExercise exercise, IWorkoutStore workoutStore)
        {
            _workoutStore = workoutStore;

            ModelExerciseViewModel exerciseViewModel = new ModelExerciseViewModel(exercise);
            exerciseViewModel.PrimaryMuscles = await GetMuscles("Primary");
            exerciseViewModel.SecondaryMuscles = await GetMuscles("Secondary");
            return exerciseViewModel;
        }

    }


}
