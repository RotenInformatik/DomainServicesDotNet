using System;
using System.Threading;




namespace RI.DomainServices.Common.Logging
{
    /// <summary>
    ///     Provides utility/extension methods for the <see cref="ILogSink" /> type.
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    public static class ILogSinkExtensions
    {
        private const string DefaultSource = "RI.DomainServices";

        /// <summary>
        /// Writes a log message to the log sink.
        /// </summary>
        /// <param name="logger">The used logger.</param>
        /// <param name="level">The log level of the log message.</param>
        /// <param name="format">The log message (with optional string expansion arguments such as <c>{0}</c>, <c>{1}</c>, etc. which are expanded by <paramref name="args"/>).</param>
        /// <param name="source">The source/origin of the log message.</param>
        /// <param name="exception">Optional exception associated with the log message.</param>
        /// <param name="args">Optional message arguments expanded into <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="logger"/> is null.</exception>
        public static void Log (this ILogSink logger, LogLevel level, string format, string source = ILogSinkExtensions.DefaultSource, Exception exception = null, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.Log(DateTime.UtcNow, Thread.CurrentThread.ManagedThreadId, level, format, source, exception, args);
        }

        /// <summary>
        /// Writes a log message to the log sink.
        /// </summary>
        /// <param name="logger">The used logger.</param>
        /// <param name="format">The log message (with optional string expansion arguments such as <c>{0}</c>, <c>{1}</c>, etc. which are expanded by <paramref name="args"/>).</param>
        /// <param name="source">The source/origin of the log message.</param>
        /// <param name="exception">Optional exception associated with the log message.</param>
        /// <param name="args">Optional message arguments expanded into <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="logger"/> is null.</exception>
        public static void LogDebug (this ILogSink logger, string format, string source = ILogSinkExtensions.DefaultSource, Exception exception = null, params object[] args) => logger.Log(LogLevel.Debug, format, source, exception, args);

        /// <summary>
        /// Writes a log message to the log sink.
        /// </summary>
        /// <param name="logger">The used logger.</param>
        /// <param name="format">The log message (with optional string expansion arguments such as <c>{0}</c>, <c>{1}</c>, etc. which are expanded by <paramref name="args"/>).</param>
        /// <param name="source">The source/origin of the log message.</param>
        /// <param name="exception">Optional exception associated with the log message.</param>
        /// <param name="args">Optional message arguments expanded into <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="logger"/> is null.</exception>
        public static void LogInformation(this ILogSink logger, string format, string source = ILogSinkExtensions.DefaultSource, Exception exception = null, params object[] args) => logger.Log(LogLevel.Information, format, source, exception, args);

        /// <summary>
        /// Writes a log message to the log sink.
        /// </summary>
        /// <param name="logger">The used logger.</param>
        /// <param name="format">The log message (with optional string expansion arguments such as <c>{0}</c>, <c>{1}</c>, etc. which are expanded by <paramref name="args"/>).</param>
        /// <param name="source">The source/origin of the log message.</param>
        /// <param name="exception">Optional exception associated with the log message.</param>
        /// <param name="args">Optional message arguments expanded into <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="logger"/> is null.</exception>
        public static void LogWarning(this ILogSink logger, string format, string source = ILogSinkExtensions.DefaultSource, Exception exception = null, params object[] args) => logger.Log(LogLevel.Warning, format, source, exception, args);

        /// <summary>
        /// Writes a log message to the log sink.
        /// </summary>
        /// <param name="logger">The used logger.</param>
        /// <param name="format">The log message (with optional string expansion arguments such as <c>{0}</c>, <c>{1}</c>, etc. which are expanded by <paramref name="args"/>).</param>
        /// <param name="source">The source/origin of the log message.</param>
        /// <param name="exception">Optional exception associated with the log message.</param>
        /// <param name="args">Optional message arguments expanded into <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="logger"/> is null.</exception>
        public static void LogError(this ILogSink logger, string format, string source = ILogSinkExtensions.DefaultSource, Exception exception = null, params object[] args) => logger.Log(LogLevel.Error, format, source, exception, args);

        /// <summary>
        /// Writes a log message to the log sink.
        /// </summary>
        /// <param name="logger">The used logger.</param>
        /// <param name="format">The log message (with optional string expansion arguments such as <c>{0}</c>, <c>{1}</c>, etc. which are expanded by <paramref name="args"/>).</param>
        /// <param name="source">The source/origin of the log message.</param>
        /// <param name="exception">Optional exception associated with the log message.</param>
        /// <param name="args">Optional message arguments expanded into <paramref name="format"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="logger"/> is null.</exception>
        public static void LogFatal(this ILogSink logger, string format, string source = ILogSinkExtensions.DefaultSource, Exception exception = null, params object[] args) => logger.Log(LogLevel.Fatal, format, source, exception, args);
    }
}
