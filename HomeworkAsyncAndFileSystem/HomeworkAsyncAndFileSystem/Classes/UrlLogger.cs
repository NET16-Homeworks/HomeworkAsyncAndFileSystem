using Microsoft.AspNetCore.Mvc.Filters;

namespace HomeworkAsyncAndFileSystem.Classes
{
    public class UrlLogger
    {
        public async Task LogRequestAsync(ActionExecutedContext executedContext, ActionExecutingContext executingContext)
        {
            string pathToLogsJSON = Constants.Path.GetLogsJSONFullPath();

            using (StreamWriter streamWriter = File.AppendText(pathToLogsJSON))
            {
                string info = $"{DateTime.Now} - {executedContext.Controller.GetType().Name} - {executingContext.HttpContext.Request.Path}";
                await streamWriter.WriteLineAsync(info);
            }
        }
    }
}