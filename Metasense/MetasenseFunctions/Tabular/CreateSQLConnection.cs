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
    public class CreateSQLConnection : BaseFunction<SQLConnection>
    {
        public ExcelArg Name { get; set; }

        public ExcelArg ServerName { get; set; }

        public ExcelArg DatabaseName { get; set; }

        private string name;

        private string serverName;

        private string databaseName;

        private SQLConnection sqlConnection;

        public CreateSQLConnection(Enums.FunctionType functionType) : base(functionType)
        {
        }

        public override void ResolveInputs()
        {
            name = Name.AsString();

            serverName = ServerName.AsString();

            databaseName = DatabaseName.AsString();
        }

        public override SQLConnection Calculate()
        {
            return new SQLConnection(serverName, databaseName);
        }

        public override object Render(SQLConnection resultObject)
        {
            return ObjectStore.Add(name, resultObject);
        }
    }
}
