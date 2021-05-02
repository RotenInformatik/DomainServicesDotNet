using System;
using System.Collections.Generic;
using System.Linq;

using RI.DomainServices.Common.Composition;




namespace RI.DomainServices.Common.Builder
{
    /// <summary>
    ///     Provides utility/extension methods for the <see cref="DomainServiceBuilder" /> type.
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    public static class DomainServiceBuilderExtensions
    {
        #region Static Methods

        /// <summary>
        ///     Adds a build-only domain service registration specifying an implementation type.
        /// </summary>
        /// <param name="builder"> The service builder being used. </param>
        /// <param name="contract"> The contract type. </param>
        /// <param name="implementation"> The implementation type. </param>
        /// <returns> The added service registration. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" />, <paramref name="contract" />, or <paramref name="implementation" /> is null. </exception>
        /// <exception cref="InvalidOperationException"> <paramref name="builder" /> has already been built and subsequent registrations cannot be used. </exception>
        public static CompositionRegistration AddBuildOnly (this DomainServiceBuilder builder, Type contract, Type implementation)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract));
            }

            if (implementation == null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            builder.ThrowIfAlreadyBuilt();

            CompositionRegistration registration = CompositionRegistration.BuildOnly(contract, implementation);
            builder.Registrations.Add(registration);
            return registration;
        }

        /// <summary>
        ///     Adds a build-only domain service registration specifying a factory.
        /// </summary>
        /// <param name="builder"> The service builder being used. </param>
        /// <param name="contract"> The contract type. </param>
        /// <param name="factory"> The factory. </param>
        /// <returns> The added service registration. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" />, <paramref name="contract" />, or <paramref name="factory" /> is null. </exception>
        /// <exception cref="InvalidOperationException"> <paramref name="builder" /> has already been built and subsequent registrations cannot be used. </exception>
        public static CompositionRegistration AddBuildOnly (this DomainServiceBuilder builder, Type contract, Func<IServiceProvider, object> factory)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            builder.ThrowIfAlreadyBuilt();

            CompositionRegistration registration = CompositionRegistration.BuildOnly(contract, factory);
            builder.Registrations.Add(registration);
            return registration;
        }

        /// <summary>
        ///     Adds a build-only domain service registration specifying an implementation instance.
        /// </summary>
        /// <param name="builder"> The service builder being used. </param>
        /// <param name="contract"> The contract type. </param>
        /// <param name="instance"> The implementation instance. </param>
        /// <returns> The added service registration. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" />, <paramref name="contract" />, or <paramref name="instance" /> is null. </exception>
        /// <exception cref="InvalidOperationException"> <paramref name="builder" /> has already been built and subsequent registrations cannot be used. </exception>
        public static CompositionRegistration AddBuildOnly (this DomainServiceBuilder builder, Type contract, object instance)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract));
            }

            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            builder.ThrowIfAlreadyBuilt();

            CompositionRegistration registration = CompositionRegistration.BuildOnly(contract, instance);
            builder.Registrations.Add(registration);
            return registration;
        }

        /// <summary>
        ///     Adds a singleton domain service registration specifying an implementation type.
        /// </summary>
        /// <param name="builder"> The service builder being used. </param>
        /// <param name="contract"> The contract type. </param>
        /// <param name="implementation"> The implementation type. </param>
        /// <returns> The added service registration. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" />, <paramref name="contract" />, or <paramref name="implementation" /> is null. </exception>
        /// <exception cref="InvalidOperationException"> <paramref name="builder" /> has already been built and subsequent registrations cannot be used. </exception>
        public static CompositionRegistration AddSingleton (this DomainServiceBuilder builder, Type contract, Type implementation)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract));
            }

            if (implementation == null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            builder.ThrowIfAlreadyBuilt();

            CompositionRegistration registration = CompositionRegistration.Singleton(contract, implementation);
            builder.Registrations.Add(registration);
            return registration;
        }

        /// <summary>
        ///     Adds a singleton domain service registration specifying a factory.
        /// </summary>
        /// <param name="builder"> The service builder being used. </param>
        /// <param name="contract"> The contract type. </param>
        /// <param name="factory"> The factory. </param>
        /// <returns> The added service registration. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" />, <paramref name="contract" />, or <paramref name="factory" /> is null. </exception>
        /// <exception cref="InvalidOperationException"> <paramref name="builder" /> has already been built and subsequent registrations cannot be used. </exception>
        public static CompositionRegistration AddSingleton (this DomainServiceBuilder builder, Type contract, Func<IServiceProvider, object> factory)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            builder.ThrowIfAlreadyBuilt();

            CompositionRegistration registration = CompositionRegistration.Singleton(contract, factory);
            builder.Registrations.Add(registration);
            return registration;
        }

        /// <summary>
        ///     Adds a singleton domain service registration specifying an implementation instance.
        /// </summary>
        /// <param name="builder"> The service builder being used. </param>
        /// <param name="contract"> The contract type. </param>
        /// <param name="instance"> The implementation instance. </param>
        /// <returns> The added service registration. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" />, <paramref name="contract" />, or <paramref name="instance" /> is null. </exception>
        /// <exception cref="InvalidOperationException"> <paramref name="builder" /> has already been built and subsequent registrations cannot be used. </exception>
        public static CompositionRegistration AddSingleton (this DomainServiceBuilder builder, Type contract, object instance)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract));
            }

            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            builder.ThrowIfAlreadyBuilt();

            CompositionRegistration registration = CompositionRegistration.Singleton(contract, instance);
            builder.Registrations.Add(registration);
            return registration;
        }

        /// <summary>
        ///     Adds a transient domain service registration specifying an implementation type.
        /// </summary>
        /// <param name="builder"> The service builder being used. </param>
        /// <param name="contract"> The contract type. </param>
        /// <param name="implementation"> The implementation type. </param>
        /// <returns> The added service registration. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" />, <paramref name="contract" />, or <paramref name="implementation" /> is null. </exception>
        /// <exception cref="InvalidOperationException"> <paramref name="builder" /> has already been built and subsequent registrations cannot be used. </exception>
        public static CompositionRegistration AddTransient (this DomainServiceBuilder builder, Type contract, Type implementation)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract));
            }

            if (implementation == null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            builder.ThrowIfAlreadyBuilt();

            CompositionRegistration registration = CompositionRegistration.Transient(contract, implementation);
            builder.Registrations.Add(registration);
            return registration;
        }

        /// <summary>
        ///     Adds a transient domain service registration specifying a factory.
        /// </summary>
        /// <param name="builder"> The service builder being used. </param>
        /// <param name="contract"> The contract type. </param>
        /// <param name="factory"> The factory. </param>
        /// <returns> The added service registration. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" />, <paramref name="contract" />, or <paramref name="factory" /> is null. </exception>
        /// <exception cref="InvalidOperationException"> <paramref name="builder" /> has already been built and subsequent registrations cannot be used. </exception>
        public static CompositionRegistration AddTransient (this DomainServiceBuilder builder, Type contract, Func<IServiceProvider, object> factory)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            builder.ThrowIfAlreadyBuilt();

            CompositionRegistration registration = CompositionRegistration.Transient(contract, factory);
            builder.Registrations.Add(registration);
            return registration;
        }

        /// <summary>
        ///     Checks whether the specified domain service builder has already <see cref="DomainServiceBuilder.Build" /> called and if so throws a <see cref="InvalidOperationException" />.
        /// </summary>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> is null. </exception>
        /// <exception cref="InvalidOperationException"> <paramref name="builder" /> has already been built. </exception>
        public static void ThrowIfAlreadyBuilt (this DomainServiceBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (builder.AlreadyBuilt)
            {
                throw new InvalidOperationException(builder.GetType()
                                                           .Name + " has already been built.");
            }
        }

        /// <summary>
        ///     Gets a service instance from the domain service registrations of a an <see cref="DomainServiceBuilder" />.
        /// </summary>
        /// <typeparam name="TContract"> The type of the requested service. </typeparam>
        /// <param name="builder"> The service builder. </param>
        /// <returns> The requested service instance or null if no registration for the specified contract was found. </returns>
        /// <remarks>
        ///     <note type="important">
        ///         The service resolution is limited as it only support basic construction of instances, either by using a registered instance, factory method, or parameterless constructor.
        ///         Constructors of services which have parameters are not supported.
        ///     </note>
        /// </remarks>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> is null. </exception>
        /// <exception cref="InvalidOperationException"> <paramref name="builder" /> has already been built and services should no longer be retrieved from the registrations. </exception>
        /// <exception cref="NotSupportedException"> The resolved registration does not support instance creation (e.g. does not have a parameterless constructor, the creation threw an exception, etc.) </exception>
        public static TContract GetService <TContract> (this DomainServiceBuilder builder)
            where TContract : class
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ThrowIfAlreadyBuilt();

            ServiceProviderWrapper serviceProvider = new ServiceProviderWrapper(builder);
            return (TContract)serviceProvider.GetService(typeof(TContract));
        }

        /// <summary>
        ///     Gets a service instance from the domain service registrations of a an <see cref="DomainServiceBuilder" />.
        /// </summary>
        /// <param name="builder"> The service builder. </param>
        /// <param name="contract"> The type of the requested service. </param>
        /// <returns> The requested service instance or null if no registration for the specified contract was found. </returns>
        /// <remarks>
        ///     <note type="important">
        ///         The service resolution is limited as it only support basic construction of instances, either by using a registered instance, factory method, or parameterless constructor.
        ///         Constructors of services which have parameters are not supported.
        ///     </note>
        /// </remarks>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> or <paramref name="contract" /> is null. </exception>
        /// <exception cref="InvalidOperationException"> <paramref name="builder" /> has already been built and services should no longer be retrieved from the registrations. </exception>
        /// <exception cref="NotSupportedException"> The resolved registration does not support instance creation (e.g. does not have a parameterless constructor, the creation threw an exception, etc.) </exception>
        public static object GetService (this DomainServiceBuilder builder, Type contract)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract));
            }

            builder.ThrowIfAlreadyBuilt();

            ServiceProviderWrapper serviceProvider = new ServiceProviderWrapper(builder);
            return serviceProvider.GetService(contract);
        }

        /// <summary>
        ///     Gets a service instance from the domain service registrations of a an <see cref="DomainServiceBuilder" />.
        /// </summary>
        /// <param name="builder"> The service builder. </param>
        /// <param name="registration"> The domain service registration. </param>
        /// <returns> The requested service instance. </returns>
        /// <remarks>
        ///     <note type="important">
        ///         The service resolution is limited as it only support basic construction of instances, either by using a registered instance, factory method, or parameterless constructor.
        ///         Constructors of services which have parameters are not supported.
        ///     </note>
        /// </remarks>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> or <paramref name="registration" /> is null. </exception>
        /// <exception cref="InvalidOperationException"> <paramref name="builder" /> has already been built and services should no longer be retrieved from the registrations. </exception>
        /// <exception cref="NotSupportedException"> The resolved registration does not support instance creation (e.g. does not have a parameterless constructor, the creation threw an exception, etc.) </exception>
        public static object GetService (this DomainServiceBuilder builder, CompositionRegistration registration)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (registration == null)
            {
                throw new ArgumentNullException(nameof(registration));
            }

            builder.ThrowIfAlreadyBuilt();

            ServiceProviderWrapper serviceProvider = new ServiceProviderWrapper(builder);
            return serviceProvider.GetService(registration);
        }

        /// <summary>
        ///     Gets an <see cref="IServiceProvider" /> which can be used to access the domain service registrations of an <see cref="DomainServiceBuilder" />.
        /// </summary>
        /// <param name="builder"> The service builder. </param>
        /// <returns> The <see cref="IServiceProvider" /> (one new instance for each call to <see cref="GetServiceProvider" />). </returns>
        /// <remarks>
        ///     <note type="important">
        ///         The returned <see cref="IServiceProvider" /> is limited as it only supports basic construction of instances, either by using a registered instance, factory method, or parameterless constructor.
        ///         Constructors of services which have parameters are not supported.
        ///     </note>
        /// </remarks>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> is null. </exception>
        /// <exception cref="InvalidOperationException"> <paramref name="builder" /> has already been built and services should no longer be retrieved from the registrations. </exception>
        public static IServiceProvider GetServiceProvider (this DomainServiceBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ThrowIfAlreadyBuilt();

            return new ServiceProviderWrapper(builder);
        }

        /// <summary>
        ///     Gets all service instances from the domain service registrations of a an <see cref="DomainServiceBuilder" />.
        /// </summary>
        /// <typeparam name="TContract"> The type of the requested service. </typeparam>
        /// <param name="builder"> The service builder. </param>
        /// <returns> The list of instances of the requested service or an empty list if no registration for the specified contract was found. </returns>
        /// <remarks>
        ///     <note type="important">
        ///         The service resolution is limited as it only support basic construction of instances, either by using a registered instance, factory method, or parameterless constructor.
        ///         Constructors of services which have parameters are not supported.
        ///     </note>
        /// </remarks>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> is null. </exception>
        /// <exception cref="InvalidOperationException"> <paramref name="builder" /> has already been built and services should no longer be retrieved from the registrations. </exception>
        /// <exception cref="NotSupportedException"> The resolved registration does not support instance creation (e.g. does not have a parameterless constructor, the creation threw an exception, etc.) </exception>
        public static IList<TContract> GetServices <TContract> (this DomainServiceBuilder builder)
            where TContract : class
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ThrowIfAlreadyBuilt();

            ServiceProviderWrapper serviceProvider = new ServiceProviderWrapper(builder);

            return serviceProvider.GetServices(typeof(TContract))
                                  .Cast<TContract>()
                                  .ToList();
        }

        /// <summary>
        ///     Gets all service instances from the domain service registrations of a an <see cref="DomainServiceBuilder" />.
        /// </summary>
        /// <param name="builder"> The service builder. </param>
        /// <param name="contract"> The type of the requested service. </param>
        /// <returns> The list of instances of the requested service or an empty list if no registration for the specified contract was found. </returns>
        /// <remarks>
        ///     <note type="important">
        ///         The service resolution is limited as it only support basic construction of instances, either by using a registered instance, factory method, or parameterless constructor.
        ///         Constructors of services which have parameters are not supported.
        ///     </note>
        /// </remarks>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> or <paramref name="contract" /> is null. </exception>
        /// <exception cref="InvalidOperationException"> <paramref name="builder" /> has already been built and services should no longer be retrieved from the registrations. </exception>
        /// <exception cref="NotSupportedException"> The resolved registration does not support instance creation (e.g. does not have a parameterless constructor, the creation threw an exception, etc.) </exception>
        public static IList<object> GetServices (this DomainServiceBuilder builder, Type contract)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract));
            }

            builder.ThrowIfAlreadyBuilt();

            ServiceProviderWrapper serviceProvider = new ServiceProviderWrapper(builder);
            return serviceProvider.GetServices(contract);
        }

        /// <summary>
        ///     Removes all registrations which are registered as <see cref="CompositionRegistrationMode.BuildOnly" />.
        /// </summary>
        /// <param name="builder"> The service builder being used. </param>
        /// <returns> The number of removed registrations or zero if no build-only registrations were found to be removed. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> is null. </exception>
        /// <exception cref="InvalidOperationException"> <paramref name="builder" /> has already been built and subsequent registrations cannot be used. </exception>
        public static int RemoveBuildOnlyContracts (this DomainServiceBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ThrowIfAlreadyBuilt();

            return builder.Registrations.RemoveAll(x => x.Mode == CompositionRegistrationMode.BuildOnly);
        }

        /// <summary>
        ///     Removes all registrations of a specified contract type from the domain service registrations.
        /// </summary>
        /// <param name="builder"> The service builder being used. </param>
        /// <param name="contract"> The contract type. </param>
        /// <returns> The number of removed registrations or zero if no contracts were found to be removed. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> or <paramref name="contract" /> is null. </exception>
        /// <exception cref="InvalidOperationException"> <paramref name="builder" /> has already been built and subsequent registrations cannot be used. </exception>
        public static int RemoveContract (this DomainServiceBuilder builder, Type contract)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract));
            }

            builder.ThrowIfAlreadyBuilt();

            return builder.Registrations.RemoveAll(x => x.Contract == contract);
        }

        /// <summary>
        /// Counts the number of registrations for a specified contract.
        /// </summary>
        /// <param name="builder"> The service builder being used. </param>
        /// <param name="contract"> The contract type. </param>
        /// <returns>The number of registrations for <paramref name="contract"/>.</returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> or <paramref name="contract" /> is null. </exception>
        public static int CountContracts (this DomainServiceBuilder builder, Type contract)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract));
            }

            return builder.Registrations.Count(x => x.Contract == contract);
        }

        /// <summary>
        /// Checks whether the specified domain service builder has an exact amount of a specified contract registered or throws a <see cref="DomainServiceBuilderException" /> if not.
        /// </summary>
        /// <param name="builder"> The service builder being used. </param>
        /// <param name="contract"> The contract type. </param>
        /// <param name="exactCount">The exact amount of registered contracts required.</param>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> or <paramref name="contract" /> is null. </exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="exactCount"/> is less than zero.</exception>
        /// <exception cref="DomainServiceBuilderException">The builder does not contain the exact amount of contracts.</exception>
        public static void ThrowIfNotExactContractCount (this DomainServiceBuilder builder, Type contract, int exactCount)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract));
            }

            if (exactCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(exactCount));
            }

            int actualCount = builder.CountContracts(contract);

            if (actualCount != exactCount)
            {
                throw new DomainServiceBuilderException($"{builder.GetType().Name} requires exact {exactCount} contracts of type {contract.Name} but has {actualCount}.");
            }
        }

        /// <summary>
        /// Checks whether the specified domain service builder has a minimum amount of a specified contract registered or throws a <see cref="DomainServiceBuilderException" /> if not.
        /// </summary>
        /// <param name="builder"> The service builder being used. </param>
        /// <param name="contract"> The contract type. </param>
        /// <param name="minCount">The minimum amount of registered contracts required.</param>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> or <paramref name="contract" /> is null. </exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minCount"/> is less than zero.</exception>
        /// <exception cref="DomainServiceBuilderException">The builder does not contain the minimum amount of contracts.</exception>
        public static void ThrowIfNotMinContractCount(this DomainServiceBuilder builder, Type contract, int minCount)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract));
            }

            if (minCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minCount));
            }

            int actualCount = builder.CountContracts(contract);

            if (actualCount < minCount)
            {
                throw new DomainServiceBuilderException($"{builder.GetType().Name} requires at least {minCount} contracts of type {contract.Name} but has {actualCount}.");
            }
        }

        /// <summary>
        /// Checks whether the specified domain service builder has a maximum amount of a specified contract registered or throws a <see cref="DomainServiceBuilderException" /> if not.
        /// </summary>
        /// <param name="builder"> The service builder being used. </param>
        /// <param name="contract"> The contract type. </param>
        /// <param name="maxCount">The maximum amount of registered contracts required.</param>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> or <paramref name="contract" /> is null. </exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxCount"/> is less than zero.</exception>
        /// <exception cref="DomainServiceBuilderException">The builder does contain more than the maximum amount of contracts.</exception>
        public static void ThrowIfNotMaxContractCount(this DomainServiceBuilder builder, Type contract, int maxCount)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract));
            }

            if (maxCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxCount));
            }

            int actualCount = builder.CountContracts(contract);

            if (actualCount > maxCount)
            {
                throw new DomainServiceBuilderException($"{builder.GetType().Name} requires at most {maxCount} contracts of type {contract.Name} but has {actualCount}.");
            }
        }

        #endregion




        #region Type: ServiceProviderWrapper

        private sealed class ServiceProviderWrapper : IServiceProvider
        {
            #region Instance Constructor/Destructor

            public ServiceProviderWrapper (DomainServiceBuilder builder)
            {
                if (builder == null)
                {
                    throw new ArgumentNullException(nameof(builder));
                }

                this.Builder = builder;
            }

            #endregion




            #region Instance Properties/Indexer

            private DomainServiceBuilder Builder { get; }

            #endregion




            #region Instance Methods

            public object GetService (CompositionRegistration registration)
            {
                if (registration == null)
                {
                    throw new ArgumentNullException(nameof(registration));
                }

                this.Builder.ThrowIfAlreadyBuilt();

                return registration.GetOrCreateInstance(this);
            }

            public List<object> GetServices (Type serviceType)
            {
                if (serviceType == null)
                {
                    throw new ArgumentNullException(nameof(serviceType));
                }

                this.Builder.ThrowIfAlreadyBuilt();

                List<object> instances = new List<object>();

                foreach (CompositionRegistration registration in this.Builder.Registrations)
                {
                    if (serviceType == registration.Contract)
                    {
                        instances.Add(this.GetService(registration));
                    }
                }

                return instances;
            }

            #endregion




            #region Interface: IServiceProvider

            public object GetService (Type serviceType)
            {
                if (serviceType == null)
                {
                    throw new ArgumentNullException(nameof(serviceType));
                }

                this.Builder.ThrowIfAlreadyBuilt();

                foreach (CompositionRegistration registration in this.Builder.Registrations)
                {
                    if (serviceType == registration.Contract)
                    {
                        return this.GetService(registration);
                    }
                }

                return null;
            }

            #endregion
        }

        #endregion
    }
}
