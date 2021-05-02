using System;




namespace RI.DomainServices.Common.Logging
{
    /// <summary>
    /// A log sink to provide logging to domain services.
    /// </summary>
    /// <remarks>
    ///<para>
    ///A log sink abstracts the runtime environments way of logging. 
    /// </para>
    /// </remarks>
    public interface ILogSink
    {
        /// <summary>
        /// Writes a log message to the log sink.
        /// </summary>
        /// <param name="timestampUtc">The UTC timestamp to be associated with the log message.</param>
        /// <param name="threadId">The ID of the thread the log message originates from.</param>
        /// <param name="level">The log level of the log message.</param>
        /// <param name="format">The log message (with optional string expansion arguments such as <c>{0}</c>, <c>{1}</c>, etc. which are expanded by <paramref name="args"/>).</param>
        /// <param name="source">The source/origin of the log message.</param>
        /// <param name="exception">Optional exception associated with the log message.</param>
        /// <param name="args">Optional message arguments expanded into <paramref name="format"/>.</param>
        void Log (DateTime timestampUtc, int threadId, LogLevel level, string format, string source, Exception exception, params object[] args);
    }
}
