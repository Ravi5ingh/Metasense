using System.Collections.Generic;
using System.Linq;
using Metasense.Infrastructure;
using Metasense.Infrastructure.Functions;
using Metasense.Infrastructure.Tabular;

namespace Metasense.MetasenseFunctions.Tabular
{
    public class MergeTimeSeries : BaseFunction<object[,]>
    {
        public ExcelArg InputTimeSeries { get; set; }

        public ExcelArg FullMerge { get; set; }

        private List<TimeSeries> inputTimeSeries;

        private bool performFullMerge;

        public MergeTimeSeries(Enums.FunctionType functionType) : base(functionType)
        {
        }

        public override void ResolveInputs()
        {
            inputTimeSeries = new List<TimeSeries>();
            var handles = InputTimeSeries.As2DArray<string>();
            foreach (var handle in handles)
            {
                if (ObjectStore.Contains(handle))
                {
                    inputTimeSeries.Add(ObjectStore.Get(handle) as TimeSeries);
                }
            }
        }

        public override object[,] Calculate()
        {
            object[,] retVal;
            if (performFullMerge)
            {
                var mergedTimeSeries = TimeSeries.CreateMerged(inputTimeSeries);
                retVal = new object[mergedTimeSeries.Count,2];
                for (var i = 0; i < mergedTimeSeries.Count; i++)
                {
                    var point = mergedTimeSeries[i];
                    retVal[i, 0] = point.Time;
                    retVal[i, 1] = point.Value;
                }
            }
            else
            {
                var mergedColumns = TimeSeries.MergeDates(inputTimeSeries.ToArray());
                var rows = mergedColumns.Count;
                var columns = mergedColumns.First().Value.Length;
                retVal = new object[rows,columns];
                for (var i = 0; i < rows; i++)
                {
                    var row = mergedColumns.ElementAt(i).Value;
                    for (var j = 0; j < columns; j++)
                    {
                        retVal[i, j] = row[j];
                    }
                }
            }

            return retVal;
        }
    }
}
