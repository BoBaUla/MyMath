using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMath.Typen
{
    public class TDataPoint 
    {
        public double XValue { get; set; }
        public double YValue { get; set; }

        public TDataPoint(double xValue, double yValue)
        {
            XValue = xValue;
            YValue = yValue;
        }
    }
}
