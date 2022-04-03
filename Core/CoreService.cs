using Common;
using DataProcessor;
using ObsidianParser;

namespace Core
{
    public class CoreService
    {
        private IDataProvider _dataProvider;
        private IEnumerable<DataPoint> _rawData;
        private DataProcessorService _dataProcessor;
        public ProcessedData ProcessedData { get; private set; }

        public void LoadRawData(string filepath)
        {
            _dataProvider = new DataProvider(filepath);
            _dataProvider.ReadData();
        }

        /// <summary>
        /// Initialize DataProvider and read data
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="dailyNoteFormatForRegex"></param>
        public void LoadRawData(string filepath, string dailyNoteFormatForRegex)
        {
            _dataProvider = new DataProvider(filepath, dailyNoteFormatForRegex);
            _dataProvider.ReadData();
            _rawData = _dataProvider.GetData();
        }

        public void ProcessData()
        {
            _dataProcessor = new DataProcessorService();
            // crutch for testing
            // TODO: fixme
            _dataProcessor.AddValueDictionary(new Dictionary<string, string>()
                {
                    {"low", "1" },
                    {"volatile", "2" },
                    {"stable", "3" },
                    {"high", "4" },
                    {"very high", "5" },
                }, "phase");
            ProcessedData = _dataProcessor.ProcessData(_rawData);
        }

        public DataShape GetDataShape() => ProcessedData.DataShape;


        public IEnumerable<DataSeries> GetDataSeries(IEnumerable<string> fieldNames)
            => ProcessedData.DataSeriesCollection.Where(ds => fieldNames.Contains(ds.Name));

        public DataSeries? GetDataSeries(string fieldName) => ProcessedData.DataSeriesCollection.FirstOrDefault(ds => ds.Name == fieldName);

    }
}