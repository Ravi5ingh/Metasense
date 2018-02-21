using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using ExcelDna.Integration;
using Metasense.Infrastructure;

namespace Metasense.MetasenseFunctions
{
    /// <summary>
    /// Class to execute different function types safely
    /// </summary>
    public static class FunctionRunner
    {
        /// <summary>
        /// A register of flags to store which individual function calls have had their function wizard run
        /// </summary>
        private static IDictionary<ExcelReference, bool> functionWizardRunRegister = new Dictionary<ExcelReference, bool>();
        
        /// <summary>
        /// This construct caches the results of individual function calls in the sheet
        /// </summary>
        private static IDictionary<ExcelReference, string> functionCache = new Dictionary<ExcelReference, string>();
        
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
                //Get the function type
                var functionTypeId = (int) function.FunctionType;

                //Register the function in the FW register if necessary
                var callingRange = Util.GetCallingRange();
                if (!functionWizardRunRegister.ContainsKey(callingRange))
                {
                    functionWizardRunRegister[callingRange] = false;
                }

                //Reset the FW flag if in function wizard
                var isInFunctionWizard = ExcelDnaUtil.IsInFunctionWizard();
                functionWizardRunRegister[callingRange] = isInFunctionWizard;

                switch (functionTypeId)
                {
                    //Light
                    case 1:
                        return Execute(function);
                    
                    //Heavy
                    case 2:
                        return isInFunctionWizard ? "..." : Execute(function);

                    //Sticky
                    case 4:
                        return functionWizardRunRegister[callingRange] ? Execute(function) : functionCache[callingRange];
                    
                    //Sticky and Heavy
                    case 6:
                        return isInFunctionWizard ? "..." : functionWizardRunRegister[callingRange] ? functionCache[callingRange] : Execute(function);

                    default:
                        throw new Exception($"INTERNAL ERROR : {function.FunctionType} is not a function type that can be processed");
                }

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
