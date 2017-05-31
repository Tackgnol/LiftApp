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
            _connection.CreateTableAsync<ModelExercise>();
            _connection.CreateTableAsync<Muscle>();
            _connection.CreateTableAsync<MuscleExerciseAssosiation>();
        }

        public async Task AddWorkout(Workout workout)
        {
            await _connection.InsertAsync(workout);
        }
        public async Task AddModelExercise(ModelExercise modelExercise)
        {
            await _connection.InsertAsync(modelExercise);
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

        public async Task<IEnumerable<ModelExercise>> GetModelExercisesAsync()
        {
            return await _connection.Table<ModelExercise>().ToListAsync();
        }

        public async Task<List<Muscle>> GetMusclesForModelExercise(int exerciseId, string muscleType)
        {
            var musclesForExercise = await _connection.Table<MuscleExerciseAssosiation>()
                .Where(e => e.ModelExerciseId == exerciseId && e.MuslceType ==muscleType)
                .ToListAsync();
            List<Muscle> muscles = new List<Muscle>();
            foreach (var muscle in musclesForExercise)
            {
                muscles.Add(await GetMuscle(muscle.MuscleId));
            }
            return muscles;
        }

        public async Task<IEnumerable<MuscleExerciseAssosiation>> GetMuscleExerciseAssosiations(string muscleType, int? muscleID = null)
        {
            List<MuscleExerciseAssosiation> assosiations = new List<MuscleExerciseAssosiation>();
            if (muscleID == null)
            {
                assosiations = await _connection.Table<MuscleExerciseAssosiation>()
                    .Where(e=>e.MuslceType == muscleType).ToListAsync();
            }
            else
            {
                assosiations = await _connection.Table<MuscleExerciseAssosiation>()
                    .Where(e => e.MuscleId == muscleID && e.MuslceType == muscleType).ToListAsync();
            }

            return assosiations;
        }

        public async Task<WorkOutType> GetWorkoutType(int typeId)
        {
            return await _connection.FindAsync<WorkOutType>(typeId);
        }

        public async Task UpdateWorkout(Workout workout)
        {
           await _connection.UpdateAsync(workout);
        }

        public async Task BuildMuscleDatabase(AbstractSourceFactory factory)
        {
            await FillMusclesFromDataStore(factory);
            await FillExercisesFromDataStore(factory);
            await FillMuscleExerciseAssosiations(factory);
        }

        public async Task<IEnumerable<Muscle>> GetMusclesAsync()
        {
            return await _connection.Table<Muscle>().ToListAsync();
        }
        public async Task<Muscle> GetMuscle(int id)
        {
            return await _connection.FindAsync<Muscle>(id);
        }

        public async Task DropModelExercises()
        {
            await _connection.DropTableAsync<ModelExercise>();
            await _connection.DropTableAsync<Muscle>();
            await _connection.DropTableAsync<MuscleExerciseAssosiation>();
        }

        private async Task FillExercisesFromDataStore(AbstractSourceFactory factory)
        {
            List<ModelExercise> exercises = await factory.ExercisesGet();
            await _connection.InsertAllAsync(exercises);
        }

        private async Task FillMusclesFromDataStore(AbstractSourceFactory factory)
        {
            List<Muscle> muscles = await factory.MuscleGet();
            await _connection.InsertAllAsync(muscles);
        }
        private async Task FillMuscleExerciseAssosiations(AbstractSourceFactory factory)
        {
            List<MuscleExerciseAssosiation> assosiations =  factory.AssosiationGet();
            await _connection.InsertAllAsync(assosiations);
        }


    }
}
