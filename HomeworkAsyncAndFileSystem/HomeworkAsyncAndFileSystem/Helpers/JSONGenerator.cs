using System.Text.Json;

namespace HomeworkAsyncAndFileSystem.Helpers
{
    public static class JSONGenerator
    {
        public static async void Generate<T>(string directoryName, string fileName, T content)
        {
            string path = Path.Combine(directoryName, fileName);

            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            if (!File.Exists(path))
            {
                using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    var options = new JsonSerializerOptions()
                    {
                        WriteIndented = true
                    };

                    await JsonSerializer.SerializeAsync(fileStream, content, options);
                }
            }
        }
    }
}
