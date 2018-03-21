using Metasense.Infrastructure;
using Metasense.Infrastructure.Functions;
using Metasense.Tabular;

namespace Metasense.MetasenseFunctions.Tabular
{
    public class CreateTable : BaseFunction<Table>
    {
        public ExcelArg TableName { get; set; }

        public ExcelArg Data { get; set; }

        private string tableName;

        private object[,] rawData;

        public CreateTable(Enums.FunctionType functionType) : base(functionType)
        {
        }

        public override void ResolveInputs()
        {
            tableName = TableName.AsString();

            rawData = Data.As2DArray<object>();
        }

        public override Table Calculate()
        {
            return Table.CreateFromRawData(rawData);
        }

        public override object Render(Table resultObject)
        {
            return ObjectStore.Add(tableName, resultObject);
        }
    }
}
