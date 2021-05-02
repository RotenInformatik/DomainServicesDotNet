using System;

using Microsoft.Extensions.Logging;

using RI.DomainServices.Common.Logging;




namespace RI.DomainServices.Common.Builder
{
    /// <summary>
    ///     Provides utility/extension methods for the <see cref="DomainServiceBuilder" /> type.
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    public static class LoggerDomainServiceBuilderExtensions
    {
        #region Static Methods

        /// <summary>
        ///     Registers services for using a logger as log sink.
        /// </summary>
        /// <param name="builder"> The service builder being configured. </param>
        /// <param name="logger"> The logger to use. </param>
        /// <returns> The service builder being configured. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> or <paramref name="logger" /> is null. </exception>
        public static DomainServiceBuilder UseLogger(this DomainServiceBuilder builder, ILogger logger)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            builder.AddSingleton(typeof(ILogSink), new LoggerLogSink(logger));

            return builder;
        }

        /// <summary>
        ///     Registers services for using a logger as log sink.
        /// </summary>
        /// <param name="builder"> The service builder being configured. </param>
        /// <param name="loggerFactory"> The logger factory to use. </param>
        /// <returns> The service builder being configured. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> or <paramref name="loggerFactory" /> is null. </exception>
        public static DomainServiceBuilder UseLoggerFactory(this DomainServiceBuilder builder, ILoggerFactory loggerFactory)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            builder.AddSingleton(typeof(ILogSink), new LoggerLogSink(loggerFactory));

            return builder;
        }

        #endregion
    }
}
