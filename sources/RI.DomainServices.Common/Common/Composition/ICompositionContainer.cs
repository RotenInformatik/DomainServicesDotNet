using System;
using System.Collections.Generic;

using RI.DomainServices.Common.Builder;




namespace RI.DomainServices.Common.Composition
{
    /// <summary>
    ///     Composition container used for registering and subsequently providing domain service types/instances.
    /// </summary>
    /// <remarks>
    ///     <note type="note">
    ///         <see cref="ICompositionContainer" /> is intended to be used by the domain service builder (<see cref="DomainServiceBuilder" />) exclusively and should therefore be registered using <see cref="CompositionRegistrationMode.BuildOnly" />.
    ///     </note>
    /// </remarks>
    public interface ICompositionContainer
    {
        /// <summary>
        ///     Registers the configured domain service registrations in this composition container.
        /// </summary>
        /// <param name="registrations"> The configured domain service registrations. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="registrations" /> is null. </exception>
        /// <exception cref="NotSupportedException"> <paramref name="registrations" /> contains unsupported registrations not supported by this composition container. </exception>
        void Register (IEnumerable<CompositionRegistration> registrations);
    }
}
