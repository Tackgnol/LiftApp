using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftApp.Models
{
    public interface ISetElement
    {
        object GetSetElement();
    }

    public abstract class Element<T> : ISetElement
    {
        public abstract T GetContent();
        object ISetElement.GetSetElement()
        {
            return GetContent();
        }
    }
}
