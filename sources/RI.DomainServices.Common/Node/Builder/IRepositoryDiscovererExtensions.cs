using System;
using System.Collections.Generic;
using System.Linq;

using RI.DomainServices.Node.Repositories;
using RI.Utilities.Reflection;




namespace RI.DomainServices.Node.Builder
{
    /// <summary>
    ///     Provides utility/extension methods for the <see cref="IRepositoryDiscoverer" /> type.
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    public static class IRepositoryDiscovererExtensions
    {
        /// <summary>
        ///     Gets the repository contract type of a repository implementation.
        /// </summary>
        /// <param name="discoverer">The used repository discoverer.</param>
        /// <param name="type"> The type whose repository contract is to be retrieved. </param>
        /// <returns> The list of all repository contract types <paramref name="type"/> implements. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="discoverer" /> or <paramref name="type" /> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="type" /> is not a valid repository implementation type. </exception>
        public static List<Type> GetRepositoryContracts(this IRepositoryDiscoverer discoverer, Type type)
        {
            if (discoverer == null)
            {
                throw new ArgumentNullException(nameof(discoverer));
            }

            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (discoverer.IsValidRepositoryImplementation(type))
            {
                throw new ArgumentException("The type is not a valid repository implementation type.", nameof(type));
            }

            Type[] interfaces = type.GetInterfaces();

            IEnumerable<Type> candidates = interfaces.Where(x => !x.IsGenericType || (x.IsGenericType && (x.GetGenericTypeDefinition() != typeof(IRepository<>)) && (x.GetGenericTypeDefinition() != typeof(IRepository<,>))));

            IEnumerable<Type> contracts = candidates.Where(x => x.IsAssignableToGenericType(typeof(IRepository<>)));

            return new List<Type>(contracts);
        }

        /// <summary>
        ///     Checks whether a given type is a valid repository implementation type.
        /// </summary>
        /// <param name="discoverer">The used repository discoverer.</param>
        /// <param name="type"> The type to check. </param>
        /// <returns> true if <paramref name="type" /> is a valid repository implementation type, false otherwise. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="discoverer" /> or <paramref name="type" /> is null. </exception>
        public static bool IsValidRepositoryImplementation(this IRepositoryDiscoverer discoverer, Type type)
        {
            if (discoverer == null)
            {
                throw new ArgumentNullException(nameof(discoverer));
            }

            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return !type.IsAbstract && type.IsClass && type
                                                       .GetInterfaces()
                                                       .Where(x => x.IsGenericType)
                                                       .Any(x => x.GetGenericTypeDefinition() == typeof(IRepository<,>));
        }
    }
}
