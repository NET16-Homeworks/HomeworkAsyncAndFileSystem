using System.Text.Json;

namespace HomeworkAsyncAndFileSystem.Helpers
{
    public static class JSONWriter
    {
        public static async void Write<T>(string directoryName, string fileName, T content)
        {
            string path = Path.Combine(directoryName, fileName);

            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            await Write(path, content);
        }
    
        public static async Task Write<T>(string path, T content)
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
