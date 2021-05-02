using System;

using RI.Utilities.Reflection;




namespace RI.DomainServices.Common.Composition
{
    /// <summary>
    ///     A single domain service registration used by domain service builders to configure and build the required types.
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    public sealed class CompositionRegistration
    {
        #region Static Methods

        /// <summary>
        ///     Creates a build-only domain service registration specifying an implementation type.
        /// </summary>
        /// <param name="contract"> The contract type. </param>
        /// <param name="implementation"> The implementation type. </param>
        /// <returns> The created service registration. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="contract" /> or <paramref name="implementation" /> is null. </exception>
        public static CompositionRegistration BuildOnly (Type contract, Type implementation)
        {
            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract));
            }

            if (implementation == null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            return new CompositionRegistration
            {
                Contract = contract,
                Implementation = implementation,
                Factory = null,
                Instance = null,
                Mode = CompositionRegistrationMode.BuildOnly,
            };
        }

        /// <summary>
        ///     Creates a build-only domain service registration specifying a factory.
        /// </summary>
        /// <param name="contract"> The contract type. </param>
        /// <param name="factory"> The factory. </param>
        /// <returns> The created service registration. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="contract" /> or <paramref name="factory" /> is null. </exception>
        public static CompositionRegistration BuildOnly (Type contract, Func<IServiceProvider, object> factory)
        {
            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return new CompositionRegistration
            {
                Contract = contract,
                Implementation = null,
                Factory = factory,
                Instance = null,
                Mode = CompositionRegistrationMode.BuildOnly,
            };
        }

        /// <summary>
        ///     Creates a build-only domain service registration specifying an implementation instance.
        /// </summary>
        /// <param name="contract"> The contract type. </param>
        /// <param name="instance"> The implementation instance. </param>
        /// <returns> The created service registration. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="contract" /> or <paramref name="instance" /> is null. </exception>
        public static CompositionRegistration BuildOnly (Type contract, object instance)
        {
            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract));
            }

            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return new CompositionRegistration
            {
                Contract = contract,
                Implementation = null,
                Factory = null,
                Instance = instance,
                Mode = CompositionRegistrationMode.BuildOnly,
            };
        }

        /// <summary>
        ///     Creates a singleton domain service registration specifying an implementation type.
        /// </summary>
        /// <param name="contract"> The contract type. </param>
        /// <param name="implementation"> The implementation type. </param>
        /// <returns> The created service registration. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="contract" /> or <paramref name="implementation" /> is null. </exception>
        public static CompositionRegistration Singleton (Type contract, Type implementation)
        {
            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract));
            }

            if (implementation == null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            return new CompositionRegistration
            {
                Contract = contract,
                Implementation = implementation,
                Factory = null,
                Instance = null,
                Mode = CompositionRegistrationMode.Singleton,
            };
        }

        /// <summary>
        ///     Creates a singleton domain service registration specifying a factory.
        /// </summary>
        /// <param name="contract"> The contract type. </param>
        /// <param name="factory"> The factory. </param>
        /// <returns> The created service registration. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="contract" /> or <paramref name="factory" /> is null. </exception>
        public static CompositionRegistration Singleton (Type contract, Func<IServiceProvider, object> factory)
        {
            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return new CompositionRegistration
            {
                Contract = contract,
                Implementation = null,
                Factory = factory,
                Instance = null,
                Mode = CompositionRegistrationMode.Singleton,
            };
        }

        /// <summary>
        ///     Creates a singleton domain service registration specifying an implementation instance.
        /// </summary>
        /// <param name="contract"> The contract type. </param>
        /// <param name="instance"> The implementation instance. </param>
        /// <returns> The created service registration. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="contract" /> or <paramref name="instance" /> is null. </exception>
        public static CompositionRegistration Singleton (Type contract, object instance)
        {
            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract));
            }

            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return new CompositionRegistration
            {
                Contract = contract,
                Implementation = null,
                Factory = null,
                Instance = instance,
                Mode = CompositionRegistrationMode.Singleton,
            };
        }

        /// <summary>
        ///     Creates a transient domain service registration specifying an implementation type.
        /// </summary>
        /// <param name="contract"> The contract type. </param>
        /// <param name="implementation"> The implementation type. </param>
        /// <returns> The created service registration. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="contract" /> or <paramref name="implementation" /> is null. </exception>
        public static CompositionRegistration Transient (Type contract, Type implementation)
        {
            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract));
            }

            if (implementation == null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            return new CompositionRegistration
            {
                Contract = contract,
                Implementation = implementation,
                Factory = null,
                Instance = null,
                Mode = CompositionRegistrationMode.Transient,
            };
        }

        /// <summary>
        ///     Creates a transient domain service registration specifying a factory.
        /// </summary>
        /// <param name="contract"> The contract type. </param>
        /// <param name="factory"> The factory. </param>
        /// <returns> The created service registration. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="contract" /> or <paramref name="factory" /> is null. </exception>
        public static CompositionRegistration Transient (Type contract, Func<IServiceProvider, object> factory)
        {
            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return new CompositionRegistration
            {
                Contract = contract,
                Implementation = null,
                Factory = factory,
                Instance = null,
                Mode = CompositionRegistrationMode.Transient,
            };
        }

        #endregion




        #region Instance Properties/Indexer

        /// <summary>
        ///     Gets the contract type.
        /// </summary>
        /// <value>
        ///     The contract type.
        /// </value>
        public Type Contract { get; private set; }

        /// <summary>
        ///     Gets the factory.
        /// </summary>
        /// <value>
        ///     The factory or null if implementation type or implementation instance is used.
        /// </value>
        public Func<IServiceProvider, object> Factory { get; internal set; }

        /// <summary>
        ///     Gets the implementation type.
        /// </summary>
        /// <value>
        ///     The implementation type or null if factory or implementation instance is used.
        /// </value>
        public Type Implementation { get; internal set; }

        /// <summary>
        ///     Gets the implementation type.
        /// </summary>
        /// <value>
        ///     The implementation type or null if implementation type or factory is used.
        /// </value>
        public object Instance { get; internal set; }

        /// <summary>
        ///     Gets the service registration mode.
        /// </summary>
        /// <value>
        ///     The service registration mode.
        /// </value>
        public CompositionRegistrationMode Mode { get; internal set; }

        #endregion





        /// <summary>
        ///     Attempts to get or create the instance registered by this domain service registration.
        /// </summary>
        /// <param name="serviceProviderForFactories"> An optional <see cref="IServiceProvider" /> which is forwarded to potential factory methods if used.. </param>
        /// <returns> The instance. </returns>
        /// <remarks>
        ///     <note type="important">
        ///         The instance creation is limited as it only support basic construction of instances, either by using a registered instance, factory method, or parameterless constructor.
        ///         Constructors of services which have parameters are not supported.
        ///     </note>
        /// </remarks>
        /// <exception cref="NotSupportedException"> The registration does not support instance creation (e.g. does not have a parameterless constructor, the creation threw an exception, etc.) </exception>
        public object GetOrCreateInstance(IServiceProvider serviceProviderForFactories = null)
        {
            try
            {
                object instance;

                if ((this.Instance != null) && (this.Mode != CompositionRegistrationMode.Transient))
                {
                    instance = this.Instance;
                }
                else if ((this.Factory != null) && (this.Mode == CompositionRegistrationMode.BuildOnly))
                {
                    instance = this.Factory(serviceProviderForFactories);
                }
                else if ((this.Implementation != null) && (this.Mode == CompositionRegistrationMode.BuildOnly))
                {
                    instance = Activator.CreateInstance(this.Implementation);
                }
                else
                {
                    throw new NotSupportedException($"Registration not supported (invalid registration): {this}");
                }

                this.Instance = instance;
                this.Factory = null;
                this.Implementation = null;

                return instance;
            }
            catch (NotSupportedException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new NotSupportedException($"Registration not supported (creation failed): {this}", exception);
            }
        }




        #region Overrides

        /// <inheritdoc />
        public override string ToString () => $"{nameof(CompositionRegistration)}; Mode={this.Mode}; Contract={this.Contract?.Name ?? "[null]"}; Implementation={this.Implementation?.Name ?? "[null]"}; Factory={this.Factory?.GetFullName() ?? "[null]"}; Instance={this.Instance ?? "[null]"}";

        #endregion
    }
}
