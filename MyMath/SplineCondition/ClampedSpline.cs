using Mathematik;
using MyMath.Interfeces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMath.SplineCondition
{
    public class ClampedSpline : ISplineCondition
    {


        public void AddConditionalValues(ref Matrix matrix, double leftEnd, double rightEnd)
        {
            int dimY = matrix.GetColumns();
            int dimX = matrix.GetRows();

            matrix.AddValue(dimY - 2, 1, 1);
            matrix.AddValue(dimY - 2, dimX-1, leftEnd);
            matrix.AddValue(dimY - 1, dimX - 4, 1);
            matrix.AddValue(dimY - 1, dimX - 1, rightEnd);

        }
    }
}
