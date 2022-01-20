using System;

namespace OrkEngine3D.Diagnostics.Logging
{
	public delegate void HandleLog(LogMessage message);
	
	// a static class to hold some built-in hanlders
	// only added a console handler for now but we may also add ones for log files, etc...
	public static class LogHandlers
	{
		// standard console logger
		public static void ConsoleLog(LogMessage message)
		{
			// some colors for the log types
			Console.ForegroundColor =
			message.LogType == LogMessageType.DEBUG       ? ConsoleColor.Gray     :
			message.LogType == LogMessageType.ERROR       ? ConsoleColor.Red      :
			message.LogType == LogMessageType.FATAL       ? ConsoleColor.DarkRed  :
			message.LogType == LogMessageType.WARNING     ? ConsoleColor.Yellow   :
			message.LogType == LogMessageType.SUCCESS     ? ConsoleColor.Green    :
			message.LogType == LogMessageType.INFORMATION ? ConsoleColor.Cyan     :
			message.LogType == LogMessageType.UNKNOWN      ? ConsoleColor.DarkGray :
			ConsoleColor.White;

			Console.WriteLine(message.FormattedMessage);
			Console.ResetColor();
		}
	}
}
