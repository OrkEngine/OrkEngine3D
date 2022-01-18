using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrkEngine3D.Diagnostics.Logging.API;

namespace OrkEngine3D.Diagnostics.Logging
{
    public class Logger : ILogMessage
    {
        public Logger()
        {

            /*
             * LogMessage logmsg = new LogMessage();
             * logmsg.Log(ILogMessage
             *
             * LogMessage.Log ?
             * LogMessage.Report
             *
             * Logger.Log
             *
             * Logger logger = new Logger();
             * logger.Log()
             *
             * Check.
             */
        }

        public Logger(string module)
        {
            Module = module;
        }

        public string Module { get; }

        public string Log(LogMessageType type, string message)
        {
            //TESTS ... (Texture)-[DEBUG]: msg
            string typeName;

            // Converting the type to string
            //TODO: maybe seperate function
            switch (type)
            {
                case LogMessageType.DEBUG:
                    typeName = "DEBUG";
                    break;
                case LogMessageType.ERROR:
                    typeName = "ERROR";
                    break;
                case LogMessageType.FAILURE:
                    typeName = "FAILURE";
                    break;
                case LogMessageType.WARNING:
                    typeName = "WARNING";
                    break;
                case LogMessageType.INFORMATION:
                    typeName = "INFO";
                    break;
                case LogMessageType.SUCCESS:
                    typeName = "SUCCESS";
                    break;
                default:
                    typeName = "UNKNOWN";
                    break;
                
            }

            return $"({Module})-[{typeName}]: {message}";
        }
    }
}
