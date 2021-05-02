using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;

using RI.DomainServices.Node.Objects;
using RI.DomainServices.Node.Transactions;




namespace RI.DomainServices.Node.Repositories
{
    /// <summary>
    ///     Unit-of-work used by repositories.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Unit-of-work implementations can inherit from <see cref="UnitOfWorkBase" /> to implement basic boilerplate.
    ///     </para>
    ///     <note type="implement">
    ///         <see cref="IDisposable.Dispose" /> and <see cref="IAsyncDisposable.DisposeAsync" /> should call <see cref="ISupportTransaction.Rollback" />.
    ///     </note>
    /// </remarks>
    public interface IUnitOfWork : ISupportTransaction, IDisposable, IAsyncDisposable
    {
        /// <summary>
        ///     Gets the list of already published domain and integration events.
        /// </summary>
        /// <value>
        ///     The list of already published domain and integration events.
        /// </value>
        /// <remarks>
        ///     <note type="implement">
        ///         This property must not be null.
        ///     </note>
        /// </remarks>
        IReadOnlyList<IEvent> PublishedEvents { get; }

        /// <summary>
        ///     Publishes a domain event or integration event as part of the current transaction.
        /// </summary>
        /// <param name="event"> The domain event or integration event to publish. </param>
        /// <returns> The task to await until the event was published as part of the current transaction. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="event" /> is null. </exception>
        /// <exception cref="InvalidOperationException"> The transaction is not started or was already committed or rolled back. </exception>
        /// <exception cref="SerializationException"> <paramref name="event" /> cannot be serialized. </exception>
        Task Publish (IEvent @event);
    }
}
