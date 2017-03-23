using LiftApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftApp.ViewModels
{
    class WorkoutViewModel
    {
        public int Id { get; set; }
        public bool IsTemplate { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public string ImageDirectory { get; set; }
        public virtual WorkOutType Type { get; set; }
        public virtual IEnumerable<Exercise> Exercises { get; set; }
        private WorkoutViewModel(Workout workout)
        {
            Id = workout.Id;
            IsTemplate = workout.IsTemplate;
            Date = workout.Date;
            Name = workout.Name;
            Description = workout.Description;
            Comments = workout.Comments;
            ImageDirectory = workout.ImageDirectory;
        }

        private async Task GetWorkOutTypeAsync(IWorkoutStore workoutStore, Workout workout)
        {
            var type = await workoutStore.GetWorkoutType(workout.TypeId);
            Type = type;
        }

        private async Task GetExercises(IWorkoutStore workoutStore)
        {
            Exercises = await workoutStore.GetRelatedExercises(Id);
        }

        public async Task<WorkoutViewModel> Builder(IWorkoutStore workoutStore, Workout workout)
        {
            WorkoutViewModel workoutModel = new WorkoutViewModel(workout);
            await workoutModel.GetWorkOutTypeAsync(workoutStore, workout);
            await workoutModel.GetExercises(workoutStore);
            return workoutModel;
        }
    }
}
