using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMath.Typen
{
    public class TDataPoint: IComparable 
    {
        public double XValue { get; set; }
        public double YValue { get; set; }

        public TDataPoint(double xValue, double yValue)
        {
            XValue = xValue;
            YValue = yValue;
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            TDataPoint otherPoint = obj as TDataPoint;
            if (otherPoint != null)
                return this.XValue.CompareTo(otherPoint.XValue);
            else
                throw new ArgumentException("Object is not a TDataPoint");
        }
    }
}
