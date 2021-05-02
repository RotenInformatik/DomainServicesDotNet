using System;

using Microsoft.Extensions.DependencyInjection;

using RI.DomainServices.Common.Builder;




namespace RI.DomainServices.Broker.Builder
{
    /// <summary>
    ///     Provides utility/extension methods for the <see cref="IServiceCollection" /> type.
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    public static class BrokerServiceCollectionExtensions
    {
        #region Static Methods

        /// <summary>
        ///     Creates a new domain service builder for building a domain service broker and uses the given Service Collection as the composition container.
        /// </summary>
        /// <param name="services"> The Service Collection being used </param>
        /// <returns> The domain service builder used to further configure the domain services. </returns>
        /// <remarks>
        ///     <note type="important">
        ///         <see cref="AddBrokerDomainServices" /> does not yet configure or actually add any services to the Service Collection.
        ///         It just prepares the domain service builder for use in further configuration steps.
        ///         Domain services are only added to the Service Collection and usable after <see cref="DomainServiceBuilder.Build" /> is called on the returned <see cref="DomainServiceBrokerBuilder" />.
        ///     </note>
        /// </remarks>
        /// <exception cref="ArgumentNullException"> <paramref name="services" /> is null. </exception>
        public static DomainServiceBrokerBuilder AddBrokerDomainServices (this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            DomainServiceBrokerBuilder builder = new DomainServiceBrokerBuilder();
            builder.UseServiceCollectionCompositionContainer(services);
            return builder;
        }

        #endregion
    }
}
