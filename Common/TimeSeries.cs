
namespace Common
{
    [Serializable] // TODO: это нужно?
    public struct TimeSeries
    {
        public string Name { get; init; }
        public Dictionary<string, string[]> Entries { get; init; }

    }
}
