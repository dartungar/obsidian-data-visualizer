using Core;

namespace DataProcessor
{
    public struct DataPoint : IDataPoint
    {
        public DateOnly Date { get; init; }
        public string Name { get; init; }
        public string[] Values { get; init; }
        public Type Type { get; init; }
    }
}
