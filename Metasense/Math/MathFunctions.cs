using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metasense.Math
{
    /// <summary>
    /// Math functions
    /// </summary>
    public static class MathFunctions
    {
        /// <summary>
        /// Fits a 1-D Gaussian to a scatter plot with the given x and y values. Returns a tuple of a, mean, standard deviation
        /// </summary>
        /// <remarks>
        /// This implementation needs to be optimized cuz the way a is calculated is pretty retarded
        /// </remarks>
        /// <param name="xValues"></param>
        /// <param name="yValues"></param>
        /// <returns></returns>
        public static Tuple<double, double, double> FitGaussianToPlot(double[] xValues, double[] yValues)
        {
            //Calc Mean and Std Dev
            if (xValues.Length != yValues.Length)
            {
                throw new ArgumentException("The same number of x and y values need to be provided");
            }
            var mean = xValues.Zip(yValues, (x, y) => x * y).Sum() / yValues.Sum();
            var variance = xValues.Zip(yValues, (x, y) => sq(x - mean) * y).Sum() / yValues.Sum();
            var stdDev = System.Math.Sqrt(variance);

            //Iteratively deduce 'a'
            var aRange = new Tuple<int, int>(0, (int)(yValues.Max() + 1));
            var errToAMapping = new Dictionary<double, int>();
            foreach (var aValue in GetValuesInBetween(aRange.Item1, aRange.Item2, 50))
            {
                var error = CalcMSE(x => Gaussian(aValue, mean, stdDev, x),
                    xValues,
                    yValues);
                errToAMapping.Add(error, aValue);
            }
            var minA = errToAMapping[errToAMapping.Min(kvp => kvp.Key)];

            return new Tuple<double, double, double>(minA, mean, stdDev);
        }

        /// <summary>
        /// Calculate the mean squared error between the function and the actual value (y values) given the x values
        /// </summary>
        /// <param name="function"></param>
        /// <param name="xValues"></param>
        /// <param name="yValues"></param>
        /// <returns></returns>
        public static double CalcMSE(Func<double, double> function, double[] xValues, double[] yValues)
        {
            if (xValues.Length != yValues.Length)
            {
                throw new ArgumentException("x and y values need to have the same length");
            }

            return xValues.Zip(yValues, (x, y) => System.Math.Pow(function(x) - y, 2)).Sum();
        }

        public static int[] GetValuesInBetween(int from, int to, int numberOfValues)
        {
            if (from >= to)
            {
                throw new ArgumentException($"to ({to}) needs to be more than from ({from})");
            }
            if (numberOfValues > (to - from) + 1)
            {
                throw new ArgumentException($"There are less than {numberOfValues} discrete numbers between {from} and {to}");
            }

            var retVal = new int[numberOfValues];
            var inc = (to - from) / numberOfValues;
            var current = from;
            for (var i = 0; i < retVal.Length; i++)
            {
                retVal[i] = current;
                current = System.Math.Min(to, current + inc);
            }

            return retVal;
        }

        /// <summary>
        /// The Gaussian function
        /// </summary>
        /// <param name="a">The leading co-efficient</param>
        /// <param name="mean">The mean</param>
        /// <param name="standardDeviation">The standard devitation</param>
        /// <param name="x">The input value of the function</param>
        /// <returns></returns>
        public static double Gaussian(double a, double mean, double standardDeviation, double x)
        {
            return a * e(sq(x - mean) / sq(standardDeviation) * ((double)-1 / 2));
        }

        /// <summary>
        /// e to te power of
        /// </summary>
        /// <param name="exponent"></param>
        /// <returns></returns>
        private static double e(double exponent)
        {
            return System.Math.Pow(System.Math.E, exponent);
        }

        /// <summary>
        /// input squared
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static double sq(double input)
        {
            return System.Math.Pow(input, 2);
        }
    }
}
