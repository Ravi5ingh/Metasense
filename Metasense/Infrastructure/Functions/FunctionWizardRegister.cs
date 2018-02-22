using System.Collections.Generic;
using System.Windows.Forms;
using ExcelDna.Integration;

namespace Metasense.Infrastructure.Functions
{
    public static class FunctionWizardRegister
    {
        private static IDictionary<ExcelReference, bool> functionWizardRunRegister = new Dictionary<ExcelReference, bool>();

        public static bool GetValue(ExcelReference callingRange)
        {
            if (!functionWizardRunRegister.ContainsKey(callingRange))
            {
                functionWizardRunRegister[callingRange] = false;
            }

            return functionWizardRunRegister[callingRange];
        }

        public static void Register(ExcelReference callingRange)
        {
            functionWizardRunRegister[callingRange] = true;
        }

        public static void DeRegister(ExcelReference callingRange)
        {
            functionWizardRunRegister[callingRange] = false;
        }
    }
}
