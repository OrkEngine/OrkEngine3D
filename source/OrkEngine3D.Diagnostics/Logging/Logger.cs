using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkEngine3D.Diagnostics.Logging
{

	public class Logger
	{
		private static readonly Dictionary<string, Logger> Loggers = new Dictionary<string, Logger>();


		// unique name for each Logger
		private string LoggerIdentifier;



		// have an option for the user to get the Loggers ID
		// this ID is immutable so we dont mess up our mapping
		public string LoggerID
		{
			get 
			{
				return LoggerIdentifier;
			}
		}

		// log handler (will be called when theres a new log message)
		public event HandleLog LogMessageHandler;



		// the module in which the Logger was created
		private string Module { get; }

		// logging history
		private List<LogMessage> LogHistory = new List<LogMessage>();




		// constructors
		public Logger() {}

		public Logger(string LoggerID, string module = "null")
        {
            LoggerIdentifier = LoggerID;
            Module = module;

			// always add the default console log handler
			// (we will probably want a parameter for this later)
			LogMessageHandler += LogHandlers.ConsoleLog;


			// always add loggers automatically
			// (again, we may want to add a parameter for this)
			Add(this);
        }



		// method for logging, using the standard format
		public string Log(LogMessageType type, string message)
        {
            // Timestamp (Texture)-[DEBUG]: msg
			// send this over to Logf() with the standard format
			return Logf(type, message, "%tim% (%mod%)-[%typ%]: %msg%");
        }

		// method for logging, using a custom format
		public string Logf(LogMessageType type, string message, string format)
        {
            // <Timestamp> (Texture)-[DEBUG]: msg
            string typeName;

			// a little shortcut to avoid using a switch (just returns the Enum entry's name)
			typeName = Enum.GetName(typeof(LogMessageType), type);

			// if somehow the user managed to pass in an enum member that doesnt exist.
			if (typeName == null) typeName = "UNKNOWN";

			// format the log message
			string logMessage = FormatLog(Module, typeName, message, format);

			// create a log message object and push it to the history and any subscribed handlers
			LogMessage logMessageObject = new LogMessage(Module, type, message, logMessage);
            LogMessageHandler?.Invoke(logMessageObject);
            LogHistory.Add(logMessageObject);


			// return the formatted message to the user
            return logMessage;
        }

		// very simplistc function for formatting log messages based on a template
		private string FormatLog(string module, string type, string message, string format)
		{
			string formattedLog = format;

			/*
			 * Placeholders:
			 * %mod% -> Module
			 * %typ% -> Type
			 * %msg% -> Message
			 * %tim% -> Timestamp
			 */

			formattedLog = formattedLog.Replace("%mod%", module);
			formattedLog = formattedLog.Replace("%typ%", type);
			formattedLog = formattedLog.Replace("%msg%", message);
			formattedLog = formattedLog.Replace("%tim%", GetTimeStamp());

			return formattedLog;
		}

		// little helper for creating timestamps in the "dd-MM-yy-HH-mm" format
		private string GetTimeStamp()
        {
            return DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        }

		// Static Logger class things
		// --------------------------

		/// <summary>
		/// This will add a Logger to the global Logger map.
		/// </summary>
		public static void Add(Logger logger)
		{
			// if a logger with this name is already registered
			if (Loggers.ContainsKey(logger.LoggerID))
			{
				logger.Log(LogMessageType.ERROR, $"A Logger with the name '{logger.LoggerID}' already exists!");
				return;
			}

			// if the name is still free -> register this logger
			Loggers.Add(logger.LoggerID, logger);
		}

		/// <summary>
		/// This will look for a Logger with the given name, if it doesnt exist the function will return null.
		/// </summary>
		public static Logger Find(string name)
		{
			if (Loggers.ContainsKey(name))
				return Loggers[name];

			return null;
		}

		/// <summary>
		/// This will look for a Logger with the given name, if it doesnt exist a new Logger will be created.
		/// </summary>
		public static Logger Get(string name, string module = "null")
		{
			if (Loggers.ContainsKey(name))
				return Loggers[name];

			// if theres no logger with that name -> create one
			Logger logger = new Logger(name, module);
			return logger;
		}

		/// <summary>
		/// This will check if a logger with the given name is registered.
		/// </summary>
		public static bool Exists(string name)
		{
			return Loggers.ContainsKey(name);
		}

		/// <summary>
		/// This will remove a Logger with the given name, if possible.
		/// </summary>
		public static void Remove(string name)
        {
			// left this out because we have no logger to log to lol
            // > Log(LogMessageType.FATAL, $"Logger for ({name}) doesn't exist!");

            if (Loggers.ContainsKey(name))
                Loggers.Remove(name);
        }
	}
}
