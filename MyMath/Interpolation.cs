using Mathematik;
using Mathematik.Algorithmen;
using Mathematik.Interfaces;
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
    /// <summary>
    /// 
    /// </summary>
    public class Interpolation
    {
        List<TDataPoint> _values = new List<TDataPoint>();
        
        ISplineCondition _condition = new NaturalSpline();
        List<TSplineCubic> _spline = new List<TSplineCubic>();

        double _leftCondition = 0;
        double _rightCondition = 0;

        public int Count
        {
            get { return _values.Count; }
            private set {; }
        }

        public double Xmin
        {
            get { return _values.First().XValue; }
            private set {; }
        }

        public double Xmax
        {
            get { return _values.Last().XValue; }
            private set {; }
        }

        public AApproximationSolver<Matrix, Matrix> Solver { get; set; } = new TransposedGaussSeidelApproximation(10);
        
        public void AddValue(TDataPoint value)
        {
            if (!_values.Contains(value))
            {
                _values.Add(value);
                _values.Sort();
            }
        }



        public Interpolation() 
        {
           
        }
        
        public Interpolation(ISplineCondition cond, double leftCondition, double rightCondition)
        {
            _condition = cond;
            _leftCondition = leftCondition;
            _rightCondition = rightCondition;
        }

        private double deltaXPow(TDataPoint x0, TDataPoint x1, int pow)
        {
            return Math.Pow(x1.XValue - x0.XValue,pow);
        }

        /// <summary>
        /// Creates a (n x n+1) -matrix, with entries refering to the added values and interpolation conditions of a cubic spline
        /// </summary>
        /// <returns></returns>
        public Matrix GetEquationSystem()
        {
            if (_values.Count < 2)
                throw new wrongCountOfElementsException();

            int dataCount = _values.Count;
            int dimension = 4 * (dataCount - 1);

            Matrix result = new Matrix( dimension , dimension + 1);

            int firstConditionEntries = 2 * (dataCount - 1);

            for (int zeile = 0; zeile < firstConditionEntries / 2; zeile++)
            {
                result.AddValue(
                    zeile: zeile,
                    spalte: zeile * 4,
                    value: 1);
                result.AddValue(
                    zeile: zeile,
                    spalte: dimension ,
                    value: _values[zeile].YValue
                    );
            }

            for (int zeile = 0; zeile < firstConditionEntries / 2 ; zeile++)
            {
                for (int spalte = 0; spalte < 4; spalte++)
                {
                    result.AddValue(
                        zeile: zeile + firstConditionEntries / 2,
                        spalte: spalte + zeile * 4,
                        value: deltaXPow(_values[zeile], _values[zeile + 1], spalte));
                }
                result.AddValue(
                    zeile: zeile + firstConditionEntries / 2,
                    spalte: dimension,
                    value: _values[zeile+1].YValue
                    );
            }   

            int secondConditionEntries = (dataCount - 2);

            for ( int zeile = 0; zeile < secondConditionEntries ; zeile ++)
            {
                for (int spalte = 0; spalte < 4; spalte++)
                {
                    result.AddValue(
                          zeile: zeile + firstConditionEntries,
                          spalte: spalte + zeile * 4,
                          value: (double)spalte * deltaXPow(_values[zeile], _values[zeile + 1], spalte - 1));
                }
                result.AddValue(
                         zeile: zeile + firstConditionEntries,
                         spalte: 4 + 1 + zeile * 4,
                         value: -1);
            }

            int thirdConditionEntries = secondConditionEntries;

            for (int zeile = 0; zeile < thirdConditionEntries  ; zeile++)
            {
                for (int spalte = 0; spalte < 4; spalte++)
                {
                    result.AddValue(
                        zeile: zeile + firstConditionEntries + secondConditionEntries,
                        spalte: spalte + zeile * 4,
                        value: (double)spalte * (double)(spalte - 1) * deltaXPow(_values[zeile], _values[zeile + 1], spalte - 2) / 2);
                }
                result.AddValue(
                            zeile: zeile + firstConditionEntries + secondConditionEntries,
                            spalte: 4 + 2 + zeile * 4,
                            value: -1);
            }
            _condition.AddConditionalValues(ref result,_leftCondition,_rightCondition);
            return result;
        }

        /// <summary>
        /// n x n - Part of the n x n+1 equation system, that is given by GetEquationSystem
        /// </summary>
        /// <returns></returns>
        public Matrix GetConditionPart()
        {
            return GetEquationSystem().RemoveRow(4 * (_values.Count - 1) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>b from an Ax = b linear equation system, that is described by 'GetEquationSystem'</returns>
        public Matrix GetSolutionPart()
        {
            int dimension = 4 * (_values.Count - 1);
            Matrix result = new Matrix(dimension, 1);
            int i = 0;
            foreach (double value in this.GetEquationSystem().GetRow(dimension))
            {
                result.AddValue(i, 0, value);
                i++;
            }
            return result;
        }
        
        /// <summary>
        /// List of all coeefficients from the spline interpolation
        /// </summary>
        /// <param name="solvingAlgorthim">Algorithm to solve a linear equation system</param>
        /// <returns></returns>
        public List<List<double>> GetCoeefficients(AApproximationSolver<Matrix, Matrix> solvingAlgorthim)
        {
            Matrix temp = this.GetConditionPart();
            temp.ApproxSolver = solvingAlgorthim;

            Matrix solution = temp.Solve(this.GetSolutionPart());
            List<List<double>> result = new List<List<double>>();
            for (int i = 0; i < solution.GetColumns(); i = i + 4)
                result.Add(new List<double>()
                {
                    solution.GetValue(i  ,0),
                    solution.GetValue(i+1,0),
                    solution.GetValue(i+2,0),
                    solution.GetValue(i+3,0)
                });
            return result;
        }

        /// <summary>
        /// List of all coeefficients from the spline interpolation
        /// </summary>
        /// <returns></returns>
        public List<List<double>> GetCoeefficients()
        {
            Matrix temp = this.GetConditionPart();
            temp.ApproxSolver = Solver;
            Matrix solution = temp.Solve(this.GetSolutionPart());
            List<List<double>> result = new List<List<double>>();
            for (int i = 0; i < solution.GetColumns(); i = i + 4)
                result.Add(new List<double>()
                {
                    solution.GetValue(i  ,0),
                    solution.GetValue(i+1,0),
                    solution.GetValue(i+2,0),
                    solution.GetValue(i+3,0)
                });
            return result;
        }

        /// <summary>
        /// Interpolates the given TDataPoints by ISpline objects, that are set by initailisiation
        /// </summary>
        public void Interpolate()
        {
            _spline = new List<TSplineCubic>();
            List<List<double>> coeefficients = GetCoeefficients(Solver);
            foreach(List<double> list in coeefficients)
            {
                _spline.Add(new TSplineCubic() { Coeefficiens = list});
            }
            for (int i = 0; i < _spline.Count; i++)
            {
                _spline[i].X0Value = _values[i].XValue;
                _spline[i].Y0Value = _values[i].YValue;
                _spline[i].X1Value = _values[i+1].XValue;
                _spline[i].Y1Value = _values[i+1].YValue;
            }
        }

        public double GetValue(double x)
        {
            for (int i = 0; i < _spline.Count; i++)
                if (_spline[i].X0Value <= x && _spline[i].X1Value > x)
                    return _spline[i].GetSpline()(x);
            return double.NaN;
        }
    }
}
;