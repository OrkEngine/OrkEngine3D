namespace OrkEngine3D.Diagnostics.Logging
{
	public class LogMessage
	{
		public string Module { get; }
		public LogMessageType LogType { get; }
		public string Message { get; }
		public string FormattedMessage { get; }

		public LogMessage(string module, LogMessageType type, string message, string formattedMessage)
		{
			Module           = module;
			LogType          = type;
			Message          = message;
			FormattedMessage = formattedMessage;
		}
	}

	public enum LogMessageType
    {
		UNKNOWN,
        DEBUG,
        ERROR,
        WARNING,
        INFORMATION,
        SUCCESS,
        FATAL
    }
}
