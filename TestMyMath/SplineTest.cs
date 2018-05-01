using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyMath.Exceptions;
using MyMath.Typen;

namespace TestMyMath
{
    [TestClass]
    public class SplineTest
    {
        

        [TestMethod]
        public void TestSpline()
        {
            TSplineCubic spline = new TSplineCubic();
        }


        [TestMethod]
        public void TestSetSpline()
        {
            TSplineCubic spline = new TSplineCubic();
            spline.X0Value = 1;
            spline.X1Value = 1;
            spline.Y0Values = new double[]{ 1,2,3};
            spline.Y1Values = new double[] { 1, 2, 3 };
        }


        [TestMethod]
        public void TestSetSplineException()
        {
            TSplineCubic spline = new TSplineCubic();
            try
            {
                spline.Y1Values = new double[] { 1, 2 };
            }
            catch (wrongDegreeException e)
            {

            }
        }


        [TestMethod]
        public void TestGetSpline()
        {
            TSplineCubic spline = new TSplineCubic();
            spline.X0Value = 1;
            spline.Coeefficiens = new double[] { 1, 2, 3, 4 };
            Func<double, double> spl = spline.GetSpline();
            if (spl(0) != -2)
                throw new Exception("Mistake in calculation");
        }

        [TestMethod]
        public void TestGetSplineFirstDerivat()
        {
            TSplineCubic spline = new TSplineCubic();
            spline.X0Value = 1;
            spline.Coeefficiens = new double[] { 1, 2, 3, 4 };
            Func<double, double> spl = spline.GetSplineFirstDerivat();
            if (spl(0) != 8)
                throw new Exception("Mistake in calculation" + spl(0));
        }

        [TestMethod]
        public void TestGetSplineSecondDerivat()
        {
            TSplineCubic spline = new TSplineCubic();
            spline.X0Value = 1;
            spline.Coeefficiens = new double[] { 1, 2, 3, 4 };
            Func<double, double> spl = spline.GetSplineSecondDerivat();
            if (spl(0) != -18)
                throw new Exception("Mistake in calculation" + spl(0));
        }
    }
}
