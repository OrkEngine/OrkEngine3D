using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkEngine3D.Diagnostics.Logging.API
{
    public interface ILogMessage
    {
        string Module { get; }

        string Context { get; set; }

        string Log(LogMessageType type, string message);
    }

    public enum LogMessageType
    {
        DEBUG,
        ERROR,
        WARNING,
        INFORMATION,
        SUCCESS,
        FATAL
    }
}
