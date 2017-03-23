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
        }

        public async Task AddWorkout(Workout workout)
        {
            await _connection.InsertAsync(workout);
        }

        public async Task DeleteWorkout(Workout workout)
        {
            await _connection.DeleteAsync(workout);
        }

        public async Task<Workout> GetWorkout(int id)
        {
            return await _connection.FindAsync<Workout>(id);
        }

        public async Task<IEnumerable<Workout>> GetWorkoutsAsync()
        {
            return await _connection.Table<Workout>().ToListAsync();
        }

        public async Task UpdateWorkout(Workout workout)
        {
           await _connection.UpdateAsync(workout);
        }


    }
}
