using System;
using Metasense.Infrastructure;
using Metasense.Infrastructure.Functions;
using Metasense.Infrastructure.Tabular;

namespace Metasense.MetasenseFunctions.Tabular
{
    public class CropTimeSeries : BaseFunction<TimeSeries>
    {
        public ExcelArg Name { get; set; }

        public ExcelArg InputTimeSeries { get; set; }

        public ExcelArg Start { get; set; }

        public ExcelArg End { get; set; }

        private string name;

        private TimeSeries inputTimeSeries;

        private DateTime startDate;

        private DateTime endDate;

        public CropTimeSeries(Enums.FunctionType functionType) : base(functionType)
        {
        }

        public override void ResolveInputs()
        {
            name = Name.AsString();

            inputTimeSeries = InputTimeSeries.GetFromStoreAs<TimeSeries>();

            startDate = Start.AsDateTime();

            endDate = End.AsDateTime();
        }

        public override TimeSeries Calculate()
        {
            return inputTimeSeries.Crop(startDate, endDate);
        }

        public override object Render(TimeSeries resultObject)
        {
            return ObjectStore.Add(name, resultObject);
        }
    }
}
