
namespace Common
{
    public struct DataPoint
    {
        public DateTime Date { get; init; }
        public string Name { get; init; }
        public string[] Values { get; init; }
        public Type Type { get; init; }
    }
}
