using System;
using Metasense.Infrastructure;
using Metasense.Infrastructure.Functions;
using Metasense.Tabular;

namespace Metasense.MetasenseFunctions.Tabular
{
    /// <summary>
    /// TODO : Need to think about equilvalence of different 2d data structures (eg. 2d array, table, time series, etc.)
    /// </summary>
    public class GetSubGrid : BaseFunction<Table>
    {
        public ExcelArg Grid { get; set; }

        public ExcelArg StartRow { get; set; }

        public ExcelArg EndRow { get; set; }

        public ExcelArg StartColumn { get; set; }

        public ExcelArg EndColumn { get; set; }

        private Table grid;

        private int startRow;

        private int endRow;

        private int startCol;

        private int endCol;

        public GetSubGrid(Enums.FunctionType functionType) : base(functionType)
        {
        }

        public override void ResolveInputs()
        {

        }

        public override Table Calculate()
        {
            throw new NotImplementedException();
        }
    }
}
