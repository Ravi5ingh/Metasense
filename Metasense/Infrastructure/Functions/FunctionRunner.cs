using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using ExcelDna.Integration;
using Metasense.Infrastructure;

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

                object retVal;
                switch (functionTypeId)
                {
                    //Light
                    case 1:
                        retVal = Execute(function);
                        break;
                    //Heavy
                    case 2:
                        retVal = isInFunctionWizard ? "..." : Execute(function);
                        break;
                    //Sticky
                    case 4:
                        retVal = FunctionWizardRegister.GetValue(callingRange)
                            ? Execute(function)
                            : CalculationCache.GetValue(callingRange, () => Execute(function));
                        break;
                    //Sticky and Heavy
                    case 6:
                        retVal = isInFunctionWizard ? "..." :
                            FunctionWizardRegister.GetValue(callingRange) ? 
                                Execute(function) :
                                CalculationCache.GetValue(callingRange, () => Execute(function));
                        break;

                    default:
                        throw new Exception($"INTERNAL ERROR : {function.FunctionType} is not a function type that can be processed");
                }

                // update the FW regsiter
                FunctionWizardRegister.Update(callingRange, isInFunctionWizard);

                return retVal;

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// Peform the function execution
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="function"></param>
        /// <returns></returns>
        private static object Execute<T>(IFunction<T> function)
        {
            function.ResolveInputs();

            var rawOutput = function.Calculate();

            return function.Render(rawOutput);
        }
    }
}
