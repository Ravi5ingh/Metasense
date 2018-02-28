using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            query = SQLQuery.AsString();
        }

        public override Table Calculate()
        {
            return Table.LoadFromSQL(query, sqlConnection);
        }

        public override object Render(Table resultObject)
        {
            return ObjectStore.Add(name, resultObject);
        }
    }
}
