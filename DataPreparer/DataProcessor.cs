namespace DataProcessor
{
    public class DataProcessor
    {
        private List<RawDataPoint> _rawData;

        private IDataValueMapper _mapper;

        public IEnumerable<ProcessedDataPoint> ProcessedData { get; set; }

        public DataProcessor(IEnumerable<RawDataPoint> rawData, IDataValueMapper mapper)
        {
            _rawData = rawData.ToList();
            _mapper = mapper;
        }

        public void ProcessRawData()
        {
            ProcessedData = _rawData.Select(r => _mapper.MapRawDataPoint(r));
            // TODO
        }
    }
}