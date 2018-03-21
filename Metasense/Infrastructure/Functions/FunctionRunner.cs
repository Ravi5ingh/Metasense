using System;
using ExcelDna.Integration;

namespace Metasense.Infrastructure.Functions
{
    /// <summary>
    /// Class to execute different function types safely
    /// </summary>
    public static class FunctionRunner
    {   
        /// <summary>
        /// Run the function and return the output or the message of the error
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="function"></param>
        /// <returns></returns>
        public static object Run<T>(IFunction<T> function)
        {
            try
            {
                // get the function type
                var functionTypeId = (int) function.FunctionType;

                // get fields
                var isInFunctionWizard = ExcelDnaUtil.IsInFunctionWizard();
                var callingRange = Util.GetCallingRange();

                if (isInFunctionWizard)
                {
                    FunctionWizardRegister.Register(callingRange);
                }

                object retVal;
                switch (functionTypeId)
                {
                    //Light
                    case 1:
                        retVal = ForceExecute(callingRange, function);
                        break;
                    //Heavy
                    case 2:
                        retVal = isInFunctionWizard ? "..." : ForceExecute(callingRange, function);
                        break;
                    //Sticky
                    case 4:
                        retVal = ExecuteSticky(callingRange, function);
                        break;
                    //Sticky and Heavy
                    case 6:
                        retVal = isInFunctionWizard ? "..." : ExecuteSticky(callingRange, function);
                        break;

                    default:
                        throw new Exception($"INTERNAL ERROR : {function.FunctionType} is not a function type that can be processed");
                }

                return retVal;

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private static object ExecuteSticky<T>(ExcelReference callingRange, IFunction<T> function)
        {
            var retVal = CalculationCache.GetValue(callingRange, function, FunctionWizardRegister.GetValue(callingRange));
            FunctionWizardRegister.DeRegister(callingRange);
            return retVal;
        }

        private static object ForceExecute<T>(ExcelReference callingRange, IFunction<T> function)
        {
            return CalculationCache.GetValue(callingRange, function, true);
        }

        ///// <summary>
        ///// Peform the function execution
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="function"></param>
        ///// <returns></returns>
        //private static object Execute<T>(IFunction<T> function)
        //{
        //    function.ResolveInputs();

        //    var rawOutput = function.Calculate();

        //    return function.Render(rawOutput);
        //}
    }
}
