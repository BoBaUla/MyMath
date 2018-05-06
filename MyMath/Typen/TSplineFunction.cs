using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMath.Typen
{
    public class TCubicSplineFunction
    {
        private List<TSplineCubic> _SplineList = new List<TSplineCubic>();

        public TCubicSplineFunction() { }

        public void AddSpline(TSplineCubic spline)
        {
            _SplineList.Add(spline);
        }

        public double GetValue(double x)
        {
            foreach(TSplineCubic spline in _SplineList)
            {
                if (spline.X0Value <= x && spline.X1Value >= x)
                    return spline.GetSpline()(x);
            }
            return double.NaN;
        }


    }
}
