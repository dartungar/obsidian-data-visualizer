﻿using Common;

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
            _fields = DataPoints.Select(d =>
                new FieldShape { Name = d.Name, ValueType = d.Type.Name, UniqueValues = DataPoints.FilterByName(d.Name).SelectMany(dp => dp.Values).Distinct().ToArray() })
                .DistinctBy(fs => fs.Name).ToList();
            _fields.ForEach(FillUniqueValues);

            _dataShape = new DataShape() { DateRange = _dateRange, Fields = _fields };

            void FillUniqueValues(FieldShape fs)
            {
                var uniq = DataPoints.FilterByName(fs.Name).SelectMany(dp => dp.Values).Distinct().ToArray();
                fs.UniqueValues = DataPoints.FilterByName(fs.Name).SelectMany(dp => dp.Values).Distinct().ToArray();
            }
        }

        public DataShape GetDataShape() => _dataShape;

        public Tuple<DateTime, DateTime> GetDateRange() => _dateRange;

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
    }
}