using System;




namespace RI.DomainServices.Common.Composition
{
    /// <summary>
    ///     Indicates the service registration mode of a <see cref="CompositionRegistration" />.
    /// </summary>
    [Serializable,]
    public enum CompositionRegistrationMode
    {
        /// <summary>
        ///     The service is registered as a singleton (one instance shared with all requests).
        /// </summary>
        Singleton = 0,

        /// <summary>
        ///     The service is registered as a transient (one unique instance per requests).
        /// </summary>
        Transient = 1,

        /// <summary>
        ///     The service is registered for exclusive use by the domain service builder itself and wont be registered in the used composition container.
        /// </summary>
        BuildOnly = 2,
    }
}
