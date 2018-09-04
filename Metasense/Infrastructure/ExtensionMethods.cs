using System;
using System.Collections.Generic;
using System.Linq;
using ExcelDna.Integration;
using Microsoft.Office.Interop.Excel;

namespace Metasense.Infrastructure
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Get the number of rows in this range
        /// </summary>
        /// <param name="callingRange"></param>
        /// <returns></returns>
        public static int GetNumRows(this ExcelReference callingRange)
        {
            return callingRange.RowLast - callingRange.RowFirst + 1;
        }

        /// <summary>
        /// Get the number of columns in this range
        /// </summary>
        /// <param name="callingRange"></param>
        /// <returns></returns>
        public static int GetNumCols(this ExcelReference callingRange)
        {
            return callingRange.ColumnLast - callingRange.ColumnFirst + 1;
        }

        /// <summary>
        /// Get the formula in this range
        /// </summary>
        /// <param name="callingRange"></param>
        /// <returns></returns>
        public static string GetFormula(this ExcelReference callingRange)
        {
            var formula =
            ((Range) ((Worksheet) ((Application) ExcelDnaUtil.Application).ActiveWorkbook.ActiveSheet).Cells[
                callingRange.RowFirst + 1, callingRange.ColumnFirst + 1]).Formula as string;
            return formula.Split('=', '(')[1];
        }

        /// <summary>
        /// Is this string null or empty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

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

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
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

        /// <summary>
        /// Casts an object array to another 2d array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalArray"></param>
        /// <returns></returns>
        public static T[,] Cast2D<T>(this object[,] originalArray)
        {
            if (originalArray.GetLength(0) == 0 || originalArray.GetLength(1) == 0)
            {
                throw new ArgumentException("Cannot perform cast on array with 0 dimension lengths");
            }

            var retVal = new T[originalArray.GetLength(0), originalArray.GetLength(1)];
            for (var i = 0; i < retVal.GetLength(0); i++)
            {
                for (var j = 0; j < retVal.GetLength(1); j++)
                {
                    retVal[i, j] = (T)originalArray[i, j];
                }
            }

            return retVal;
        }

        /// <summary>
        /// Aggregate the contents into a human readable string (using ToString())
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static string AggregateReadable<T>(this IEnumerable<T> arr, string delimiter = ", ")
        {
            var retVal = string.Empty;
            foreach (var element in arr)
            {
                retVal = $"{retVal}{delimiter}{element}";
            }
            return retVal.IsNullOrEmpty() ? retVal : retVal.Substring(delimiter.Length);
        }

        /// <summary>
        /// Aggregate the contents into a human readable string (using ToString())
        /// </summary>
        /// <param name="data"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static string AggregateReadable<T>(this T[,] data, string delimiter = ", ")
        {
            var retVal = string.Empty;
            foreach (var element in data)
            {
                retVal = $"{retVal}{delimiter}{element}";
            }
            return retVal.IsNullOrEmpty() ? retVal : retVal.Substring(delimiter.Length);
        }
    }
}
