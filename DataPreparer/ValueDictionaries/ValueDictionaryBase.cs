using Common;

namespace DataProcessor
{
    internal class ValueDictionaryBase : IValueDictionary
    {
        public ValueDictionaryType DictType { get; init; }
        public Dictionary<string, string> Dict { get; init; }

        public Type ValueType { get; init; }

        public ValueDictionaryBase(Type valueType, Dictionary<string, string> valueDict, ValueDictionaryType type = ValueDictionaryType.Custom)
        {
            ValueType = valueType;
            Dict = valueDict;
            DictType = type;
        }

        public IEnumerable<DataPoint> MapDataPoints(IEnumerable<DataPoint> points)
            => points.Select(MapDataPoint);

        public virtual DataPoint MapDataPoint(DataPoint point)
        {
            if (!Dict.ContainsKey(point.Value))
                return point;
            return new DataPoint { Date = point.Date, Name = point.Name, Value = Dict[point.Value], Type = ValueType };
        }
    }
}
