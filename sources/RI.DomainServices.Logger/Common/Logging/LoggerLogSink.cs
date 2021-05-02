using System;

using Microsoft.Extensions.Logging;

using RI.Utilities.Dates;
using RI.Utilities.Exceptions;




namespace RI.DomainServices.Common.Logging
{
    /// <summary>
    ///     Logger log sink.
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    public sealed class LoggerLogSink : ILogSink
    {
        #region Instance Constructor/Destructor

        /// <summary>
        ///     Creates a new instance of <see cref="LoggerLogSink" />.
        /// </summary>
        /// <param name="logger"> The logger to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="logger" /> is null. </exception>
        public LoggerLogSink(ILogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            this.Logger = logger;
        }

        /// <summary>
        ///     Creates a new instance of <see cref="LoggerLogSink" />.
        /// </summary>
        /// <param name="loggerFactory"> The logger factory to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="loggerFactory" /> is null. </exception>
        public LoggerLogSink(ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            this.Logger = loggerFactory.CreateLogger("DomainServices");
        }

        #endregion




        #region Instance Properties/Indexer

        /// <summary>
        ///     Gets the logger to use.
        /// </summary>
        /// <value>
        /// The logger to use.
        /// </value>
        public ILogger Logger { get; }

        #endregion

        /// <inheritdoc />
        public void Log(DateTime timestampUtc, int threadId, LogLevel level, string source, string format, Exception exception, params object[] args)
        {
            if (exception != null)
            {
                this.Logger.Log(this.TranslateLogLevel(level), exception, $"[{timestampUtc.ToSortableString('-')}] [{threadId}] [{level}] [{source}]{Environment.NewLine}[EXCEPTION]{Environment.NewLine}{exception.ToDetailedString()}{Environment.NewLine}[MESSAGE]{Environment.NewLine}{string.Format(format, args)}");
            }
            else
            {
                this.Logger.Log(this.TranslateLogLevel(level), $"[{timestampUtc.ToSortableString('-')}] [{threadId}] [{level}] [{source}] {string.Format(format, args)}");
            }
        }

        private Microsoft.Extensions.Logging.LogLevel TranslateLogLevel (LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return Microsoft.Extensions.Logging.LogLevel.Debug;
                case LogLevel.Information:
                    return Microsoft.Extensions.Logging.LogLevel.Information;
                case LogLevel.Warning:
                    return Microsoft.Extensions.Logging.LogLevel.Warning;
                case LogLevel.Error:
                    return Microsoft.Extensions.Logging.LogLevel.Error;
                case LogLevel.Fatal:
                    return Microsoft.Extensions.Logging.LogLevel.Critical;
                default:
                    return Microsoft.Extensions.Logging.LogLevel.None;
            }
        }
    }
}
