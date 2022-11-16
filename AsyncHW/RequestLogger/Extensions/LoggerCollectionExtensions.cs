using DI.UserInFile.Interfaces;
using DI.UserInFile;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RequestLogger.Interfaces;

namespace RequestLogger.Extensions
{
    public static class LoggerCollectionExtensions
    {
        public static IServiceCollection AddUrlLogger(this IServiceCollection services)
        {
            services.AddScoped<IRequestLogger, UrlLogger>();

            return services;
        }
    }
}
