using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metasense.Infrastructure;
using Metasense.Infrastructure.Functions;
using Metasense.Infrastructure.Tabular;

namespace Metasense.MetasenseFunctions.Tabular
{
    public class ExecuteSQL : BaseFunction<string[,]>
    {
        public ExcelArg SQLConnection { get; set; }

        public ExcelArg SQLQuery { get; set; }

        private SQLConnection sqlConnection;

        private string query;

        public ExecuteSQL(Enums.FunctionType functionType) : base(functionType)
        {
        }

        public override void ResolveInputs()
        {
            sqlConnection = SQLConnection.GetFromStoreAs<SQLConnection>();

            query = SQLQuery.AsString();
        }

        public override string[,] Calculate()
        {
            return sqlConnection.ExecuteQuery(query).As2DArray();
        }
    }
}
