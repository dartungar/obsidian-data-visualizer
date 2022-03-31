using Common;

namespace DataProcessor
{
    public class DataProcessor
    {
        public IEnumerable<DataPoint> DataPoints { get; set; }
        private Tuple<DateTime, DateTime> _dateRange;
        private List<FieldShape> _fields;
        private DataShape _dataShape;

        public DataProcessor(IEnumerable<DataPoint> rawData)
        {
            DataPoints = rawData;
        }

        public void ProcessData()
        {
            if (!DataPoints.Any()) return;

            _dateRange = new Tuple<DateTime, DateTime>(DataPoints.Select(d => d.Date).Min(), DataPoints.Select(d => d.Date).Max());
            
            _fields = DataPoints.DistinctBy(dp => new { dp.Name, dp.Value}).Select(d =>
                new FieldShape { 
                    Name = d.Name, 
                    ValueType = d.Type.Name, 
                    UniqueValues = GetUniqueValues(d.Name) })
                .DistinctBy(fs => fs.Name).ToList();

            _dataShape = new DataShape() { DateRange = _dateRange, Fields = _fields };

            string[] GetUniqueValues(string fieldName) => DataPoints.FilterByName(fieldName)?.Select(dp => dp.Value).Distinct().ToArray();

        }

        public DataShape GetDataShape() => _dataShape;

        public Tuple<DateTime, DateTime> GetDateRange() => _dateRange;

        public IEnumerable<TimeSeries> GetTimeSeries(IEnumerable<string> fieldNames)
            => fieldNames.Select(fieldName => GetTimeSeries(fieldName)).Where(ts => ts != null).Select(ts => ts.Value);


        public TimeSeries? GetTimeSeries(string fieldName)
        {
            var data = DataPoints.FilterByName(fieldName);

            if (!data.Any()) return null;

            return new TimeSeries()
            {
                Name = fieldName,
                ValueType = data.First().Type.Name,
                Entries = data.Select(e => new TimeSeriesEntry
                {
                    Name = e.Date.ToString(),
                    Value = e.Value,
                }).ToArray(),
            };
        }
    }
}