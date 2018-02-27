using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Metasense.Infrastructure.Tabular
{
    public class SQLConnection
    {
        public string Server { get; }

        public string DatabaseName { get; }

        public SQLConnection(string serverName, string databaseName)
        {
            Server = serverName;
            DatabaseName = databaseName;
        }

        public List<string[]> ExecuteQuery(string query)
        {
            List<string[]> retVal;
            using (var sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = GetConnectionString();

                sqlConnection.Open();

                var sqlCommand = new SqlCommand(query, sqlConnection);

                using (var reader = sqlCommand.ExecuteReader())
                {
                    retVal = ReadData(reader);
                }
            }

            return retVal;
        }

        private string GetConnectionString()
        {
            if (Server.IsNullOrEmpty() || DatabaseName.IsNullOrEmpty())
            {
                throw new ArgumentException(string.Format("Cannot generate connection string, {0} {1}",
                    Server.IsNullOrEmpty() ? "Server name is not set" : string.Empty,
                    DatabaseName.IsNullOrEmpty() ? "Database name is not set" : string.Empty));
            }

            return $"Data Source={Server};Initial Catalog = {DatabaseName}; Integrated Security = True";
        }

        private List<string[]> ReadData(SqlDataReader reader)
        {
            var retVal = new List<string[]>();
            var numCols = reader.FieldCount;
            while (reader.Read())
            {
                var row = new string[numCols];
                for (var i = 0; i < numCols; i++)
                {
                    row[i] = reader[i].ToString();
                }
                retVal.Add(row);
            }

            return retVal;
        }
    }
}
