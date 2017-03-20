using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftApp.Models
{
    public class TimeElement : Element<WorkTime>
    {
        public WorkTime w;
        public override WorkTime GetContent()
        {
            return w;
        }
    }
}
