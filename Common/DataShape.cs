
namespace Common
{
    public struct FieldShape
    {
        public string Name { get; init; }
        public string ValueType { get; init; }
        public string[] UniqueValues { get; set; }
    }
    
    public struct DataShape
    {
        public IEnumerable<FieldShape> Fields { get; init; }

        public Tuple<DateTime, DateTime> DateRange { get; init; }
    }
}
