using LiftApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LiftApp.Persistence
{
    public interface IExercisesSource
    {
        HttpClient Client { get; set; }
        string _connectionString { get; set; }
        List<Exercise> GetExercises();



    }
}
