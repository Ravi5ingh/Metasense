using System;
using System.Linq;
using Metasense.Infrastructure;
using Metasense.Infrastructure.Functions;
using Metasense.Tabular;

namespace Metasense.MetasenseFunctions.DateTime
{
    public class GetTimeOfDay : BaseFunction<object>
    {
        public ExcelArg Dates { get; set; }

        public ExcelArg OutputTableName { get; set; }

        private double[] dates;

        private string outputTableName;

        public GetTimeOfDay(Enums.FunctionType functionType) : base(functionType)
        {
        }

        public override void ResolveInputs()
        {
            if (Dates.IsInObjectStoreAs<Table>())
            {
                dates = Dates.GetFromStoreAs<Table>().Data.Cast<double>().ToArray();
            }
            else
            {
                dates = Dates.As1DArray<double>();
            }
        }

        public override object Calculate()
        {
            object retVal;
            dates = dates.Select(d => d - (int)d).ToArray();
            if (dates.Length == 1)
            {
                retVal = dates[0];
            }
            else
            {
                outputTableName = OutputTableName.AsString();
                retVal = Table.CreateColumnFrom(dates);
            }

            return retVal;
        }

        public override object Render(object resultObject)
        {
            object retVal;
            if (resultObject is Table)
            {
                retVal = ObjectStore.Add(outputTableName, resultObject);
            }
            else
            {
                retVal = resultObject;
            }

            return retVal;
        }
    }
}
