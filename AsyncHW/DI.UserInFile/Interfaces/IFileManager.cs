using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.UserInFile.Interfaces
{
    public interface IFileManager
    {
        Task<string> Read(string fileName);

        Task Write(string fileName, string text);
    }
}
