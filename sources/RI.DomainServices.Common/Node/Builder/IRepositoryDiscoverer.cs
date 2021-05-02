using System.Collections.Generic;

using RI.DomainServices.Common.Composition;
using RI.DomainServices.Node.Repositories;




namespace RI.DomainServices.Node.Builder
{
    /// <summary>
    ///     Domain repository discoverer.
    /// </summary>
    /// <remarks>
    ///     <note type="note">
    ///         <see cref="IRepositoryDiscoverer" /> is intended to be used by the node domain service builder (<see cref="DomainServiceNodeBuilder" />) exclusively and should therefore be registered using <see cref="CompositionRegistrationMode.BuildOnly" />.
    ///     </note>
    /// </remarks>
    public interface IRepositoryDiscoverer
    {
        /// <summary>
        ///     Discovers all repository implementations (<see cref="IRepository{TRoot,TUnitOfWork}" />) this discoverer can find.
        /// </summary>
        /// <returns> A list with all found repository implementations. An empty list is returned if no repository implementations could be found.</returns>
        IList<RepositoryDiscovery> DiscoverRepositoryTypes ();
    }
}
