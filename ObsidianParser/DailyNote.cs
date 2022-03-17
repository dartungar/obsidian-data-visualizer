using System.Text.RegularExpressions;
using ObsidianParser.Exceptions;
using Common;

namespace ObsidianParser
{
    public class DailyNote: IDailyNote
    {
        private string _rawContent;
        private DateOnly _date;
        private IEnumerable<MetadataFieldRaw> _metadataFieldsRaw = new List<MetadataFieldRaw>();
        private static Regex _wholeMetadataRegex = new Regex(@"---\n(.*\n)+---");
        private static Regex _metadataLineRegex = new Regex(@"^(\w+)\s*:\s*(\w+|(\[(\s*[\w]\s*,?){1,}\s*\]))$");

        public DateOnly Date => _date;
        public IEnumerable<MetadataFieldRaw> MetadataRaw => _metadataFieldsRaw;
        public IEnumerable<MetadataField> Metadata { get; private set; }

        public DailyNote(string content, DateOnly date)
        {
            _rawContent = content;
            _date = date;
        }
        public void ParseMetadata()
        {
            var rawMetadata = ExtractRawMetadata();
            if (rawMetadata.Equals("")) return;
            var parsedMetadata = rawMetadata.Split('\n').Select(l => ParseMetadataFromLine(l));
            _metadataFieldsRaw = parsedMetadata.Where(m => m != null).Select(m => m.Value);
        }

        public void CleanMetadata()
        {
            if (!_metadataFieldsRaw.Any()) return;
            Metadata = _metadataFieldsRaw.Select(DailyNoteDataMapper.MapMetadataFieldValues);
        }

        public IEnumerable<DataPoint> GetDataPoints()
        {
            if (Metadata == null || !Metadata.Any()) return new List<DataPoint>();
            return Metadata.Select(MetadataToDataPoint);
        }

        private DataPoint MetadataToDataPoint(MetadataField metadataField)  
            => new DataPoint { Date = _date, Name = metadataField.Name, Values = metadataField.Values, Type = metadataField.Type };
        

        private string ExtractRawMetadata() => _wholeMetadataRegex.Match(_rawContent).Groups[0].Value ?? throw new ParserException();

        private MetadataFieldRaw? ParseMetadataFromLine(string line)
        {
            var match = _metadataLineRegex.Match(line);
            if (!match.Success) return null;

            var name = match.Groups[1].Value;
            var values = new List<string>();
            if (match.Groups[3].Success)
            {
                values.AddRange(match.Groups[3].Value.Replace("[", string.Empty).Replace("]", string.Empty).Split(','));
            }
            else values.Add(match.Groups[2].Value);

            return new MetadataFieldRaw { Name = name, Values = values.Select(v => v.Trim()).ToArray() };
        }

    }
}