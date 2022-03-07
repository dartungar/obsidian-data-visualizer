using DataProcessor;

namespace ObsidianParser
{
    public class ObsidianDataProvider : IDataProvider
    {
        private DailyNoteFileProcessor _processor;
        public ObsidianDataProvider(string dataFolderPath)
        {
            _processor = new DailyNoteFileProcessor(dataFolderPath);
        }

        public void ReadData() => _processor.ReadAndProcessFilesIntoNotes();

        public IEnumerable<DataPoint> GetData()
        {
            return _processor.GetData();
        }
    }
}
