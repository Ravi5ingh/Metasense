using System.Collections.Generic;
using ExcelDna.Integration;

namespace Metasense.Infrastructure.Functions
{
    public static class CalculationCache
    {
        private static IDictionary<CacheKey, object> cache = new Dictionary<CacheKey, object>();

        public static object GetValue<T>(ExcelReference callingRange, IFunction<T> function, bool reCalculate = false)
        {
            var key = CacheKey.GenerateFrom(callingRange);
            if (!reCalculate && cache.ContainsKey(key))
            {
                return cache[key];
            }

            cache[key] = Execute(function);
            return cache[key];
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

        private class CacheKey
        {
            private string functionName;

            private int rowIndex;

            private int colIndex;

            private CacheKey()
            {

            }

            public static CacheKey GenerateFrom(ExcelReference callingRange)
            {
                return new CacheKey
                {
                    rowIndex = callingRange.RowFirst,
                    colIndex = callingRange.ColumnFirst,
                    functionName = callingRange.GetFormula()
                };
            }

            public override int GetHashCode()
            {
                return functionName.GetHashCode() + rowIndex + colIndex;
            }

            public override bool Equals(object obj)
            {
                var otherHash = (obj as CacheKey)?.GetHashCode();
                return GetHashCode() == otherHash;
            }
        }
    }
}
