using System;
using Serilog;
using Serilog.Core;

namespace Tinyblog.Common.Log.Implementations
{
    /// <summary>
    /// Logger on serilog.
    /// </summary>
    /// <seealso cref="Tinyblog.Common.Log.ILogger"/>
    public class SerilogLogger : ILogger
    {
        private readonly Logger logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("log.txt").CreateLogger();

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Debug(string message)
        {
            logger.Debug(message);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Error(string message)
        {
            logger.Error(message);
        }

        /// <summary>
        /// Errors the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="message">The message.</param>
        public void Error(Exception ex, string message)
        {
            logger.Error(ex, message);
        }
    }
}
