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
        private class _response
        {
            public int count { get; set; }
            public string next { get; set; }
            public string previous { get; set; }
            public List<_wgerExercise> results { get; set; }
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
        private string _URL = "https://wger.de/api/v2/exercise/?format=api&language=2";
        private List<ModelExercise> _exercises = new List<ModelExercise>();
        private HttpClient _client = new HttpClient();
        public async override Task<List<ModelExercise>> ExercisesGet()
        {
            string next;
            next = await ParseJSON();
            while (next != null)
            {
               await ParseJSON(next);
            }
            return _exercises;
        }
        private async Task<string> ParseJSON(string url= null)
        {
            if (url == null)
            {
                url = _URL;
            }
            var content = await _client.GetStringAsync(_URL);
            var response = JsonConvert.DeserializeObject<_response>(content);
            var exercises = response.results;
            foreach (var exercise in exercises)
            {
                _exercises.Add(
                    new ModelExercise
                    {
                        Id = exercise.id,
                        Name = exercise.name,
                        Description = exercise.description,
                    }
                    );
            }
            return response.next;

        }
    }
    
}
