using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftApp.Models
{
    public class RepElement : Element<Rep>
    {
        public Rep r;

        public override Rep GetContent()
        {
            return r;
        }
    }
}
