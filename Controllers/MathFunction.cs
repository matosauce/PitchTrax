using System;
using System.Collections.Generic;
using System.Linq;

namespace Controllers
{
    //http://www.codeproject.com/Articles/318126/Naive-Bayes-Classifier
    public static class MathFunction
    {
        public static double Variance(this IEnumerable<double> source)
        {
            var enumerable = source as IList<double> ?? source.ToList();
            var avg = enumerable.Average();
            var d = enumerable.Aggregate(0.0, (total, next) => Math.Pow(next - avg, 2));
            return d / (enumerable.Count() - 1);
        }

        public static double Mean(this IEnumerable<double> source)
        {
            var enumerable = source as IList<double> ?? source.ToList();
            if (!enumerable.Any())
                return 0.0;

            double length = enumerable.Count();
            double sum = enumerable.Sum();
            return sum / length;
        }

        public static double NormalDist(double x, double mean, double standardDev)
        {
            var fact = standardDev * Math.Sqrt(2.0 * Math.PI);
            var expo = (x - mean) * (x - mean) / (2.0 * standardDev * standardDev);
            return Math.Exp(-expo) / fact;
        }

        public static double Normdist(double x, double mean, double standardDev, bool cumulative)
        {
            const double parts = 50000.0; //large enough to make the trapzoids small enough

            const double lowBound = 0.0;
            if (cumulative) //do integration: trapezoidal rule used here
            {
                double width = (x - lowBound) / (parts - 1.0);
                double integral = 0.0;
                for (int i = 1; i < parts - 1; i++)
                {
                    integral += 0.5 * width * (NormalDist(lowBound + width * i, mean, standardDev) +
                        (NormalDist(lowBound + width * (i + 1), mean, standardDev)));
                }
                return integral;
            }
            return NormalDist(x, mean, standardDev);
        }

        public static double SquareRoot(double source)
        {
            return Math.Sqrt(source);
        }
    }
}
