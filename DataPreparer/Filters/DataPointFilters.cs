using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessor
{
    internal static class DataPointFilters
    {
        public static IEnumerable<DataPoint> FilterByName(this IEnumerable<DataPoint> data, string name) => data.Where(d => d.Name == name);

        public static IEnumerable<DataPoint> FilterByDate(this IEnumerable<DataPoint> data, DateOnly start = default, DateOnly end = default) 
            => data.Where(d => d.Date >= start && d.Date <= end);
    }
}
