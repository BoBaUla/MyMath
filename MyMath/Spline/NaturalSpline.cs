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
            int dimY = matrix.Values.GetLength(0);
            int dimX = matrix.Values.GetLength(1);

            matrix.addValue(2, dimY - 2, 1);
            matrix.addValue(dimX - 3, dimY - 1, 1);

        }
    }
}
