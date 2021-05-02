using System;
using System.Text.Json;

using RI.DomainServices.Common.Builder;
using RI.DomainServices.Node.Serialization;




namespace RI.DomainServices.Node.Builder
{
    /// <summary>
    ///     Provides utility/extension methods for the <see cref="DomainServiceNodeBuilder" /> type.
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    public static class JsonDomainServiceNodeBuilderExtensions
    {
        #region Static Methods

        /// <summary>
        ///     Registers services for using JSON as integration and domain event serialization format.
        /// </summary>
        /// <param name="builder"> The service builder being configured. </param>
        /// <param name="configure"> Optional configuration callback to configure JSON serialization options. Can be null to use default options.</param>
        /// <returns> The service builder being configured. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> is null. </exception>
        public static DomainServiceNodeBuilder UseJsonEventSerializer (this DomainServiceNodeBuilder builder, Action<JsonSerializerOptions> configure = null)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            JsonSerializerOptions options = null;

            if (configure != null)
            {
                options = new JsonSerializerOptions();
                configure(options);
            }

            builder.AddSingleton(typeof(IEventSerializer), new JsonEventSerializer(options));

            return builder;
        }

        /// <summary>
        ///     Registers services for using JSON as integration and domain event serialization format.
        /// </summary>
        /// <param name="builder"> The service builder being configured. </param>
        /// <param name="options"> Optional JSON serialization options. Can be null to use default options. </param>
        /// <returns> The service builder being configured. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> is null. </exception>
        public static DomainServiceNodeBuilder UseJsonEventSerializer (this DomainServiceNodeBuilder builder, JsonSerializerOptions options)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.AddSingleton(typeof(IEventSerializer), new JsonEventSerializer(options));

            return builder;
        }

        #endregion
    }
}
