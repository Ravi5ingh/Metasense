using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Encog.ML.EA.Train;
using Metasense.Infrastructure.Tabular;

namespace Metasense.Tabular
{
    /// <summary>
    /// Represents a table in Excel
    /// </summary>
    public class Table
    {
        /// <summary>
        /// The data
        /// </summary>]
        public Object[,] Data { get; }

        public static Table LoadFromCSV(FileInfo fileInfo, char delimiter = ',')
        {
            var data = new List<Object[]>();

            var lines = File.ReadAllLines(fileInfo.FullName);
            foreach (var line in lines)
            {
                var elements = line.Split(delimiter).Cast<Object>().ToArray();
                data.Add(elements);
            }

            return new Table(data.ToArray().As2DArray());
        }

        public static Table LoadFromSQL(string sqlQuery, SQLConnection sqlConnection)
        {
            var data = sqlConnection.ExecuteQuery(sqlQuery);
            
            return new Table(data.As2DArray());
        }

        public static Table CreateFromRawData(object[,] rawData)
        {
            return new Table(rawData);
        }

        public static Table CreateRowFrom<T>(IEnumerable<T> rawData)
        {
            var rawArr = rawData.ToArray();
            var row = new object[1, rawArr.Length];
            for (var i = 0; i < rawArr.Length; i++)
            {
                row[0, i] = rawArr[i];
            }
            return new Table(row);
        }

        public static Table CreateColumnFrom<T>(IEnumerable<T> rawData)
        {
            var rawArr = rawData.ToArray();
            var column = new object[rawArr.Length, 1];
            for (var i = 0; i < rawArr.Length; i++)
            {
                column[i, 0] = rawArr[i];
            }
            return new Table(column);
        }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="data"></param>
        private Table(Object[,] data)
        {
            this.Data = data;
        }
    }
}
