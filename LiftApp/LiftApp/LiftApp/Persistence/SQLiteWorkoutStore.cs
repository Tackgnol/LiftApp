using HelloWorld;
using LiftApp.Models;
using LiftApp.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftApp.Persistence
{
    public class SQLiteWorkoutStore : IWorkoutStore
    {
        private SQLiteAsyncConnection _connection;
        public SQLiteWorkoutStore(ISQLiteDb db)
        {
            _connection = db.GetConnection();
            _connection.CreateTableAsync<Workout>();
            _connection.CreateTableAsync<WorkOutType>();
            _connection.CreateTableAsync<Exercise>();
        }

        public async Task AddWorkout(Workout workout)
        {
            await _connection.InsertAsync(workout);
        }

        public async Task DeleteWorkout(Workout workout)
        {
            await _connection.DeleteAsync(workout);
        }

        public async Task<IEnumerable<Exercise>> GetRelatedExercises(int workoutId)
        {
            var exercises = _connection.Table<Exercise>();
            var selectedExercises = from e in exercises
                                    where e.WorkoutId == workoutId
                                    select e;
            
            return await selectedExercises.ToListAsync();
        }

        public async Task<Workout> GetWorkout(int id)
        {
            return await _connection.FindAsync<Workout>(id);
        }

        public async Task<IEnumerable<Workout>> GetWorkoutsAsync()
        {
            return await _connection.Table<Workout>().ToListAsync();
        }

        public async Task<WorkOutType> GetWorkoutType(int typeId)
        {
            return await _connection.FindAsync<WorkOutType>(typeId);
        }

        public async Task UpdateWorkout(Workout workout)
        {
           await _connection.UpdateAsync(workout);
        }


    }
}
