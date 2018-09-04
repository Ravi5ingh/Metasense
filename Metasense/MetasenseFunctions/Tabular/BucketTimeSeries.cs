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

        public ExcelArg IntervalBucketSize { get; set; }

        public ExcelArg IsCumulativeSeries { get; set; }

        #endregion

        #region Resolved Parameters

        private string name;

        private TimeSeries inputTimeSeries;

        private System.DateTime start;

        private System.DateTime end;

        private long intervalBucketSizeInSeconds;

        private bool isCumulativeSeries;

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

            isCumulativeSeries = IsCumulativeSeries.AsBoolean(false);
        }

        public override TimeSeries Calculate()
        {
            return inputTimeSeries.BucketExperimental(start, end, intervalBucketSizeInSeconds, isCumulativeSeries);
        }

        public override object Render(TimeSeries resultObject)
        {
            return ObjectStore.Add(name, resultObject);
        }
    }
}
