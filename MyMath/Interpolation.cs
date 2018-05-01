using Mathematik;
using MyMath.Exceptions;
using MyMath.Interfeces;
using MyMath.Spline;
using MyMath.Typen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMath
{

    public class Interpolation
    {
        List<TDataPoint> _values = new List<TDataPoint>();
        ISplineCondition _condition = new NaturalSpline();

        public void AddValue(TDataPoint value)
        {
            _values.Add(value);
        }


        public Interpolation()
        {
           
        }

        public Interpolation(ISplineCondition cond)
        {
            _condition = cond;
        }

        private double deltaXPow(TDataPoint x0, TDataPoint x1, int pow)
        {
            return Math.Pow(x1.XValue - x0.XValue,pow);
        }

        /// <summary>
        /// Creates a (n x n+1) -matrix, with entries refering to the added values and interpolation conditions of a cubic spline
        /// </summary>
        /// <returns></returns>
        public Matrix GetConditionMatrix()
        {
            if (_values.Count < 2)
                throw new wrongCountOfElementsException();

            int dataCount = _values.Count;
            int dimension = 4 * (dataCount - 1);

            Matrix result = new Matrix( dimension , dimension + 1);

            int firstConditionEntries = 2 * (dataCount - 1);

            for (int zeile = 0; zeile < firstConditionEntries / 2; zeile++)
            {
                result.addValue(
                    X: zeile * 4,
                    Y: zeile,
                    value: 1);
                result.addValue(
                    X: dimension ,
                    Y: zeile,
                    value: _values[zeile].YValue
                    );
            }

            for (int zeile = 0; zeile < firstConditionEntries / 2 ; zeile++)
            {
                for (int spalte = 0; spalte < 4; spalte++)
                {
                    result.addValue(
                        X: spalte + zeile * 4,
                        Y: zeile + firstConditionEntries / 2,
                        value: deltaXPow(_values[zeile], _values[zeile + 1], spalte));
                }
                result.addValue(
                    X: dimension ,
                    Y: zeile + firstConditionEntries / 2,
                    value: _values[zeile+1].YValue
                    );
            }   

            int secondConditionEntries = (dataCount - 2);

            for ( int zeile = 0; zeile < secondConditionEntries ; zeile ++)
            {
                for (int spalte = 0; spalte < 4; spalte++)
                    result.addValue(
                        X: spalte + zeile * 4,
                        Y: zeile + firstConditionEntries,
                        value: (double)spalte * deltaXPow(_values[zeile], _values[zeile+1], spalte-1));
                result.addValue(
                        X: 5 + zeile * 4,
                        Y: zeile + firstConditionEntries,
                        value: -1);
            }

            int thirdConditionEntries = secondConditionEntries;

            for (int zeile = 0; zeile < thirdConditionEntries  ; zeile++)
            {
                for (int spalte = 0; spalte < 4; spalte++)
                    result.addValue(
                        X: spalte + zeile * 4,
                        Y: zeile + firstConditionEntries + secondConditionEntries,
                        value: (double)spalte  * (double)(spalte - 1) * deltaXPow(_values[zeile], _values[zeile + 1], spalte - 2) / 2);

                result.addValue(
                        X: 6 + zeile * 4,
                        Y: zeile + firstConditionEntries + secondConditionEntries,
                        value: -1);
            }
            _condition.AddConditionalValues(ref result);
            return result;
        }

        public Matrix ConditionMatrix()
        {
            return GetConditionMatrix().removeRow(4 * (_values.Count - 1) - 1);
        }

        public Matrix SolutionRow()
        {
            Matrix cond = GetConditionMatrix();
            Matrix result = cond.getRow(cond.Values.GetLength(0));
            return result;
        }
        
        public List<List<double>> GetCoeefficients()
        {
            Matrix temp = new Matrix(4 * (_values.Count - 1), 1);
            Matrix solutuion = SolutionRow();
            Matrix invers = ConditionMatrix().invert();
            
            temp = ( solutuion * invers);
            List<List<double>> result = new List<List<double>>();
            for (int i = 0; i < temp.Values.GetLength(0); i = i + 4)
                result.Add(new List<double>()
                {
                    temp.Values[i,0],
                    temp.Values[i+1,0],
                    temp.Values[i+2,0],
                    temp.Values[i+3,0]
                });
            return result;
        }
    }
}
