
namespace Common
{
    [Serializable] // TODO: это нужно?
    public struct DataSeries
    {
        public string Name { get; init; }
        public string ValueType { get; init; }
        public DataSeriesEntry[] Series { get; init; }

    }

    [Serializable]
    public struct DataSeriesEntry
    {
        public string Name { get; init; }
        public string Value { get; init; }
    }
}
