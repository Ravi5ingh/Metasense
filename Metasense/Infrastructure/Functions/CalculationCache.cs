using System;
using System.Collections.Generic;
using ExcelDna.Integration;

namespace Metasense.Infrastructure.Functions
{
    public static class CalculationCache
    {
        private static IDictionary<ExcelReference, object> cache = new Dictionary<ExcelReference, object>();

        public static object GetValue<T>(ExcelReference callingRange, IFunction<T> function, bool reCalculate = false)
        {
            if (!reCalculate && cache.ContainsKey(callingRange))
            {
                return cache[callingRange];
            }

            cache[callingRange] = Execute(function);
            return cache[callingRange];
        }

        /// <summary>
        /// Peform the function execution
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="function"></param>
        /// <returns></returns>
        private static object Execute<T>(IFunction<T> function)
        {
            function.ResolveInputs();

            var rawOutput = function.Calculate();

            return function.Render(rawOutput);
        }
    }
}
