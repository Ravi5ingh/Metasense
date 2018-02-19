using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metasense
{
    /// <summary>
    /// The Excel Argument class facilitates argument resolution
    /// </summary>
    public class ExcelArg
    {
        /// <summary>
        /// The argument
        /// </summary>
        private object argument;

        /// <summary>
        /// The name of the argument
        /// </summary>
        private string name;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="name"></param>
        public ExcelArg(object argument, string name)
        {
            this.argument = argument;
            this.name = name;
        }

        /// <summary>
        /// Is the Excel argument empty
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return argument.GetType() == typeof(ExcelDna.Integration.ExcelMissing);
            }
        }

        /// <summary>
        /// Get from the object store, the object keyed on the value in the argument as the specified type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetFromStoreAs<T>()
        {
            return (T)ObjectStore.Get(AsString());
        }

        /// <summary>
        /// Resolve as a char
        /// </summary>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public char AsChar(char defaultValue = ' ')
        {
            var str = AsString();
            return IsEmpty || str == null ? defaultValue : str[0];
        }

        /// <summary>
        /// Get the argument as a string
        /// </summary>
        /// <returns></returns>
        public string AsString(string defaultValue = null)
        {
            return IsEmpty && defaultValue != null ? defaultValue : argument as string;
        }

        /// <summary>
        /// Get the argument as a double
        /// </summary>
        /// <returns></returns>
        public double AsDouble(double? defaultValue = null)
        {
            return IsEmpty && defaultValue != null ? (double)defaultValue : (double)argument;
        }

        /// <summary>
        /// Get the argument as a boolean
        /// </summary>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public bool AsBoolean(bool? defaultValue = null)
        {
            return IsEmpty && defaultValue != null ? (bool)defaultValue : (bool)argument;
        }

        /// <summary>
        /// Resolve the argument as a 1D data structure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T[] As1DArray<T>()
        {
            T[] retVal;
            if (argument is object[,])
            {
                var argArr = argument as object[,];
                if (argArr.GetLength(0) == 1)
                {
                    retVal = new T[argArr.GetLength(1)];
                    for (var i = 0; i < argArr.GetLength(1); i++)
                    {
                        retVal[i] = (T)argArr[0, i];
                    }
                }
                else if (argArr.GetLength(1) == 1)
                {
                    retVal = new T[argArr.GetLength(0)];
                    for (var i = 0; i < argArr.GetLength(0); i++)
                    {
                        retVal[i] = (T)argArr[i, 0];
                    }
                }
                else
                {
                    throw new ArgumentException("Parameter " + name + " cannot be resolved as a 1D data structure");
                }
            }
            else
            {
                retVal = new T[1];
                retVal[0] = (T)argument;
            }

            return retVal;
        }

        /// <summary>
        /// Get the parameter as an array of the specified type
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <returns>The converted array</returns>
        public T[,] As2DArray<T>()
        {
            T[,] retVal;
            if (argument is object[,])
            {
                var argObjArr = argument as object[,];
                retVal = new T[argObjArr.GetLength(0), argObjArr.GetLength(1)];
                for (var i = 0; i < argObjArr.GetLength(0); i++)
                {
                    for (var j = 0; j < argObjArr.GetLength(1); j++)
                    {
                        try
                        {
                            retVal[i, j] = (T)argObjArr[i, j];
                        }
                        catch (Exception exp)
                        {
                            throw new ArgumentException("Element at [" + i + "][" + j + "] in parameter " + name +
                                                        " cannot be converted from " + argObjArr[i, j].GetType().ToString() +
                                                        " to " + typeof(T).GetType().ToString());
                        }
                    }
                }
            }
            else
            {
                retVal = new T[1, 1];
                retVal[0, 0] = (T)argument;
            }

            return retVal;
        }
    }
}
