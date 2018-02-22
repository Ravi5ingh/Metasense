using System.IO;
using Metasense.Infrastructure;
using Metasense.Infrastructure.Functions;
using Metasense.Tabular;

namespace Metasense.MetasenseFunctions.Tabular
{
    public class LoadTable : BaseFunction<Table>
    {
        public ExcelArg Name { get; set; }

        public ExcelArg Location { get; set; }

        public ExcelArg Delimiter { get; set; }

        private string name;

        private FileInfo location;

        private char delimiter;

        public LoadTable(Enums.FunctionType functionType = Enums.FunctionType.Light) : base(functionType)
        {
        }

        public override void ResolveInputs()
        {
            name = Name.AsString();
            location = new FileInfo(Location.AsString());
            delimiter = Delimiter.AsChar(',');
        }

        public override Table Calculate()
        {
            Table retVal;
            switch (location.Extension.ToUpper())
            {
                case "CSV":
                    retVal = Table.LoadFromCSV(location, delimiter);
                    break;
                default:
                    retVal = Table.LoadFromCSV(location, delimiter);
                    break;

            }

            return retVal;
        }

        public override object Render(Table resultObject)
        {
            return ObjectStore.Add(name, resultObject);
        }
    }
}
