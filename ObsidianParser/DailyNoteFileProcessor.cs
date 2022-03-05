using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using ObsidianParser.Exceptions;

namespace ObsidianParser
{
    internal class DailyNoteFileProcessor
    {
        private List<IDailyNote> _notes;

        private string _folderPath;
        private string _dailyNoteFormatForRegex;

        public DailyNoteFileProcessor(string path, string dailyNoteFormatForRegex = @"\d\d\d\d-\d\d-\d\d")
        {
            _folderPath = path;
            _dailyNoteFormatForRegex = dailyNoteFormatForRegex;
        }

        public void ReadAndProcessFilesIntoNotes()
        {
            if (_folderPath == null) throw new FileReadingException();
            var files = Directory.GetFiles(this._folderPath);
            foreach (var fileName in files)
            {
                var date = ParseDateFromFileName(fileName);
                // parse only files which (at least in part) adhere to dailyNoteFormatForRegex
                if (date == null) continue; 

                var fileContent = File.ReadAllText(_folderPath + fileName);
                var note = new DailyNote(fileContent, date.Value);
                note.ParseMetadata();
                _notes.Add(note);
            }
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
