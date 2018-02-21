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
