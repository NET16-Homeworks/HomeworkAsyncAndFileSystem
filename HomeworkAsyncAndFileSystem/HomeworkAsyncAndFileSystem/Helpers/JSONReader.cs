using System.Text.Json;

namespace HomeworkAsyncAndFileSystem.Helpers
{
    public static class JSONReader
    {
       public static async Task<T> Read<T>(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                return await JsonSerializer.DeserializeAsync<T>(fileStream);
            }
        }
    }
}
