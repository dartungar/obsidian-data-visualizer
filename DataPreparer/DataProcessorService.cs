using Common;
using DataProcessor.Exceptions;

namespace DataProcessor
{
    public class DataProcessorService
    {
        private List<IValueDictionary> _valueDictionaries = new List<IValueDictionary>();

        public DataProcessorService()
        {
            // add default dictionaries for mapping values
            _valueDictionaries.Add(new LevelDictionary());
            _valueDictionaries.Add(new BooleanDictionary());
        }

        public ProcessedData ProcessData(IEnumerable<DataPoint> rawData)
        {
            
            
            if (!rawData.Any())
                throw new DataProcessingException("Could not process data: no raw data"); // TODO: throw exception

            // map values in raw data series
            // first, map custom dictionaries
            // then, apply universal mappings
            var dataPoints = _valueDictionaries.MapValues(rawData, ValueDictionaryType.Custom);
            // TODO: разобраться, почему не мапятся некоторые значения (остаются low, medium etc)
            dataPoints = _valueDictionaries.MapValues(dataPoints, ValueDictionaryType.Universal);

            var dateRange = new Tuple<DateTime, DateTime>(dataPoints.Select(d => d.Date).Min(), dataPoints.Select(d => d.Date).Max());

            var fields = dataPoints.DistinctBy(dp => dp.Name).Select(d =>
                new FieldShape
                {
                    Name = d.Name,
                    ValueType = d.Type?.Name ?? "string", // TODO: test
                    UniqueValues = GetUniqueValues(d.Name)
                })
                .DistinctBy(fs => fs.Name).ToList();

            var dataShape = new DataShape() { DateRange = dateRange, Fields = fields };

            // create DataSeries for all fields
            // TODO: test if parallel is really faster
            var dataSeriesCollection = fields.AsParallel().Select(CreateDataSeriesForField).ToList();

            return new ProcessedData
            {
                DataShape = dataShape,
                DataSeriesCollection = dataSeriesCollection,
                DateRange = dateRange
            };

            string[] GetUniqueValues(string fieldName) => dataPoints.FilterByName(fieldName)?.Select(dp => dp.Value).Distinct().ToArray();

            DataSeries CreateDataSeriesForField(FieldShape field)
                => new()
                {
                    Name = field.Name,
                    ValueType = field.ValueType,
                    Series = dataPoints.FilterByName(field.Name).Select(e => new DataSeriesEntry
                    {
                        Name = e.Date.ToString(),
                        Value = e.Value,
                    }).ToArray()
                };
        }

        // TODO: add custom value dictionary from UI
        public void AddValueDictionary(Dictionary<string, string> dictionary, string fieldName)
        {
            _valueDictionaries.Add(new ValueDictionaryCustom(typeof(string), dictionary, fieldName));
        }
    }
}