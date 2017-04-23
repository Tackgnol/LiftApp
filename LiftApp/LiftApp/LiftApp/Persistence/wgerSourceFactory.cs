using System.Collections.Generic;
using LiftApp.Models;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System;

namespace LiftApp.Persistence
{


    public class wgerSourceFactory : AbstractSourceFactory
    {


        private class _response<T>
        {
            public int count { get; set; }
            public string next { get; set; }
            public string previous { get; set; }
            public List<T> results { get; set; }
        }

        private class _wgerExercise
        {
            public int id { get; set; }
            public string license_author { get; set; }
            public int status { get; set; }
            public string description { get; set; }
            public string name { get; set; }
            public string name_original { get; set; }
            public string creation_date { get; set; }
            public string uuid { get; set; }
            public int license { get; set; }
            public int category { get; set; }
            public int language { get; set; }
            public List<int> muscles { get; set; }
            public List<int> muscles_secondary { get; set; }
            public List<int> equipment { get; set; }

        }
        private class _wgerMuscle
        {
            public int id { get; set; }
            public string name { get; set; }
            public bool is_front { get; set; }
        }

        private string _ExerciseURL = "https://wger.de/api/v2/exercise/?format=json&language=2";
        private string _MuscleURL = "https://wger.de/api/v2/muscle/?format=json";
        private string _next;
        private List<ModelExercise> _exercises = new List<ModelExercise>();
        private List<_wgerExercise> _wgerExercises = new List<_wgerExercise>();
        private List<Muscle> _muscles = new List<Muscle>();
        private List<MuscleExerciseAssosiation> _muscleAssosiations = new List<MuscleExerciseAssosiation>();
        private HttpClient _client = new HttpClient();

        public async override Task<List<Muscle>> MuscleGet()
        {
            var content = await _client.GetStringAsync(_MuscleURL);
            var muscles = await ParseJSON<_wgerMuscle>();
            while (_next != null)
            {
                muscles.AddRange(await ParseJSON<_wgerMuscle>(_next));
            }
            foreach (var muscle in muscles)
            {
                _muscles.Add(
                    new Muscle
                    {
                        Id = muscle.id,
                        Name = muscle.name
                    });
            }
            return _muscles;
        }

        public async override Task<List<ModelExercise>> ExercisesGet()
        {

            var _wgerExercises = await ParseJSON<_wgerExercise>(_ExerciseURL);

            while (_next != null)
            {
                _wgerExercises.AddRange(await ParseJSON<_wgerExercise>(_next));
            }
            foreach (var exercise in _wgerExercises)
            {
                _exercises.Add(
                    new ModelExercise
                    {
                        Id = exercise.id,
                        Name = exercise.name,
                        Description = exercise.description,
                        UserGenerated = false
                    }
                    );
            }

            return _exercises;
        }
        public override List<MuscleExerciseAssosiation> AssosiationGet()
        {
            if (_wgerExercises.Count == 0 || _muscles.Count == 0)
            {
                return new List<MuscleExerciseAssosiation>();
            }
            foreach (var exercise in _wgerExercises)
            {
                foreach (var muscle in exercise.muscles)
                {
                    _muscleAssosiations.Add(
                        new MuscleExerciseAssosiation
                        {
                            ModelExerciseId = exercise.id,
                            MuscleId = muscle,
                            MuslceType = "Primary"
                        }
                        );
                }
                foreach (var muscle in exercise.muscles_secondary)
                {
                    _muscleAssosiations.Add(
                    new MuscleExerciseAssosiation
                    {
                        ModelExerciseId = exercise.id,
                        MuscleId = muscle,
                        MuslceType = "Secondary"
                    }
                    );
                }
            }
            return _muscleAssosiations;
        }
        private async Task<List<T>> ParseJSON<T>(string url = null)
        {
            _next = null;
            if (url == null)
            {
                url = _ExerciseURL;
            }
            var content = await _client.GetStringAsync(url);
            var response = JsonConvert.DeserializeObject<_response<T>>(content);
            var results = response.results;
            _next = response.next;

            return results;
        }



    }

}
