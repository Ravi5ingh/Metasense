using System.Collections.Generic;
using System.Linq;
using Metasense.Infrastructure;
using Metasense.Infrastructure.Functions;
using Metasense.Tabular;

namespace Metasense.MetasenseFunctions.Tabular
{
    public class Intersect : BaseFunction<object[,]>
    {
        public ExcelArg Input1 { get; set; }

        public ExcelArg Input2 { get; set; }

        private object[] input1;

        private object[] input2;

        public Intersect(Enums.FunctionType functionType) : base(functionType)
        {
        }

        public override void ResolveInputs()
        {
            input1 = Input1.As1DArray<object>();

            input2 = Input2.As1DArray<object>();
        }

        public override object[,] Calculate()
        {
            var data = input1.Where(el => input2.Contains(el)).Distinct();
            if (!data.Any())
            {
                data = new List<object> {"No common elements"};
            }
            return Table.CreateColumnFrom(data).Data;
        }
    }
}
