using Microsoft.AspNetCore.Mvc.Filters;
using HomeworkAsyncAndFileSystem.Classes;

namespace HomeworkAsyncAndFileSystem.Filters
{
    public class RequestLogFilter : IActionFilter
    {
        UrlLogger _urlLogger = new();

        ActionExecutingContext executingContext;

        public void OnActionExecuted(ActionExecutedContext executedContext)
        {
            _urlLogger.LogRequestAsync(executedContext, executingContext);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            executingContext = context;
        }
    }
}