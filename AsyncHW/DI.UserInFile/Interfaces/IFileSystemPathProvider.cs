using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.UserInFile.Interfaces
{
    public interface IFileSystemPathProvider
    {
        string GetUsersPath();
        string GetAddressesPath();
        string GetLoggerPath();
    }
}
