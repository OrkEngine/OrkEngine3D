/*
 * Copyright (c) OrkEngine3D
 * Inspired by Stride3D, but not copied.
 */
using OrkEngine3D.Diagnostics.Exceptions;

namespace OrkEngine3D.Diagnostics.API
{
    public interface ILogMessage
    {
        LogMessageType Type { get; set; }

        string Text { get; set; }

        ExceptionInfo ExceptionInfo { get; }
    }

    public enum LogMessageType
    {
        /// <summary>
        /// A debug message (level 0).
        /// </summary>
        /// <remarks>
        /// Level 0: Debug infomation in build.
        /// </remarks>
        Debug = 0,

        /// <summary>
        /// A verbose message.
        /// </summary>
        /// <remarks>
        /// Level 1: Output all debug info.
        /// </remarks>
        Verbose = 1,

        /// <summary>
        /// An regular info message.
        /// </summary>
        /// <remarks>
        /// Level 2: Can continue, displays regular infomation.
        /// </remarks>
        Info = 2,

        /// <summary>
        /// A warning message.
        /// </summary>
        /// <remarks>
        /// Level 3: Can continue, with warning
        /// </remarks>
        Warning = 3,

        /// <summary>
        /// An error message.
        /// </summary>
        /// <remarks>
        /// Level 4: Cannot Continue, with error.
        /// </remarks>
        Error = 4,

        /// <summary>
        /// A Fatal error message.
        /// </summary>
        /// <remarks>
        /// Level 5: Something went horribly wrong. Can't continue core process.
        /// </remarks>
        Fatal = 5,
    }
}
