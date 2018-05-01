using MyMath.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMath.Typen
{
   

    /// <summary>
    /// Class to describe splines with maximum degree of 2
    /// </summary>
    public class TSplineCubic
    {
        private double[] _Y0Values;
        private double[] _Y1Values;
        private double[] _Coeefficiens = { 0, 0, 0, 0 };

        public TSplineCubic()
        {
                Degree = 3;
        }

        public double X0Value { get; set; }
        public double X1Value { get; set; }
        /// <summary>
        /// The index i represents the value of the i-the derivation at y0
        /// </summary>
        public double[] Y0Values
        {
            get
            {
                return _Y0Values;
            }
            set
            {
                if (value.Length == Degree)
                    this._Y0Values = value;
                else
                    throw new wrongDegreeException();
            }
        }

        /// <summary>
        /// The index i represents the value of the i-the derivation at y1
        /// </summary>
        public double[] Y1Values
        {
            get
            {
                return _Y0Values;
            }
            set
            {
                if (value.Length == Degree)
                    this._Y0Values = value;
                else
                    throw new wrongDegreeException();
            }
        }

        public double[] Coeefficiens {
            get
            {
                return _Coeefficiens;
            }
            set
            {
                if (value.Length == Degree + 1)
                    this._Coeefficiens = value;
                else
                    throw new wrongDegreeException();
            }
        }

        public int Degree { get; private set; }

        public Func<double, double> GetSpline()
        {
            return (double x) =>
            {
                double result = 0;
                for (int i = 0; i < Degree + 1; i++)
                    result += _Coeefficiens[i] * Math.Pow(x - X0Value, i);
                return result;
            };
        }

        public Func<double, double> GetSplineFirstDerivat()
        {
            return (double x) =>
            {
                double result = 0;
                for (int i = 0; i < Degree + 1; i++)
                    result += i * _Coeefficiens[i] * Math.Pow(x - X0Value, i-1);
                return result;
            };
        }

        public Func<double, double> GetSplineSecondDerivat()
        {
            return (double x) =>
            {
                double result = 0;
                for (int i = 0; i < Degree + 1; i++)
                    result += i * (i-1) * _Coeefficiens[i] * Math.Pow(x - X0Value, i-2);
                return result;
            };
        }
        

    }
}
