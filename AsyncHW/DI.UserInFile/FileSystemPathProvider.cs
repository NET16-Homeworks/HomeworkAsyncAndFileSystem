using DI.UserInFile.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.UserInFile
{
    public sealed class FileSystemPathProvider : IFileSystemPathProvider
    {
        public string GetAddressesPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "Jsons", "Addresses.json");
        }

        public string GetUsersPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "Jsons", "Users.json");
        }

        public string GetLoggerPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "Jsons", "logs.json");
        }
    }
}
