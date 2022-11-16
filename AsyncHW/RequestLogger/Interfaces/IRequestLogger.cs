using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RequestLogger.Interfaces
{
    public interface IRequestLogger
    {
        Task WriteLogRequest(ActionExecutingContext context);
    }
}
