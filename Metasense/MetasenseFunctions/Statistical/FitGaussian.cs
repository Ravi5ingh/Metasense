using System;
using Metasense.Infrastructure;
using Metasense.Infrastructure.Functions;
using Metasense.Math;

namespace Metasense.MetasenseFunctions.Statistical
{
    public class FitGaussian : BaseFunction<object[,]>
    {
        #region Parameters

        public ExcelArg XValues { get; set; }

        public ExcelArg YValues { get; set; }

        #endregion

        #region Resolved Parameters

        private double[] xValues;

        private double[] yValues;

        #endregion

        public FitGaussian(Enums.FunctionType functionType) : base(functionType)
        {
        }

        public override void ResolveInputs()
        {
            xValues = XValues.As1DArray<double>();

            yValues = YValues.As1DArray<double>();

            if (xValues.Length != yValues.Length)
            {
                throw new ArgumentException(
                    $"The number of x values ({xValues.Length}) needs to be equal to the number of y values ({yValues.Length})");
            }
        }

        public override object[,] Calculate()
        {
            var fittedGaussian = MathFunctions.FitGaussianToPlot(xValues, yValues);

            var retVal = new object[2, 3];
            // mean
            retVal[0, 0] = "σ";
            retVal[1, 0] = fittedGaussian.Item3;
            // std dev
            retVal[0, 1] = "µ";
            retVal[1, 1] = fittedGaussian.Item2;
            // a
            retVal[0, 2] = "a";
            retVal[1, 2] = fittedGaussian.Item1;

            return retVal;
        }
    }
}
