using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metasense
{
    /// <summary>
    /// General extension methods
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Convert the given array to a jagged array
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="arr">This array</param>
        /// <returns>Return a jagged array</returns>
        public static T[][] AsJagged<T>(this T[,] arr)
        {
            var rows = arr.GetLength(0);
            var cols = arr.GetLength(1);
            var retVal = new T[rows][];

            for (var i = 0; i < rows; i++)
            {
                retVal[i] = new T[cols];
                for (var j = 0; j < cols; j++)
                {
                    retVal[i][j] = arr[i, j];
                }
            }

            return retVal;
        }

        /// <summary>
        /// Gets the biggest value in a non-empty array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <returns></returns>
        /// TODO : Make this generic for IComparables
        public static double Max(this double[,] arr)
        {
            if (arr.GetLength(0) == 0 || arr.GetLength(1) == 0)
            {
                throw new ArgumentException("Array must have positive dimensions");
            }

            var max = arr[0, 0];
            for (var i = 0; i < arr.GetLength(0); i++)
            {
                for (var j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] > max)
                    {
                        max = arr[i, j];
                    }
                }
            }

            return max;
        }

        /// <summary>
        /// Convert a jagged array to a 2D array
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="jaggedArr">The jagged array</param>
        /// <returns>The 2D array</returns>
        public static T[,] As2DArray<T>(this T[][] jaggedArr)
        {
            var rows = jaggedArr.Length;
            var cols = jaggedArr.Max(row => row.Length);

            var retVal = new T[rows, cols];

            for(var i = 0; i < rows; i++)
            {
                for(var j = 0; j < cols; j++)
                {
                    retVal[i, j] = jaggedArr[i][j];
                }
            }

            return retVal;
        }

        /// <summary>
        /// Converts a set of arrays into a 2D array. The potential jagged-ness means some elements in the output will be the default value of <see cref="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerableOfArrays"></param>
        /// <returns></returns>
        public static T[,] As2DArray<T>(this IEnumerable<T[]> enumerableOfArrays)
        {
            var list = enumerableOfArrays.ToList();
            var numRows = list.Count();
            var numCols = list.Max(arr => arr.Length);
            var retVal = new T[numRows, numCols];

            for (var i = 0; i < numRows; i++)
            {
                for (var j = 0; j < list[i].Length; j++)
                {
                    retVal[i, j] = list[i][j];
                }
            }

            return retVal;
        }

        public static T[,] As2DRow<T>(this IEnumerable<T> enumerable)
        {
            return null;
        }

        public static Y[,] As2DColumn<Y,T>(this IEnumerable<T> enumerable)
        {
            return null;
        }

        /// <summary>
        /// Aggregate the contents into a human readable string
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string Aggregate(this IEnumerable<string> arr)
        {
            var retVal = string.Empty;
            foreach (var element in arr)
            {
                retVal = retVal + ", " + element;
            }
            return retVal.Length > 2 ? retVal.Substring(2) : retVal;
        }
    }
}
