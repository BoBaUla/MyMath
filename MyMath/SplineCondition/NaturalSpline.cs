using Mathematik;
using MyMath.Interfeces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMath.Spline
{
    class NaturalSpline : ISplineCondition
    {
        public void AddConditionalValues(ref Matrix matrix)
        {
            int dimY = matrix.GetColumns();
            int dimX = matrix.GetRows();

            matrix.AddValue(dimY - 2 ,        2, 1);
            matrix.AddValue( dimY - 1, dimX - 3, 1);

        }
    }
}
