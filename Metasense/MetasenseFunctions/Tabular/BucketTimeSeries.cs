using System;
using Metasense.Infrastructure;
using Metasense.Infrastructure.Functions;
using Metasense.Infrastructure.Tabular;

namespace Metasense.MetasenseFunctions.Tabular
{
    public class BucketTimeSeries : BaseFunction<TimeSeries>
    {
        #region Parameters

        public ExcelArg Name { get; set; }

        public ExcelArg InputTimeSeries { get; set; }

        public ExcelArg StartDateTime { get; set; }

        public ExcelArg EndDateTime { get; set; }

        #endregion

        #region Resolved Parameters

        public ExcelArg IntervalBucketSize { get; set; }

        private string name;

        private TimeSeries inputTimeSeries;

        private DateTime start;

        private DateTime end;

        private long intervalBucketSizeInSeconds;

        #endregion

        public BucketTimeSeries(Enums.FunctionType functionType) : base(functionType)
        {
        }

        public override void ResolveInputs()
        {
            name = Name.AsString();

            inputTimeSeries = InputTimeSeries.GetFromStoreAs<TimeSeries>();

            start = StartDateTime.AsDateTime();

            end = EndDateTime.AsDateTime();

            intervalBucketSizeInSeconds = IntervalBucketSize.AsInt();
        }

        public override TimeSeries Calculate()
        {
            return inputTimeSeries.Bucket(start, end, intervalBucketSizeInSeconds);
        }

        public override object Render(TimeSeries resultObject)
        {
            return ObjectStore.Add(name, resultObject);
        }
    }
}
