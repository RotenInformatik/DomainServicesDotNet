using System;
using System.Collections.Generic;




namespace RI.DomainServices.Node.Builder
{
    /// <summary>
    ///     Domain repository discoverer which uses a sequence of types.
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    public sealed class TypeRepositoryDiscoverer : IRepositoryDiscoverer
    {
        #region Instance Constructor/Destructor

        /// <summary>
        ///     Creates a new instance of <see cref="TypeRepositoryDiscoverer" />.
        /// </summary>
        /// <param name="type"> The repository type to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="type" /> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="type" /> is not a repository type. </exception>
        public TypeRepositoryDiscoverer (Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!this.IsValidRepositoryImplementation(type))
            {
                throw new ArgumentException("Invalid repository type: " + type.FullName, nameof(type));
            }

            this.Types.Add(type);
        }

        /// <summary>
        ///     Creates a new instance of <see cref="TypeRepositoryDiscoverer" />.
        /// </summary>
        /// <param name="types"> The repository types to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="types" /> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="types" /> is empty or contains null or <paramref name="types" /> contains a type which is not a repository type. </exception>
        public TypeRepositoryDiscoverer (params Type[] types)
        {
            if (types == null)
            {
                throw new ArgumentNullException(nameof(types));
            }

            this.Types.AddRange(types);

            if (this.Types.Count == 0)
            {
                throw new ArgumentException("Array contains no elements.", nameof(types));
            }

            if (this.Types.Contains(null))
            {
                throw new ArgumentException("Array contains null.", nameof(types));
            }

            foreach (Type type in this.Types)
            {
                if (!this.IsValidRepositoryImplementation(type))
                {
                    throw new ArgumentException("Invalid repository type: " + type.FullName, nameof(type));
                }
            }
        }

        /// <summary>
        ///     Creates a new instance of <see cref="TypeRepositoryDiscoverer" />.
        /// </summary>
        /// <param name="types"> The repository types to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="types" /> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="types" /> is empty or contains null or <paramref name="types" /> contains a type which is not a repository type. </exception>
        public TypeRepositoryDiscoverer (IEnumerable<Type> types)
        {
            if (types == null)
            {
                throw new ArgumentNullException(nameof(types));
            }

            this.Types.AddRange(types);

            if (this.Types.Count == 0)
            {
                throw new ArgumentException("Sequence contains no elements.", nameof(types));
            }

            if (this.Types.Contains(null))
            {
                throw new ArgumentException("Sequence contains null.", nameof(types));
            }

            foreach (Type type in this.Types)
            {
                if (!this.IsValidRepositoryImplementation(type))
                {
                    throw new ArgumentException("Invalid repository type: " + type.FullName, nameof(type));
                }
            }
        }

        #endregion




        #region Instance Properties/Indexer

        private List<RepositoryDiscovery> Discoveries { get; set; }

        private List<Type> Types { get; } = new List<Type>();

        #endregion




        #region Overrides

        /// <inheritdoc />
        public IList<RepositoryDiscovery> DiscoverRepositoryTypes ()
        {
            if (this.Discoveries == null)
            {
                this.Discoveries = new List<RepositoryDiscovery>();

                foreach (Type type in this.Types)
                {
                    List<Type> contracts = this.GetRepositoryContracts(type);
                    this.Discoveries.Add(new RepositoryDiscovery(contracts, type));
                }
            }

            return this.Discoveries;
        }

        #endregion
    }
}
