using MyMath.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMath.Interfaces
{
    public abstract class ASpline
    {
        protected List<double> _Coeefficiens = new List<double>();

        public double X0Value { get; set; }
        public double X1Value { get; set; }
        public double Y0Value { get; set; }
        public double Y1Value { get; set; }


        public List<double> Coeefficiens
        {
            get
            {
                return _Coeefficiens;
            }
            set
            {
                if (value.Count == Degree + 1)
                    this._Coeefficiens = value;
                else
                    throw new wrongDegreeException();
            }

        }

        public int Degree { get; protected set; }

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
                    result += i * _Coeefficiens[i] * Math.Pow(x - X0Value, i - 1);
                return result;
            };
        }

        public Func<double, double> GetSplineSecondDerivat()
        {
            return (double x) =>
            {
                double result = 0;
                for (int i = 0; i < Degree + 1; i++)
                    result += i * (i - 1) * _Coeefficiens[i] * Math.Pow(x - X0Value, i - 2);
                return result;
            };
        }




    }
}
