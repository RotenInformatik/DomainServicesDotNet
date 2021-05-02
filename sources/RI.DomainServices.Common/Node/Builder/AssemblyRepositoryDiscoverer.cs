using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;




namespace RI.DomainServices.Node.Builder
{
    /// <summary>
    ///     Domain repository discoverer which searches for suitable types in assemblies.
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    public sealed class AssemblyRepositoryDiscoverer : IRepositoryDiscoverer
    {
        #region Instance Constructor/Destructor

        /// <summary>
        ///     Creates a new instance of <see cref="AssemblyRepositoryDiscoverer" />.
        /// </summary>
        /// <param name="assembly"> The assembly to search for suitable repository types. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="assembly" /> is null. </exception>
        public AssemblyRepositoryDiscoverer (Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            this.Assemblies.Add(assembly);
        }

        /// <summary>
        ///     Creates a new instance of <see cref="AssemblyRepositoryDiscoverer" />.
        /// </summary>
        /// <param name="assemblies"> The assemblies to search for suitable repository types. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="assemblies" /> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="assemblies" /> is empty or contains null. </exception>
        public AssemblyRepositoryDiscoverer (params Assembly[] assemblies)
        {
            if (assemblies == null)
            {
                throw new ArgumentNullException(nameof(assemblies));
            }

            this.Assemblies.AddRange(assemblies);

            if (this.Assemblies.Count == 0)
            {
                throw new ArgumentException("Array contains no elements.", nameof(assemblies));
            }

            if (this.Assemblies.Contains(null))
            {
                throw new ArgumentException("Array contains null.", nameof(assemblies));
            }
        }

        /// <summary>
        ///     Creates a new instance of <see cref="AssemblyRepositoryDiscoverer" />.
        /// </summary>
        /// <param name="assemblies"> The assemblies to search for suitable repository types. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="assemblies" /> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="assemblies" /> is empty or contains null. </exception>
        public AssemblyRepositoryDiscoverer (IEnumerable<Assembly> assemblies)
        {
            if (assemblies == null)
            {
                throw new ArgumentNullException(nameof(assemblies));
            }

            this.Assemblies.AddRange(assemblies);

            if (this.Assemblies.Count == 0)
            {
                throw new ArgumentException("Sequence contains no elements.", nameof(assemblies));
            }

            if (this.Assemblies.Contains(null))
            {
                throw new ArgumentException("Sequence contains null.", nameof(assemblies));
            }
        }

        #endregion




        #region Instance Properties/Indexer

        private List<Assembly> Assemblies { get; } = new List<Assembly>();

        private List<RepositoryDiscovery> Discoveries { get; set; }

        private List<Type> Types { get; set; }

        #endregion




        #region Overrides

        /// <inheritdoc />
        public IList<RepositoryDiscovery> DiscoverRepositoryTypes ()
        {
            if (this.Types == null)
            {
                this.Types = new List<Type>();

                this.Types.AddRange(this.Assemblies.SelectMany(x => x.GetTypes())
                                        .Where(this.IsValidRepositoryImplementation));
            }

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
