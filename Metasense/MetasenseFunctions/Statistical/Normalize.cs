using System.Linq;
using Metasense.Infrastructure;
using Metasense.Infrastructure.Functions;
using Metasense.Tabular;

namespace Metasense.MetasenseFunctions.Statistical
{
    public class Normalize : BaseFunction<double[,]>
    {
        public ExcelArg Inputs { get; set; }

        private double[,] inputs;

        public Normalize(Enums.FunctionType functionType) : base(functionType)
        {
        }

        public override void ResolveInputs()
        {
            if (Inputs.IsInObjectStoreAs<Table>())
            {
                inputs = Inputs.GetFromStoreAs<Table>().Data.Cast2D<double>();
            }
            else
            {
                inputs = Inputs.As2DArrayOf<double>();
            }
        }

        public override double[,] Calculate()
        {
            var max = inputs.Max();
            var normalized = new double[inputs.GetLength(0), inputs.GetLength(1)];

            for (var i = 0; i < normalized.GetLength(0); i++)
            {
                for (var j = 0; j < normalized.GetLength(1); j++)
                {
                    normalized[i, j] = inputs[i, j] / max;
                }
            }

            return normalized;
        }
    }
}
