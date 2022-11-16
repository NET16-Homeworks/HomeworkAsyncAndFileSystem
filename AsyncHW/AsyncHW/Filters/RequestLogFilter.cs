using AsyncHW.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using RequestLogger.Interfaces;

namespace AsyncHW.Filters
{
    public class RequestLogFilter : Attribute, IAsyncActionFilter
    {
        private readonly IRequestLogger _logger;
        public RequestLogFilter(IRequestLogger logger)
        {
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await _logger.WriteLogRequest(context);
            await next();
        }
    }
}
