using System;

using Microsoft.Extensions.DependencyInjection;

using RI.DomainServices.Common.Composition;




namespace RI.DomainServices.Common.Builder
{
    /// <summary>
    ///     Provides utility/extension methods for the <see cref="DomainServiceBuilder" /> type.
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    public static class ServiceCollectionDomainServiceBuilderExtensions
    {
        #region Static Methods

        /// <summary>
        ///     Registers services for using a Service Collection as composition container.
        /// </summary>
        /// <param name="builder"> The service builder being configured. </param>
        /// <param name="services"> The Service Collection to use. </param>
        /// <returns> The service builder being configured. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> or <paramref name="services" /> is null. </exception>
        public static DomainServiceBuilder UseServiceCollectionCompositionContainer (this DomainServiceBuilder builder, IServiceCollection services)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            builder.AddBuildOnly(typeof(ICompositionContainer), new ServiceCollectionCompositionContainer(services));

            return builder;
        }

        #endregion
    }
}
