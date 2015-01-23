using MathNet.Numerics;

namespace Statistics
{
    public class LineFitting
    {
        //Return example: {intercept, ax, bx^2, ... }
        public double[] FitLine(double[] x, double[] y, int order)
        {
            if (order != 1) return Fit.Polynomial(x, y, order);
            var data =  Fit.Line(x, y);
            return new[]{data.Item1, data.Item2};
        }
    }
}
