using LiftApp.Models;
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
        Task AddWorkout(Workout workout);
        Task UpdateWorkout(Workout workout);
        Task DeleteWorkout(Workout workout);
    }
}
