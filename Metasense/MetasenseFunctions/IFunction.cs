
using Metasense.Infrastructure;

namespace Metasense.MetasenseFunctions
{
    /// <summary>
    /// The template for every function exposed in Excel
    /// </summary>
    /// <typeparam name="T">The type of the object that is the result of the calculation of the object</typeparam>
    public interface IFunction<T>
    {
        /// <summary>
        /// The type of function
        /// </summary>
        Enums.FunctionType FunctionType { get; }

        /// <summary>
        /// Resolve the raw input parameters
        /// </summary>
        void ResolveInputs();

        /// <summary>
        /// Perform the actual calculation and return the declared object type
        /// </summary>
        /// <returns></returns>
        T Calculate();

        /// <summary>
        /// Convert the calculation result into a format that can ben displayed in Excel
        /// </summary>
        /// <param name="resultObject"></param>
        /// <returns></returns>
        object Render(T resultObject);
    }
}
