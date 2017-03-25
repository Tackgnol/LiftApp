﻿using LiftApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LiftApp.ViewModels
{
    public class WorkoutViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public bool IsTemplate { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public string ImageDirectory { get; set; }
        public WorkOutType Type { get; set; }
        public ObservableCollection<Exercise> Exercises { get; set; }
        private static IWorkoutStore _workoutStore;
        private static IPageService _pageService;
        private Workout _workout;
        public ICommand SaveWorkoutCommand { get; private set; }
        public ICommand AddExerciseCommand { get; private set; }

        private WorkoutViewModel(Workout workout)
        {
            Id = workout.Id;
            IsTemplate = workout.IsTemplate;
            Date = workout.Date;
            Name = workout.Name;
            Description = workout.Description;
            Comments = workout.Comments;
            ImageDirectory = workout.ImageDirectory;
            _workout = workout;
            SaveWorkoutCommand = new Command(async () => await SaveWorkout());
            AddExerciseCommand = new Command(async () => await AddExercise());

        }

        private async Task<WorkoutViewModel> GetWorkoutObjects(Workout workout)
        {
            Type = await _workoutStore.GetWorkoutType(workout.TypeId);
            var exercises = await _workoutStore.GetRelatedExercises(Id);
            foreach (var ex in exercises)
            {
                Exercises.Add(ex);
            }
            return this;
        }

        public static Task<WorkoutViewModel> CreateAsync(Workout workout, IWorkoutStore workoutStore, IPageService pageService)
        {
            _workoutStore = workoutStore;
            _pageService = pageService;
            var ret = new WorkoutViewModel(workout);
            return ret.GetWorkoutObjects(workout);
        }
        private async Task SaveWorkout()
        {
            var workoutToAdd = new Workout();
            workoutToAdd.Name = Name;
            workoutToAdd.Description = Description;
            workoutToAdd.Date = Date;
            await _workoutStore.AddWorkout(workoutToAdd);
            await _pageService.PopAsync();            
        }

        private async Task AddExercise()
        {
            await _pageService.PushAsync(new ExercisesOverwievPage());
        }

    }
}
