using DataProcessorClass = DataProcessor.DataProcessor;
using DataProcessor;
using ObsidianParser;
using Common;

namespace Core
{
    public class CoreService
    {
        private IDataProvider _dataProvider;
        private IEnumerable<DataPoint> _data;
        private DataProcessorClass _dataProcessor;
        // TODO: инициализировать Parser с заданной папкой для файла и форматом

        public void InitDataProvider(string filepath)
        {
            _dataProvider = new ObsidianDataProvider(filepath);
            _dataProvider.ReadData();
        }

        public void InitDataProvider(string filepath, string dailyNoteFormatForRegex)
        {
            _dataProvider = new ObsidianDataProvider(filepath, dailyNoteFormatForRegex);
            _dataProvider.ReadData();
        }

        public void GetRawData() 
        {
            _data = _dataProvider.GetData();            
        }

        public void ProcessData()
        {
            _dataProcessor = new DataProcessorClass(_data);
            _dataProcessor.ProcessData();
        }
        
        // TODO: получить "форму" данных (какие есть поля, какой date range)
        // TODO: IDataShape тут, реализация в DataProcessor
        public DataShape GetDataShape() => _dataProcessor.GetDataShape();


        public IEnumerable<TimeSeries> GetTimeSeries(IEnumerable<string> fieldNames) => _dataProcessor.GetTimeSeries(fieldNames);
        public TimeSeries? GetTimeSeries(string fieldName) => _dataProcessor.GetTimeSeries(fieldName);

    }
}