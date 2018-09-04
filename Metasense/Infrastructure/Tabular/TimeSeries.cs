using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Metasense.Tabular;

namespace Metasense.Infrastructure.Tabular
{
    public class TimeSeries
    {
        public int Count => _ts.Count;

        //private SortedList<TSPoint> _ts;

        private SortedList<DateTime, double> _ts;

        private TimeSeries()
        {
            //_ts = new SortedList<TSPoint>();
            _ts = new SortedList<DateTime, double>();
        }

        /// <summary>
        /// Gets the <see cref="TSPoint"/> at the given index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TSPoint this[int index]
        {
            get
            {
                var kvp = _ts.ElementAt(index);
                return new TSPoint {Time = kvp.Key, Value = kvp.Value};
            }
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

        public void AddOrReplace(DateTime dateTime, double value)
        {
            if (_ts.ContainsKey(dateTime))
            {
                if (value > _ts[dateTime])
                {
                    _ts[dateTime] = value;
                }
            }
            else
            {
                _ts[dateTime] = value;
            }
        }

        public List<DateTime> GetDates()
        {
            //return _ts.Select(point => point.Time).ToList();

            return _ts.Keys.ToList();
        }

        public List<double> GetValues()
        {
            return _ts.Select(point => point.Value).ToList();
        }

        public TimeSeries Crop(DateTime start, DateTime end)
        {
            //if (end <= start)
            //{
            //    throw new ArgumentException($"End ({end}) needs to come after Start ({start}) ");
            //}
            //var retVal = new TimeSeries();
            //var croppedSeries = _ts.Where(point => point.Time >= start && point.Time <= end);
            //foreach (var point in croppedSeries)
            //{
            //    retVal.Add(point.Time, point.Value);
            //}
            //return retVal;

            if (end <= start)
            {
                throw new ArgumentException($"End ({end}) needs to come after Start ({start}) ");
            }
            var retVal = new TimeSeries();
            var croppedTS = _ts.Where(kvp => kvp.Key >= start && kvp.Key <= end);
            foreach (var kvp in croppedTS)
            {
                retVal.Add(kvp.Key, kvp.Value);
            }
            return retVal;
        }

        public TimeSeries BucketExperimental(DateTime start, DateTime end, long intervalInSeconds, bool isCumulative)
        {
            if (end <= start)
            {
                throw new ArgumentException($"End ({end}) needs to come after Start ({start}) ");
            }

            var retVal = new TimeSeries();
            if (isCumulative)
            {
                var currentStart = start;
                var currentEnd = start.AddSeconds(intervalInSeconds);
                double currentCumulative = 0;

                while (currentEnd <= end)
                {
                    var tsCroppedToCurrentBucked = _ts.Where(kvp => kvp.Key >= currentStart && kvp.Key <= currentEnd);
                    var maxValue = tsCroppedToCurrentBucked.Any() ? tsCroppedToCurrentBucked.Max(kvp => kvp.Value) : currentCumulative;
                    currentCumulative = maxValue;

                    retVal.Add(currentStart, maxValue);

                    currentStart = currentEnd;
                    currentEnd = currentStart.AddSeconds(intervalInSeconds);
                }
            }

            return retVal;
        }

        public TimeSeries Bucket(DateTime start, DateTime end, long intervalInSeconds, bool isCumulativeSeries)
        {
            //if (end <= start)
            //{
            //    throw new ArgumentException($"End ({end}) needs to come after Start ({start}) ");
            //}

            //var retVal = new TimeSeries();
            //var croppedSeries = _ts.Where(point => point.Time >= start && point.Time <= end);
            //foreach (var point in croppedSeries)
            //{
            //    var rem = (int)(point.Time - start).TotalSeconds % intervalInSeconds;
            //    var tp = point.Time.AddSeconds(-rem);
            //    retVal.Add(tp, point.Value);
            //}

            //return retVal;

            if (end <= start)
            {
                throw new ArgumentException($"End ({end}) needs to come after Start ({start}) ");
            }

            var retVal = new TimeSeries();
            var croppedTS = _ts.Where(kvp => kvp.Key >= start && kvp.Key <= end);
            if (isCumulativeSeries)
            {
                foreach (var kvp in croppedTS)
                {
                    var rem = (int)(kvp.Key - start).TotalSeconds % intervalInSeconds;
                    var tp = kvp.Key.AddSeconds(-rem);
                    retVal.AddOrReplace(tp, kvp.Value);
                }
            }
            else
            {
                foreach (var kvp in croppedTS)
                {
                    var rem = (int)(kvp.Key - start).TotalSeconds % intervalInSeconds;
                    var tp = kvp.Key.AddSeconds(-rem);
                    retVal.Add(tp, kvp.Value);
                }
            }

            return retVal;
        }

        /// <summary>
        /// Merge constituent time series into 1 object
        /// </summary>
        /// <param name="constituents"></param>
        /// <param name="fullMerge"></param>
        /// <returns></returns>
        public static TimeSeries CreateMerged(IEnumerable<TimeSeries> constituents)
        {
            var retVal = new TimeSeries();
            foreach (var constituent in constituents)
            {
                foreach (var kvp in constituent._ts)
                {
                    retVal.Add(kvp.Key, kvp.Value);
                }
            }
            return retVal;
        }

        /// <summary>
        /// Merge the dates of the constituent time series and return a mapping of DateTime to a row of values corresponding to the constituent time series
        /// </summary>
        /// <param name="constituents"></param>
        /// <returns></returns>
        public static Dictionary<DateTime, double[]> MergeDates(TimeSeries[] constituents)
        {
            var retVal = new Dictionary<DateTime, double[]>();

            for (var constituentIndex = 0; constituentIndex < constituents.Length; constituentIndex++)
            {
                foreach (var point in constituents[constituentIndex]._ts)
                {
                    if (!retVal.ContainsKey(point.Key))
                    {
                        retVal.Add(point.Key, new double[constituents.Length]);
                    }
                    retVal[point.Key][constituentIndex] += point.Value;
                }
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

        public static TimeSeries LoadFromTable(Table table, int dateColumnIndex, int valueColumnIndex, bool isCumulative)
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

                if (isCumulative)
                {
                    retVal.AddOrReplace(dateTime, value);
                }
                else
                {
                    retVal.Add(dateTime, value);
                }
            }

            return retVal;
        }
    }
}
