using DataProcessor;

namespace ObsidianParser
{
    internal class DailyNoteDataMapper
    {
        static Dictionary<string, object> Mappings => new Dictionary<string, object>
        {
            {"yes", true },
            {"no", false },
            {"none", 0 },
            {"very low", 1 },
            {"low", 2 },
            {"medium", 3 },
            {"high", 4 },
            {"very high", 5 },
        };


        public static MetadataField MapMetadataFieldValues(MetadataFieldRaw point)
        {
            var values = point.Values.Select(v => Mappings.ContainsKey(v) ? Mappings[v] : v).ToArray();
            var types = values.Select(v => v.GetType()).Distinct();
            var type = types.Count() == 1 ? types.First() : typeof(string); 
            return new MetadataField
            {
                Name = point.Name,
                Values = point.Values.Select(v => Mappings.ContainsKey(v) ? Mappings[v].ToString() : v).ToArray(),
                Type = type,
            };
        }
    }
}
