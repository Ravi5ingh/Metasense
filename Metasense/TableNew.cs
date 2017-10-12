using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Metasense
{
    public class TableNew
    {
        #region Interface

        public static TableNew LoadFromCSV(FileInfo fileInfo)
        {
            return new TableNew(fileInfo);
        }

        public Object[,] Peek()
        {
            var data = new List<Object[]>();

            var lines = File.ReadAllLines(fileInfo.FullName);
            var dataRows = Math.Min(peekRows, lines.Length);

            for(var i = 0; i < dataRows; i++)
            {
                var elements = lines[i].Split(',').Cast<Object>().ToArray();
                data.Add(elements);
            }

            return data.ToArray().As2DArray();
        }
    
        #endregion

        #region Private

        private FileInfo fileInfo;

        private const int peekRows = 10;

        /// <summary>
        /// temporary crap. get rid of this shit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <returns></returns>
        private T[,] To2D<T>(T[] arr)
        {
            var numRows = arr.Length;
            arr.Max()
        }

        private TableNew(FileInfo fileInfo)
        {
            this.fileInfo = fileInfo;
        }

        #endregion
    }
}
