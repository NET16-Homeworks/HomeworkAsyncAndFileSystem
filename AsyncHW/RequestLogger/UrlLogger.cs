using DI.Entities.Entities;
using DI.UserInFile.Interfaces;
using RequestLogger.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RequestLogger
{
    public sealed class UrlLogger : IRequestLogger
    {
        private readonly IFileManager _fileManager;
        private readonly IFileSystemPathProvider _fileSystemPathProvider;

        public UrlLogger(IFileManager fileManager, IFileSystemPathProvider fileSystemPathProvider)
        {
            _fileManager = fileManager;
            _fileSystemPathProvider = fileSystemPathProvider;
        }
        public async Task WriteLogRequest(ActionExecutingContext context)
        {
            var logs = await GetLogs();

            var log = new Log
            {
                Date = DateTime.Now,
                Action = context.HttpContext.Request.Path,
                Controller = context.Controller.GetType().Name
            };

            logs.Add(log);

            await WriteLog(logs);
        }

        private async Task<List<Log>> GetLogs()
        {
            using var reader = File.OpenRead(_fileSystemPathProvider.GetLoggerPath());
            return await JsonSerializer.DeserializeAsync<List<Log>>(reader);
        }

        private async Task WriteLog(IEnumerable<Log> logs)
        {
            await _fileManager.Write(_fileSystemPathProvider.GetLoggerPath(),
                                     JsonSerializer.Serialize(logs,
                                                              new JsonSerializerOptions
                                                              {
                                                                  WriteIndented = true
                                                              }));
        }
    }
}
