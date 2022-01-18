using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrkEngine3D.Diagnostics.API;

namespace OrkEngine3D.Diagnostics.Logging
{
    public class Logger : ILogger
    {
        public string Module
        {
            get;
            protected internal set;
        }

        public void Log(ILogMessage logMessage)
        {
            throw new NotImplementedException();
        }
    }
}
