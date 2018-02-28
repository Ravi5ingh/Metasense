﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
