using System;
using System.Collections.Generic;
using System.Reflection;

using RI.DomainServices.Common.Builder;




namespace RI.DomainServices.Node.Builder
{
    /// <summary>
    ///     Provides utility/extension methods for the <see cref="DomainServiceNodeBuilder" /> type.
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    public static class DomainServiceNodeBuilderExtensions
    {
        #region Static Methods

        /// <summary>
        ///     Registers services for using assemblies to discover repository implementations.
        /// </summary>
        /// <param name="builder"> The service builder being configured. </param>
        /// <param name="configure"> Configuration callback to configure the list of assemblies to use. </param>
        /// <returns> The service builder being configured. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> or <paramref name="configure" /> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="configure" /> added no assemblies or null to the list of assemblies to use. </exception>
        public static DomainServiceNodeBuilder UseAssemblyRepositoryDiscoverer(this DomainServiceNodeBuilder builder, Action<List<Assembly>> configure)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            List<Assembly> assemblies = new List<Assembly>();

            configure(assemblies);

            builder.AddBuildOnly(typeof(IRepositoryDiscoverer), new AssemblyRepositoryDiscoverer(assemblies));

            return builder;
        }

        /// <summary>
        ///     Registers services for using assemblies to discover repository implementations.
        /// </summary>
        /// <param name="builder"> The service builder being configured. </param>
        /// <param name="assemblies"> The sequence of assemblies to use. </param>
        /// <returns> The service builder being configured. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> or <paramref name="assemblies" /> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="assemblies" /> is empty or contains null. </exception>
        public static DomainServiceNodeBuilder UseAssemblyRepositoryDiscoverer(this DomainServiceNodeBuilder builder, IEnumerable<Assembly> assemblies)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (assemblies == null)
            {
                throw new ArgumentNullException(nameof(assemblies));
            }

            builder.AddBuildOnly(typeof(IRepositoryDiscoverer), new AssemblyRepositoryDiscoverer(assemblies));

            return builder;
        }

        /// <summary>
        ///     Registers services for using assemblies to discover repository implementations.
        /// </summary>
        /// <param name="builder"> The service builder being configured. </param>
        /// <param name="assemblies"> The sequence of assemblies to use. </param>
        /// <returns> The service builder being configured. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> or <paramref name="assemblies" /> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="assemblies" /> is empty or contains null. </exception>
        public static DomainServiceNodeBuilder UseAssemblyRepositoryDiscoverer(this DomainServiceNodeBuilder builder, params Assembly[] assemblies)
        {
            return builder.UseAssemblyRepositoryDiscoverer((IEnumerable<Assembly>)assemblies);
        }

        /// <summary>
        ///     Registers services for using a list of types to discover repository implementations.
        /// </summary>
        /// <param name="builder"> The service builder being configured. </param>
        /// <param name="configure"> Configuration callback to configure the list of types to use. </param>
        /// <returns> The service builder being configured. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> or <paramref name="configure" /> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="configure" /> added no types, null, or a type which is not a repository type to the list of types to use. </exception>
        public static DomainServiceNodeBuilder UseTypeRepositoryDiscoverer(this DomainServiceNodeBuilder builder, Action<List<Type>> configure)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            List<Type> types = new List<Type>();

            configure(types);

            builder.AddBuildOnly(typeof(IRepositoryDiscoverer), new TypeRepositoryDiscoverer(types));

            return builder;
        }

        /// <summary>
        ///     Registers services for using a list of types to discover repository implementations.
        /// </summary>
        /// <param name="builder"> The service builder being configured. </param>
        /// <param name="types"> The sequence of types to use. </param>
        /// <returns> The service builder being configured. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> or <paramref name="types" /> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="types" /> is empty or contains null or <paramref name="types" /> contains a type which is not a repository type. </exception>
        public static DomainServiceNodeBuilder UseTypeRepositoryDiscoverer(this DomainServiceNodeBuilder builder, IEnumerable<Type> types)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (types == null)
            {
                throw new ArgumentNullException(nameof(types));
            }

            builder.AddBuildOnly(typeof(IRepositoryDiscoverer), new TypeRepositoryDiscoverer(types));

            return builder;
        }

        /// <summary>
        ///     Registers services for using a list of types to discover repository implementations.
        /// </summary>
        /// <param name="builder"> The service builder being configured. </param>
        /// <param name="types"> The sequence of types to use. </param>
        /// <returns> The service builder being configured. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> or <paramref name="types" /> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="types" /> is empty or contains null or <paramref name="types" /> contains a type which is not a repository type. </exception>
        public static DomainServiceNodeBuilder UseTypeRepositoryDiscoverer(this DomainServiceNodeBuilder builder, params Type[] types)
        {
            return builder.UseTypeRepositoryDiscoverer((IEnumerable<Type>)types);
        }

        #endregion
    }
}
