using System;
using System.Collections.Generic;
using System.IO;

namespace Metasense.Infrastructure.Tabular
{
    public class TimeSeries
    {
        private SortedList<DateTime, double> _ts;

        private TimeSeries()
        {
            _ts = new SortedList<DateTime, double>();
        }

        public void Add(DateTime dateTime, double value)
        {
            if (_ts.ContainsKey(dateTime))
            {
                _ts[dateTime] += value;
            }
            else
            {
                _ts.Add(dateTime, value);
            }
        }

        public static TimeSeries LoadFromFile(
            FileInfo fileInfo, 
            char delimiter, 
            int dateColumnIndex,
            int valueColumnIndex)
        {
            var retVal = new TimeSeries();
            var lines = File.ReadAllLines(fileInfo.FullName);

            for (var i = 1; i < lines.Length; i++)
            {
                var cells = lines[i].Split(delimiter);
                if (!DateTime.TryParse(cells[dateColumnIndex], out DateTime dateTime))
                {
                    throw new ArgumentException(
                        $"The value '{cells[dateColumnIndex]}' at row : {i} col : {dateColumnIndex} cannot be parsed as a {typeof(DateTime)}");
                }
                if (!double.TryParse(cells[valueColumnIndex], out double value))
                {
                    throw new ArgumentException(
                        $"The value '{cells[valueColumnIndex]}' at row : {i} col : {valueColumnIndex} cannot be parsed as a {typeof(double)}");
                }
                retVal.Add(dateTime, value);
            }

            return retVal;
        }
    }
}
