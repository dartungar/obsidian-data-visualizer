using Common;

namespace DataProcessor
{
    internal static class DataPointFilters
    {
        public static IEnumerable<DataPoint> FilterByName(this IEnumerable<DataPoint> data, string name) => data?.Where(d => d.Name == name);

        public static IEnumerable<DataPoint> FilterByDate(this IEnumerable<DataPoint> data, DateTime start = default, DateTime end = default)
            => data.Where(d => d.Date >= start && d.Date <= end);
    }
}
