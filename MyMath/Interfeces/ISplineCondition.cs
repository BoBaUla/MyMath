using Mathematik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMath.Interfeces
{
    public interface ISplineCondition
    {
        void AddConditionalValues(ref Matrix matrix);
    }
}
