using System;
using ExcelDna.Integration;
using Metasense.Infrastructure;
using Metasense.Infrastructure.Functions;
using Metasense.Infrastructure.Tabular;

namespace Metasense.MetasenseFunctions.Tabular
{
    public class ShowTimeSeries : BaseFunction<object[,]>
    {
        #region Parameters

        public ExcelArg TimeSeries { get; set; }

        public ExcelArg ShowHeaders { get; set; }

        private TimeSeries timeSeries;

        private bool showHeaders;

        #endregion

        public ShowTimeSeries(Enums.FunctionType functionType) : base(functionType)
        {
        }

        public override void ResolveInputs()
        {
            timeSeries = TimeSeries.GetFromStoreAs<TimeSeries>();

            showHeaders = ShowHeaders.AsBoolean(true);
        }

        public override object[,] Calculate()
        {
            var callingRange = Util.GetCallingRange();
            var numRows = callingRange.GetNumRows();
            var numCols = callingRange.GetNumCols();
            var retVal = new object[showHeaders ? numRows + 1 : numRows, numCols];

            if (numRows == 1 && numCols == 1)
            {
                retVal = new object[,] {{TimeSeries.AsString()}};
            }
            else
            {
                if (numCols < 2)
                {
                    throw new ArgumentException("This function needs to be called from a range with at-least 2 columsn");
                }

                var startIndex = 0;
                if (showHeaders)
                {
                    startIndex++;
                    retVal[0, 0] = "Time";
                    retVal[0, 1] = "Value";
                }
                
                for (var i = startIndex; i < retVal.GetLength(0); i++)
                {
                    if (i < timeSeries.Count)
                    {
                        var point = timeSeries[i];
                        retVal[i, 0] = point.Time;
                        retVal[i, 1] = point.Value;
                    }
                }
            }

            return retVal;
        }
    }
}
