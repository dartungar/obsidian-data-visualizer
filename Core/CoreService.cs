using Common;
using DataProcessor;
using ObsidianParser;
using Persistence;

namespace Core
{
    public class CoreService
    {
        private IDataProvider _dataProvider;
        private IEnumerable<DataPoint> _rawData;
        private DataProcessorService _dataProcessor;
        private PersistenceService _persistence;
        public ProcessedData ProcessedData { get; private set; }

        public CoreService(DataProcessorService dataProcessor, PersistenceService persistence)
        {
            _dataProcessor = dataProcessor;
            _persistence = persistence;
        }

        public void LoadProcessedDataFromLocalStorage()
        {
            LoadDataFromLocalStorage();
        }

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
            SaveDataToLocalStorage();
        }

        private async void LoadDataFromLocalStorage()
        {
            var data = await _persistence.Load<ProcessedData>();
            if (data != null)
                this.ProcessedData = data; 
        }

        private void SaveDataToLocalStorage()
        {
            _persistence.Save<ProcessedData>(ProcessedData);
        }

        public DataShape GetDataShape() => ProcessedData.DataShape;

        public IEnumerable<DataSeries> GetDataSeries(IEnumerable<string> fieldNames)
            => ProcessedData.DataSeriesCollection.Where(ds => fieldNames.Contains(ds.Name));

        public DataSeries? GetDataSeries(string fieldName) 
            => ProcessedData.DataSeriesCollection.FirstOrDefault(ds => ds.Name == fieldName);

        public IEnumerable<DataSeries> GetDataSeriesCount(IEnumerable<string> fieldNames)
            => fieldNames.Select(name => GetDataSeriesCount(name)).NotNull();

        public DataSeries? GetDataSeriesCount(string fieldName)
            => GetDataSeries(fieldName)?.GetFieldsCount();

        public IEnumerable<DataSeries> GetDataSeriesSum(IEnumerable<string> fieldNames)
            => fieldNames.Select(name => GetDataSeriesSum(name)).NotNull();

        public DataSeries? GetDataSeriesSum(string fieldName) 
            => GetDataSeries(fieldName)?.GetFieldsSum();

        public IEnumerable<DataSeries> GetDataSeriesAvg(IEnumerable<string> fieldNames)
            => fieldNames.Select(name => GetDataSeriesAvg(name)).NotNull();

        public DataSeries? GetDataSeriesAvg(string fieldName) 
            => GetDataSeries(fieldName)?.GetFieldsAverage();


    }
}