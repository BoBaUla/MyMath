using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMath.Typen
{
    public class TSpline
    {
        public double X0Value { get; set; }
        public double X1Value { get; set; }
        /// <summary>
        /// The index i represents the value of the i-the derivation at y0
        /// </summary>
        public double[] Y0Values { get; set; }

        /// <summary>
        /// The index i represents the value of the i-the derivation at y1
        /// </summary>
        public double[] Y1Values { get; set; }

        public int Degree { get; private set; }

        public TSpline(int degree)
        {
            Degree = degree;
        }




    }
}
