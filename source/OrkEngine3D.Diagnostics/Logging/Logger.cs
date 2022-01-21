using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrkEngine3D.Diagnostics.Logging.API;
using OrkEngine3D.Diagnostics.Logging.Internal;

namespace OrkEngine3D.Diagnostics.Logging
{
    public class Logger : ILogMessage
    {
        private LogManager logManager = new LogManager();

        private static readonly Dictionary<string, Logger> loggers = new Dictionary<string, Logger>();

        private string loggerIdentifier;

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

        public string LoggerID
        {
            get
            {
                return loggerIdentifier;
            }
            set
            {
                loggerIdentifier = value;
            }
        }

        public Logger(string loggerID, string module)
        {
            LoggerID = loggerID;
            Module = module;
        }

        public Logger GetLogger(string name, string module = "null")
        {
            //Returns current Logger
            if (loggers.ContainsKey(name)) return loggers[name];

            //might be annoying maybe remove later.
            if (module is null) module = "null";

            return Add(name, module);
        }

        public bool Exists(string name)
        {
            if (loggers.ContainsKey(name))
                return true;

            return false;
        }

        public Logger Add(string name, string module)
        {
            //Register a new instance
            Logger logger = new Logger(name, module);

            //Add logger to dictionary
            loggers.Add(name, logger);

            return logger;
        }

        public void Remove(string name)
        {
            if (!loggers.ContainsKey(name))
            {
                Log(LogMessageType.FATAL, $"Logger for ({name}) doesn't exist!");
            } 
            else //if (loggers.ContainsKey(name))
            {
                loggers.Remove(name);
            }

        }

        public string Module { get; }
        public string Context { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
                case LogMessageType.FATAL:
                    typeName = "FATAL";
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

            //Pushes LogManager

            logManager.Push($"({Module})-[{typeName}]: {message}");
            return $"({Module})-[{typeName}]: {message}";
        }
    }
}
