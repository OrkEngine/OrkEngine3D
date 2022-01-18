using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkEngine3D.Diagnostics.API
{
    public interface ILogger
    {
        string Module { get; }

        void Log(ILogMessege logMessage);
    }
}
