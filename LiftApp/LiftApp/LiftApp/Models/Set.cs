using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftApp.Models
{
    public class Set
    {
        public int Id { get; set; }
        public int Target { get; set; }
        public virtual ICollection<SetPart> Parts { get; set; }
    }
}
