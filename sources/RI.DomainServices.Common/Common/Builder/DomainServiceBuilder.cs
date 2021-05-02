using System;
using System.Collections.Generic;

using RI.DomainServices.Common.Composition;




namespace RI.DomainServices.Common.Builder
{
    /// <summary>
    ///     Domain service builder used to configure and register all necessary types for using domain services.
    /// </summary>
    /// <remarks>
    ///     <note type="important">
    ///         Domain services are only added to the used composition container and usable after <see cref="Build" /> is called.
    ///     </note>
    /// </remarks>
    /// <threadsafety static="false" instance="false" />
    public abstract class DomainServiceBuilder
    {
        private readonly List<CompositionRegistration> _registrations = new List<CompositionRegistration>();

        /// <summary>
        ///     Gets whether <see cref="Build" /> has already been called.
        /// </summary>
        /// <value>
        /// true if <see cref="Build"/> was called, false otherwise.
        /// </value>
        public bool AlreadyBuilt { get; protected set; } = false;

        /// <summary>
        ///     Gets the list of all the domain service registrations.
        /// </summary>
        /// <value>
        /// The list of all the domain service registrations.
        /// </value>
        /// <remarks>
        ///     <note type="important">
        ///         Domain services are only added to the used composition container and usable after <see cref="Build" /> is called.
        ///     </note>
        /// </remarks>
        /// <exception cref="InvalidOperationException"> <see cref="Build" /> has already been called. </exception>
        public List<CompositionRegistration> Registrations
        {
            get
            {
                this.ThrowIfAlreadyBuilt();
                return this._registrations;
            }
        }

        /// <summary>
        ///     Finishes the domain service configuration, registers all necessary types in the used composition container, and makes the domain services usable.
        /// </summary>
        /// <remarks>
        ///     <note type="important">
        ///         Domain services are only added to the used composition container and usable after <see cref="Build" /> is called.
        ///     </note>
        /// </remarks>
        /// <exception cref="InvalidOperationException"> <see cref="Build" /> has already been called. </exception>
        /// <exception cref="DomainServiceBuilderException"> Necessary types could not be successfully configured or registered. </exception>
        public abstract void Build ();
    }
}
