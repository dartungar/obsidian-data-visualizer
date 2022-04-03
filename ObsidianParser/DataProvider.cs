using Common;
using DataProcessor;

namespace ObsidianParser
{
    public class DataProvider : IDataProvider
    {
        private DailyNoteLoader _dailyNoteLoader;
        public DataProvider(string dataFolderPath)
        {
            _dailyNoteLoader = new DailyNoteLoader(dataFolderPath);
        }

        public DataProvider(string dataFolderPath, string dailyNoteFormatForRegex)
        {
            _dailyNoteLoader = new DailyNoteLoader(dataFolderPath, dailyNoteFormatForRegex);
        }

        public void ReadData() => _dailyNoteLoader.ReadAndProcessFilesIntoNotes();

        public IEnumerable<DataPoint> GetData()
        {
            return _dailyNoteLoader.GetData();
        }
    }
}
