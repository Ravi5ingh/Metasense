﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Metasense.Tabular;

namespace Metasense.Infrastructure.Tabular
{
    public class TimeSeries
    {
        public int Count => _ts.Count;

        private SortedList<TSPoint> _ts;

        private TimeSeries()
        {
            _ts = new SortedList<TSPoint>();
        }

        /// <summary>
        /// Gets the <see cref="TSPoint"/> at the given index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TSPoint this[int index]
        {
            get => _ts[index];
            set => _ts[index] = value;
        }

        public void Add(DateTime dateTime, double value)
        {
            var fakePoint = new TSPoint {Time = dateTime};
            if (_ts.ContainsComparableValue(fakePoint, out var index))
            {
                var oldPoint = _ts[index];
                _ts[index]  = new TSPoint{Time = dateTime, Value = oldPoint.Value + value};
            }
            else
            {
                _ts.Add(new TSPoint {Time = dateTime, Value = value});
            }
        }

        public List<DateTime> GetDates()
        {
            return _ts.Select(point => point.Time).ToList();
        }

        public List<double> GetValues()
        {
            return _ts.Select(point => point.Value).ToList();
        }

        public TimeSeries Crop(DateTime start, DateTime end)
        {
            if (end <= start)
            {
                throw new ArgumentException($"End ({end}) needs to come after Start ({start}) ");
            }
            var retVal = new TimeSeries();
            var croppedSeries = _ts.Where(point => point.Time >= start && point.Time <= end);
            foreach (var point in croppedSeries)
            {
                retVal.Add(point.Time, point.Value);
            }
            return retVal;
        }

        public TimeSeries Bucket(DateTime start, DateTime end, long intervalInSeconds)
        {
            if (end <= start)
            {
                throw new ArgumentException($"End ({end}) needs to come after Start ({start}) ");
            }

            var retVal = new TimeSeries();
            var croppedSeries = _ts.Where(point => point.Time >= start && point.Time <= end);
            foreach (var point in croppedSeries)
            {
                var rem = (int)(point.Time - start).TotalSeconds % intervalInSeconds;
                var tp = point.Time.AddSeconds(-rem);
                retVal.Add(tp, point.Value);
            }

            return retVal;
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

        public static TimeSeries LoadFromTable(Table table, int dateColumnIndex, int valueColumnIndex)
        {
            var retVal = new TimeSeries();

            for (var i = 0; i < table.Data.GetLength(0); i++)
            {
                var dateCell = table.Data[i, dateColumnIndex] as string;
                var valueCell = table.Data[i, valueColumnIndex] as string;
                if (!DateTime.TryParse(dateCell, out DateTime dateTime))
                {
                    throw new ArgumentException(
                        $"The value '{dateCell}' at row : {i} col : {dateColumnIndex} cannot be parsed as a {typeof(DateTime)}");
                }
                if (!double.TryParse(valueCell, out double value))
                {
                    throw new ArgumentException(
                        $"The value '{valueCell}' at row : {i} col : {valueColumnIndex} cannot be parsed as a {typeof(double)}");
                }
                retVal.Add(dateTime, value);
            }

            return retVal;
        }
    }
}
