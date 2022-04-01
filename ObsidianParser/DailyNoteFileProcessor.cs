using Common;
using System.Text.RegularExpressions;
using ObsidianParser.Exceptions;
using System.Diagnostics;

namespace ObsidianParser
{
    internal class DailyNoteFileProcessor
    {
        private List<IDailyNote> _notes = new List<IDailyNote>();

        private string _folderPath;
        private string _dailyNoteFormatForRegex;

        internal DailyNoteFileProcessor(string path, string dailyNoteFormatForRegex = @"\d\d\d\d-\d\d-\d\d")
        {
            _folderPath = path;
            _dailyNoteFormatForRegex = dailyNoteFormatForRegex;
        }

        internal void ReadAndProcessFilesIntoNotes()
        {
            if (_folderPath == null) throw new FileReadingException();
            var filePaths = Directory.GetFiles(this._folderPath);
            var sw = new Stopwatch();
            sw.Start();
            // sync version is 10x faster due to having a lot of small files
            // see also: https://docs.microsoft.com/en-us/windows/win32/fileio/synchronous-and-asynchronous-i-o
            //filePaths.AsParallel().ForAll(ProcessFileAsync); 
            filePaths.ToList().ForEach(ProcessFile);
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
        }

        internal IEnumerable<DataPoint> GetData()
        {
            _notes.ForEach(n => n.PrepareMetadata());
            return _notes.SelectMany(n => n.GetDataPoints());
        }

        private DateTime? ParseDateFromFileName(string fileName)
        {
            var dateRegex = new Regex(_dailyNoteFormatForRegex);
            var match = dateRegex.Match(fileName);
            if (!match.Success) return null;
            var dateIsValid = DateTime.TryParse(match.Value, out DateTime date);
            return dateIsValid ? date : null;
        }

        private void ProcessFile(string path)
        {
            var date = ParseDateFromFileName(path);
            // parse only files which (at least in part) adhere to dailyNoteFormatForRegex
            if (date == null) return;

            var fileContent = File.ReadAllText(path);
            var note = new DailyNote(fileContent, date.Value);
            note.ParseMetadata();
            _notes.Add(note);
        }

        private async void ProcessFileAsync(string path)
        {
            var date = ParseDateFromFileName(path);
            // parse only files which (at least in part) adhere to dailyNoteFormatForRegex
            if (date == null) return;

            var fileContent = await File.ReadAllTextAsync(path);
            var note = new DailyNote(fileContent, date.Value);
            note.ParseMetadata();
            _notes.Add(note);
        }
    }
}
