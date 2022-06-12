using System.Text.Json;

namespace Persistence
{
    public class FileSaveException: Exception { }

    public class FileLoadException: Exception { }

    public class PersistenceService
    {
        private readonly string _savedDataLocation;

        public PersistenceService(string dataLocation = "./data/")
        {
            _savedDataLocation = dataLocation;
        }
        
        public async void Save<T>(T data)
        {
            var json = JsonSerializer.Serialize(data);
            await File.WriteAllTextAsync($"{_savedDataLocation}{typeof(T).Name}.json", json);
        }

        public async Task<T?> Load<T>()
        {
            var json = await File.ReadAllTextAsync($"{_savedDataLocation}{typeof(T).Name}.json");
            var data = JsonSerializer.Deserialize<T>(json);
            return data;
        }
    }
}