using System;
using System.Collections.Generic;
using ExcelDna.Integration;

namespace Metasense.Infrastructure.Functions
{
    public static class CalculationCache
    {
        private static IDictionary<ExcelReference, object> cache = new Dictionary<ExcelReference, object>();

        public static object GetValue(ExcelReference callingRange, Func<object> resultGetter)
        {
            if (cache.ContainsKey(callingRange))
            {
                return cache[callingRange];
            }

            cache[callingRange] = resultGetter();
            return resultGetter();
        }
    }
}
