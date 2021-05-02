using System;
using System.Collections.Generic;
using System.Linq;

using RI.DomainServices.Node.Repositories;
using RI.Utilities.Text;




namespace RI.DomainServices.Node.Builder
{
    /// <summary>
    ///     Result of a single repository discovery.
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    public sealed class RepositoryDiscovery
    {
        #region Instance Constructor/Destructor

        /// <summary>
        ///     Creates a new instance of <see cref="RepositoryDiscovery" />.
        /// </summary>
        /// <param name="contracts"> The repository contracts. </param>
        /// <param name="implementation"> The repository implementation. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="contracts" /> or <paramref name="implementation" /> is null. </exception>
        public RepositoryDiscovery (IEnumerable<Type> contracts, Type implementation)
        {
            if (contracts == null)
            {
                throw new ArgumentNullException(nameof(contracts));
            }

            if (implementation == null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            this.Contracts = new List<Type>(contracts);
            this.Implementation = implementation;
        }

        #endregion




        #region Instance Properties/Indexer

        /// <summary>
        ///     Gets the repository contracts (interfaces derived from <see cref="IRepository{TRoot}" />) implemented by <see cref="Implementation" />.
        /// </summary>
        /// <value>
        ///     The repository contracts (interfaces derived from <see cref="IRepository{TRoot}" />) implemented by <see cref="Implementation" />.
        /// </value>
        public IReadOnlyList<Type> Contracts { get; }

        /// <summary>
        ///     Gets the repository implementation (class implementing <see cref="IRepository{TRoot,TUnitOfWork}" /> and the interfaces derived from <see cref="IRepository{TRoot}" />).
        /// </summary>
        /// <value>
        ///     The repository implementation (class implementing <see cref="IRepository{TRoot,TUnitOfWork}" /> and the interfaces derived from <see cref="IRepository{TRoot}" />).
        /// </value>
        public Type Implementation { get; }

        #endregion




        #region Overrides

        /// <inheritdoc />
        public override string ToString () => $"{nameof(RepositoryDiscovery)}; Contracts={this.Contracts.Select(x => x.Name).Join(',')}; Implementation={this.Implementation.Name}";

        #endregion
    }
}
