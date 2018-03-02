using System;
using System.Data;
using System.Linq;
using Metasense.Infrastructure;
using Metasense.Infrastructure.Functions;
using Metasense.Math;
using Metasense.Tabular;

namespace Metasense.MetasenseFunctions.Statistical
{
    public class FitGaussian : BaseFunction<object[,]>
    {
        #region Parameters

        public ExcelArg XValues { get; set; }

        public ExcelArg YValues { get; set; }

        public ExcelArg ShowLabels { get; set; }

        #endregion

        #region Resolved Parameters

        private double[] xValues;

        private double[] yValues;

        private bool showLabels;

        #endregion

        public FitGaussian(Enums.FunctionType functionType) : base(functionType)
        {
        }

        public override void ResolveInputs()
        {
            xValues = XValues.IsInObjectStoreAs<Table>()
                ? XValues.GetFromStoreAs<Table>().Data.Cast<double>().ToArray()
                : XValues.As1DArray<double>();

            yValues = YValues.IsInObjectStoreAs<Table>()
                ? YValues.GetFromStoreAs<Table>().Data.Cast<double>().ToArray()
                : YValues.As1DArray<double>();

            if (xValues.Length != yValues.Length)
            {
                throw new ArgumentException(
                    $"The number of x values ({xValues.Length}) needs to be equal to the number of y values ({yValues.Length})");
            }

            showLabels = ShowLabels.AsBoolean(false);
        }

        public override object[,] Calculate()
        {
            var fittedGaussian = MathFunctions.FitGaussianToPlot(xValues, yValues);

            var retVal = new object[showLabels ? 2 : 1, 3];
            var numberRow = 0;
            if (showLabels)
            {
                retVal[0, 0] = "σ";
                retVal[0, 1] = "µ";
                retVal[0, 2] = "a";
                numberRow++;
            }

            retVal[numberRow, 0] = fittedGaussian.Item3;
            retVal[numberRow, 1] = fittedGaussian.Item2;
            retVal[numberRow, 2] = fittedGaussian.Item1;

            return retVal;
        }
    }
}
