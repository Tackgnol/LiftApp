using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftApp.Models
{
    public class Workout
    {
        public bool IsTemplate { get; set; }
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [ForeignKey(typeof(WorkOutType))]
        public int TypeId { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public string ImageDirectory { get; set; }
        //public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
