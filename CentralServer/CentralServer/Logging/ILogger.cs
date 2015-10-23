using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralServer.Logging
{
    interface ILogger
    {
        void Write(string sender, int category, string text, string timestamp);
    }
}
