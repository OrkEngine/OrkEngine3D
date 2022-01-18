using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkEngine3D.Diagnostics
{
    public class Logger : ILogger
    {
        public string Module
        {
            get;
            protected internal set;
        }

        public void Log(ILogMessege logMessage)
        {
            throw new NotImplementedException();
        }
    }
}
