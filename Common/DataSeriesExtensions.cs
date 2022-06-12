
namespace Common
{
    // TODO: error handling / type guards (don't try to average strings) or bubble errors up?
    public static class DataSeriesExtensions
    {
        public static DataSeries GetFieldsCount(this DataSeries source)
        {
            var fields = source.Series.Select(d => d.Name).Distinct().ToArray();
            return new DataSeries
            {
                Name = source.Name,
                Series = fields.Select(f => new DataSeriesEntry
                {
                    Name = f,
                    Value = source.Series.Where(e => e.Name == f).Count().ToString()
                }).ToArray(),
                ValueType = typeof(int).Name
            };
        }

        public static DataSeries GetFieldsSum(this DataSeries source)
        {
            var fields = source.Series.Select(d => d.Name).Distinct().ToArray();
            return new DataSeries
            {
                Name = source.Name,
                Series = fields.Select(f => new DataSeriesEntry
                {
                    Name = f,
                    Value = source.Series.Where(e => e.Name == f).Select(e => decimal.Parse(e.Value)).Sum().ToString()
                }).ToArray(),
                ValueType = typeof(decimal).Name
            };
        }

        public static DataSeries GetFieldsAverage(this DataSeries source)
        {
            var fields = source.Series.Select(d => d.Name).Distinct().ToArray();
            return new DataSeries
            {
                Name = source.Name,
                Series = fields.Select(f => new DataSeriesEntry
                {
                    Name = f,
                    Value = source.Series.Where(e => e.Name == f).Select(e => decimal.Parse(e.Value)).Average().ToString()
                }).ToArray(),
                ValueType = typeof(decimal).Name
            };
        }
    }

}
