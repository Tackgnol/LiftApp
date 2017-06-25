using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftApp.Models
{
    public class SetPart
    {
        public int Id { get; set; }
        public string Unit { get; set; }
        public double Amount { get; set; }
        public int SetId { get; set; }
    }
}
