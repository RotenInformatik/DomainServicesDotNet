using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;




namespace RI.DomainServices.Common.Composition
{
    /// <summary>
    ///     Service Collection composition container.
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    public sealed class ServiceCollectionCompositionContainer : ICompositionContainer
    {
        #region Instance Constructor/Destructor

        /// <summary>
        ///     Creates a new instance of <see cref="ServiceCollectionCompositionContainer" />.
        /// </summary>
        /// <param name="services"> The Service Collection to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="services" /> is null. </exception>
        public ServiceCollectionCompositionContainer (IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            this.Services = services;
        }

        #endregion




        #region Instance Properties/Indexer

        /// <summary>
        ///     Gets the Service Collection to use.
        /// </summary>
        /// <value>
        /// The Service Collection to use.
        /// </value>
        public IServiceCollection Services { get; }

        #endregion




        #region Interface: ICompositionContainer

        /// <inheritdoc />
        public void Register (IEnumerable<CompositionRegistration> registrations)
        {
            if (registrations == null)
            {
                throw new ArgumentNullException(nameof(registrations));
            }

            foreach (CompositionRegistration registration in registrations)
            {
                switch (registration)
                {
                    case { } r when (r.Mode == CompositionRegistrationMode.Transient) && (r.Implementation != null):
                        this.Services.AddTransient(r.Contract, r.Implementation);
                        break;

                    case { } r when (r.Mode == CompositionRegistrationMode.Transient) && (r.Factory != null):
                        this.Services.AddTransient(r.Contract, r.Factory);
                        break;

                    case { } r when (r.Mode == CompositionRegistrationMode.Singleton) && (r.Implementation != null):
                        this.Services.AddSingleton(r.Contract, r.Implementation);
                        break;

                    case { } r when (r.Mode == CompositionRegistrationMode.Singleton) && (r.Factory != null):
                        this.Services.AddSingleton(r.Contract, r.Factory);
                        break;

                    case { } r when (r.Mode == CompositionRegistrationMode.Singleton) && (r.Instance != null):
                        this.Services.AddSingleton(r.Contract, r.Instance);
                        break;

                    case { } r when r.Mode == CompositionRegistrationMode.BuildOnly:
                        break;

                    default:
                        throw new NotSupportedException($"{nameof(ServiceCollectionCompositionContainer)} does not support registration: {registration}");
                }
            }
        }

        #endregion
    }
}
