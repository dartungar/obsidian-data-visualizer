using Common;
using System.Text.RegularExpressions;
using ObsidianParser.Exceptions;

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
            var files = Directory.GetFiles(this._folderPath);
            foreach (var file in files)
            {
                var date = ParseDateFromFileName(file);
                // parse only files which (at least in part) adhere to dailyNoteFormatForRegex
                if (date == null) continue; 

                var fileContent = File.ReadAllText(file);
                var note = new DailyNote(fileContent, date.Value);
                note.ParseMetadata();
                _notes.Add(note);
            }
        }

        internal IEnumerable<DataPoint> GetData()
        {
            _notes.ForEach(n => n.CleanMetadata());
            return _notes.SelectMany(n => n.GetDataPoints());
        }

        private DateOnly? ParseDateFromFileName(string fileName)
        {
            var dateRegex = new Regex(_dailyNoteFormatForRegex);
            var match = dateRegex.Match(fileName);
            if (!match.Success) return null;
            var dateIsValid = DateOnly.TryParse(match.Value, out DateOnly date);
            return dateIsValid ? date : null;
        }
    }
}
