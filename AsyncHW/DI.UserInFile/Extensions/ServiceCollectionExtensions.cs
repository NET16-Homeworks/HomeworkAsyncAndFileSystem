using DI.UserInFile.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DI.UserInFile.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUsersManagementByFile(this IServiceCollection services)
        {
            services.AddSingleton<IFileManager, StaticFilesFileManager>();
            services.AddSingleton<IFileSystemPathProvider, FileSystemPathProvider>();

            services.AddScoped<IUserService, InFileUserService>();

            return services;
        }
    }
}
