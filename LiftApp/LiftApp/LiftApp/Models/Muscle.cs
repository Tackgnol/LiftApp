using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LiftApp.Models
{
    public class Muscle
    { 
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
