using System.Linq;
using Metasense.Infrastructure;
using Metasense.Infrastructure.Functions;
using Metasense.Infrastructure.Tabular;
using Metasense.Tabular;

namespace Metasense.MetasenseFunctions.Tabular
{
    public class GetTSDates : BaseFunction<Table>
    {
        public ExcelArg Name { get; set; }

        public ExcelArg TimeSeries { get; set; }

        private string name;

        private TimeSeries timeSeries;

        public GetTSDates(Enums.FunctionType functionType) : base(functionType)
        {
        }

        public override void ResolveInputs()
        {
            name = Name.AsString();

            timeSeries = TimeSeries.GetFromStoreAs<TimeSeries>();
        }

        public override Table Calculate()
        {
            var dates = timeSeries.GetDates().Select(date => date.ToOADate());

            return Table.CreateColumnFrom(dates);
        }

        public override object Render(Table resultObject)
        {
            return ObjectStore.Add(name, resultObject);
        }
    }
}
