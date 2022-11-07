using Microsoft.AspNetCore.Mvc.Filters;
using HomeworkAsyncAndFileSystem.Classes;

namespace HomeworkAsyncAndFileSystem.Filters
{
    public class RequestLogFilter : IAsyncActionFilter
    {
        UrlLogger _urlLogger = new();

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();

            await _urlLogger.LogRequest(context);
        }
    }
}