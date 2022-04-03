using Common;

namespace DataProcessor
{
    /// <summary>
    /// Dictionary for mapping raw value in data to another (i.e "true/false" to boolean values)
    /// </summary>
    internal interface IValueDictionary
    {
        public Dictionary<string, string> Dict { get; init; }
        public ValueDictionaryType DictType { get; init; }
        public Type ValueType { get; init; }
        public IEnumerable<DataPoint> MapDataPoints(IEnumerable<DataPoint> points);
        public DataPoint MapDataPoint(DataPoint point);
    }
}
