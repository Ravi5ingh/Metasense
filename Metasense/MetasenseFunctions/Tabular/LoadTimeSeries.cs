using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Metasense.Infrastructure;
using Metasense.Infrastructure.Functions;
using Metasense.Infrastructure.Tabular;

namespace Metasense.MetasenseFunctions.Tabular
{
    /// <summary>
    /// Load a time series object from a file into memory
    /// </summary>
    public class LoadTimeSeries : BaseFunction<TimeSeries>
    {
        #region Parameters

        public ExcelArg Name { get; set; }

        public ExcelArg FileName { get; set; }

        public ExcelArg Delimiter { get; set; }

        public ExcelArg DateColumnIndex { get; set; }

        public ExcelArg ValueColumnIndex { get; set; }

        #endregion

        #region Resolved Parameters

        private string name;

        private FileInfo fileInfo;

        private char delimiter;

        private int dateColumnIndex;

        private int valueColumnIndex;

        #endregion

        public LoadTimeSeries(Enums.FunctionType functionType) : base(functionType)
        {
        }

        public override void ResolveInputs()
        {
            name = Name.AsString();

            fileInfo = new FileInfo(FileName.AsString());

            delimiter = Delimiter.AsChar(',');

            dateColumnIndex = DateColumnIndex.AsInt();

            valueColumnIndex = ValueColumnIndex.AsInt();
        }

        public override TimeSeries Calculate()
        {
            return TimeSeries.LoadFromFile(fileInfo, delimiter, dateColumnIndex, valueColumnIndex);
        }

        public override object Render(TimeSeries resultObject)
        {
            return ObjectStore.Add(name, resultObject);
        }
    }
}
