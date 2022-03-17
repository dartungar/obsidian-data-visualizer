
namespace Common
{
    public struct FieldShape
    {
        public string Name { get; init; }
        public Type ValueType { get; init; }
        public string[] UniqueValues { get; set; }
    }
    
    public struct DataShape
    {
        public IEnumerable<FieldShape> Fields { get; init; }

        public Tuple<DateOnly, DateOnly> DateRange { get; init; }
    }
}
