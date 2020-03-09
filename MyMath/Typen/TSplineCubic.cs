using MyMath.Exceptions;
using MyMath.Interfaces;
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
    public class TSplineCubic: ASpline
    {
        
        public TSplineCubic()
        {
            this.Degree = 3;
        }

       
        

    }
}
