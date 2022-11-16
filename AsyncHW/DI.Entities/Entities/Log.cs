using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Entities.Entities
{
    public sealed class Log
    {
        public DateTime Date { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
    }
}
