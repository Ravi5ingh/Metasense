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
