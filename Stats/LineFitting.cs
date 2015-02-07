using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics;

namespace Stats
{
    public static class LineFitting
    {
        //Return example: {intercept, ax, bx^2, ... }
        //We only use the linear trend in this project so far.
        //If project stays in development, other orders will be useful.
        public static  double[] FitLine(double[] x, double[] y, int order)
        {
            if (order != 1) return Fit.Polynomial(x, y, order);
            var data =  Fit.Line(x, y);
            return new[]{data.Item1, data.Item2};
        }

        //Useful if product stays in development.
        //Not used yet.
        public static double RSquared(double[] equation, double[] xValues, double[] yValues)
        {
            var equationValues = new List<double>();
            foreach (var value in xValues)
            {
                double equationValue = 0;
                for (var i = 0; i < equation.Count(); i ++)
                {
                    equationValue += equation[i]*(Math.Pow(value,i));
                }
                equationValues.Add(equationValue);
            }
            return GoodnessOfFit.RSquared(equationValues, yValues);
        }
    }
}
