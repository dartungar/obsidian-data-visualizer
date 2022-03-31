
namespace Common
{
    [Serializable] // TODO: это нужно?
    public struct TimeSeries
    {
        public string Name { get; init; }
        public string ValueType { get; init; }
        public TimeSeriesEntry[] Entries { get; init; }

    }

    [Serializable]
    public struct TimeSeriesEntry
    {
        public string Name { get; init; }
        public string Value { get; init; }
    }
}
