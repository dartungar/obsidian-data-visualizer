namespace DataProcessor
{
    public class DataProcessor
    {
        public IEnumerable<DataPoint> DataPoints { get; set; }
        private Tuple<DateOnly, DateOnly> _dateRange;
        private IEnumerable<FieldShape> _fields;
        private DataShape _dataShape;

        public DataProcessor(IEnumerable<DataPoint> rawData)
        {
            DataPoints = rawData;
        }

        public void ProcessData()
        {
            _dateRange = new Tuple<DateOnly, DateOnly>(DataPoints.Select(d => d.Date).Min(), DataPoints.Select(d => d.Date).Max());
            _fields = DataPoints.Select(d => new FieldShape { Name = d.Name, ValueType = d.Type }).Distinct();
            _dataShape = new DataShape() { DateRange = _dateRange , Fields = _fields };
        }

        public DataShape GetDataShape() => _dataShape;

        public Tuple<DateOnly, DateOnly> GetDateRange() => _dateRange;

        public IEnumerable<TimeSeries> GetTimeSeries(IEnumerable<string> fieldNames)
            => fieldNames.Select(fieldName => GetTimeSeries(fieldName)).Where(ts => ts != null).Select(ts => ts.Value);

        // вероятно именно так я и буду это дело использовать:
        // 1. Получаем форму данных
        // 2. Выбираем на основе списка доступных полей поле
        // 3. Получаем данные с сервера по этому полю для построения данных
        public TimeSeries? GetTimeSeries(string fieldName)
        {
            var data = DataPoints.FilterByName(fieldName);

            if (!data.Any()) return null;

            return new TimeSeries()
            {
                Name = fieldName,
                Entries = data.ToDictionary(d => d.Date.ToString(), d => d.Values)
            };
        }



        public Dictionary<string, int> GetValueCount(string fieldName)
        {
            var data = DataPoints.FilterByName(fieldName);
            var allValues = data.SelectMany(d => d.Values);
            var distinctValues = allValues.Distinct();
            return distinctValues.ToDictionary(d => d, d => allValues.Where(v => v == d).Count());

        }
    }
}