using System.Text.RegularExpressions;
using DataProcessor;
using ObsidianParser.Exceptions;

namespace ObsidianParser
{
    public class DailyNote: IDailyNote
    {
        private string _rawContent;
        private DateOnly _date;
        private IEnumerable<RawDataPoint> _metadataFields;
        private static Regex _wholeMetadataRegex = new Regex(@"---\n(.*\n)+---");
        private static Regex _metadataLineRegex = new Regex(@"^(\w+)\s?:\s?(\w+|\[(\s?[\w]\s?,?){1,}\s?\])\n");

        public DateOnly Date => _date;
        public IEnumerable<RawDataPoint> Metadata => _metadataFields;

        public DailyNote(string content, DateOnly date)
        {
            _rawContent = content;
            _date = date;
        }
        public void ParseMetadata()
        {
            var rawMetadata = ExtractRawMetadata();
            var parsedMetadata = rawMetadata.Split('\n').Select(ParseMetadataFromLine);
            _metadataFields = parsedMetadata.Where(m => m != null).Select(m => m.Value);
        }

        private string ExtractRawMetadata() => _wholeMetadataRegex.Match(_rawContent).Groups[0].Value ?? throw new ParserException();

        private RawDataPoint? ParseMetadataFromLine(string line)
        {
            var match = _metadataLineRegex.Match(line);
            // multiple values
            if (!match.Success) return null;

            var name = match.Groups[0].Value;
            var values = new List<string>();
            if (match.Groups[2].Success)
            {
                values.AddRange(match.Groups[2].Value.Split(','));
            }
            else values.Add(match.Groups[1].Value);

            return new RawDataPoint { Name = name, Values = values };
        }

    }
}