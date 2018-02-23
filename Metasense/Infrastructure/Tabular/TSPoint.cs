using System;

namespace Metasense.Infrastructure.Tabular
{
    public class TSPoint : IComparable<TSPoint>
    {
        public DateTime Time { get; set; }

        public double Value { get; set; }
        public int CompareTo(TSPoint other)
        {
            return Time < other.Time ? -1 :
                Time > other.Time ? 1 : 0;
        }
    }
}
