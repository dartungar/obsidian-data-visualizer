using Common;

namespace ObsidianParser
{
    internal interface IDailyNote: INote
    {
        public DateTime Date { get; }
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
