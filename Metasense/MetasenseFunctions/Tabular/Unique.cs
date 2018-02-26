using System.Collections.Generic;
using System.Linq;
using Metasense.Infrastructure;
using Metasense.Infrastructure.Functions;

namespace Metasense.MetasenseFunctions.Tabular
{
    public class Unique : BaseFunction<string[]>
    {

        public ExcelArg InputRange { get; set; }

        private string[,] inputRange;

        public Unique(Enums.FunctionType functionType) : base(functionType)
        {
        }

        public override void ResolveInputs()
        {
            inputRange = InputRange.As2DArray<string>();
        }

        public override string[] Calculate()
        {
            var uniqueValues = new HashSet<string>();

            foreach (var value in inputRange)
            {
                uniqueValues.Add(value);
            }

            return uniqueValues.ToArray();
        }

        /// <summary>
        /// Need to take the string array and orient it vertically for displaying in Excel
        /// </summary>
        /// <param name="resultObject"></param>
        /// <returns></returns>
        public override object Render(string[] resultObject)
        {
            var retVal = new object[resultObject.Length, 1];
            for (var i = 0; i < resultObject.Length; i++)
            {
                retVal[i, 0] = resultObject[i];
            }

            return retVal;
        }
    }
}
