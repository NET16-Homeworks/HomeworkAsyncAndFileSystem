using System.Text.Json;

namespace HomeworkAsyncAndFileSystem.Helpers
{
    public static class JSONWriter
    {
        public static async Task Write<T>(string directoryName, string fileName, T content)
        {
            string path = Path.Combine(directoryName, fileName);
            await Write(path, content);
        }
    
        public static async Task Write<T>(string path, T content)
        {
            
;            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                var options = new JsonSerializerOptions()
                {
                    WriteIndented = true,
                };
                
                await JsonSerializer.SerializeAsync(fileStream, content, options);
            }
        } 
    }
}
