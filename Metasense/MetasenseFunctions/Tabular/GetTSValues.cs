using Metasense.Infrastructure;
using Metasense.Infrastructure.Functions;
using Metasense.Infrastructure.Tabular;
using Metasense.Tabular;

namespace Metasense.MetasenseFunctions.Tabular
{
    public class GetTSValues : BaseFunction<Table>
    {
        public ExcelArg Name { get; set; }

        public ExcelArg TimeSeries { get; set; }

        private string name;

        private TimeSeries timeSeries;

        public GetTSValues(Enums.FunctionType functionType) : base(functionType)
        {
        }

        public override void ResolveInputs()
        {
            name = Name.AsString();

            timeSeries = TimeSeries.GetFromStoreAs<TimeSeries>();
        }

        public override Table Calculate()
        {
            var values = timeSeries.GetValues();

            return Table.CreateRowFrom(values);
        }

        public override object Render(Table resultObject)
        {
            return ObjectStore.Add(name, resultObject);
        }
    }
}
