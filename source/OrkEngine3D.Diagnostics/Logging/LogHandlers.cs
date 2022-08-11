/*
    MIT License

Copyright (c) 2022 OrkEngine

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;

namespace OrkEngine3D.Diagnostics.Logging;

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
		message.LogType == LogMessageType.UNKNOWN     ? ConsoleColor.DarkGray :
		ConsoleColor.White;

		Console.WriteLine(message.FormattedMessage);
		Console.ResetColor();
	}
}
