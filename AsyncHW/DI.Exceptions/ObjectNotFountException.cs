using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Exceptions
{
    public class ObjectNotFountException : Exception
    {
        public ObjectNotFountException(string objName) : base($"{objName} not founded")
        {

        }
    }
}
