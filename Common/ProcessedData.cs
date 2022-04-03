namespace Common
{
    public class ProcessedData
    {
        public List<DataSeries> DataSeriesCollection { get; init; }
        public DataShape DataShape { get; init; }
        public Tuple<DateTime, DateTime> DateRange { get; init; }
    }
}
