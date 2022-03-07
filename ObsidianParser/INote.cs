using DataProcessor;

namespace ObsidianParser
{
    internal interface IDailyNote: INote
    {
        public DateOnly Date { get; }
    }
    
    internal interface INote
    {
        public IEnumerable<MetadataFieldRaw> MetadataRaw { get; }
        public IEnumerable<MetadataField> Metadata { get; }
        public void ParseMetadata();
        public void CleanMetadata();
        public IEnumerable<DataPoint> GetDataPoints();
    }
}
