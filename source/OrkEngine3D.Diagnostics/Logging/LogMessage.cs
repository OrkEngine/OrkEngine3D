using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrkEngine3D.Diagnostics.API;
using OrkEngine3D.Diagnostics.Exceptions;

namespace OrkEngine3D.Diagnostics.Logging
{
    public class LogMessage : ILogMessage
    {
        public LogMessageType Type { get; set; }
        public string Text { get; set; }
        public ExceptionInfo ExceptionInfo { get; }
    }
}
