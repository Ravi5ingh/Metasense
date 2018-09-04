using Metasense.Infrastructure;
using Metasense.Infrastructure.Functions;
using Metasense.Infrastructure.Tabular;
using Metasense.Tabular;

namespace Metasense.MetasenseFunctions.Tabular
{
    public class ExecuteSQL : BaseFunction<Table>
    {
        public ExcelArg Name { get; set; }

        public ExcelArg SQLConnection { get; set; }

        public ExcelArg SQLQuery { get; set; }

        private string name;

        private SQLConnection sqlConnection;

        private string query;

        public ExecuteSQL(Enums.FunctionType functionType) : base(functionType)
        {
        }

        public override void ResolveInputs()
        {
            name = Name.AsString();

            sqlConnection = SQLConnection.GetFromStoreAs<SQLConnection>();

            query = ResolveSQLQueryString(SQLQuery);
        }

        public override Table Calculate()
        {
            return Table.LoadFromSQL(query, sqlConnection);
        }

        public override object Render(Table resultObject)
        {
            return ObjectStore.Add(name, resultObject);
        }

        private static string ResolveSQLQueryString(ExcelArg QueryArg)
        {
            var sqlQueryGrid = QueryArg.As2DArrayOf<object>();

            var retVal = string.Empty;
            foreach (var element in sqlQueryGrid)
            {
                if (element is string || element is int || element is double)
                {
                    retVal += $" {element}";
                }
            }

            return retVal;
        }
    }
}
