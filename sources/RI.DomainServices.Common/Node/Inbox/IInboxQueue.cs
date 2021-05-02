using System;

using RI.DomainServices.Node.Objects;
using RI.DomainServices.Node.Serialization;
using RI.DomainServices.Node.Transactions;




namespace RI.DomainServices.Node.Inbox
{
    /// <summary>
    /// The integration and domain event inbox queue from which the events are retrieved.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Inbox queue implementations can inherit from <see cref="InboxQueueBase" /> to implement basic boilerplate.
    ///     </para>
    ///     <note type="implement">
    ///         <see cref="IDisposable.Dispose" /> and <see cref="IAsyncDisposable.DisposeAsync" /> should call <see cref="ISupportTransaction.Rollback" />.
    ///     </note>
    /// </remarks>
    public interface IInboxQueue : ISupportTransaction, IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// Gets the level of order preservation this inbox queue can guarantee.
        /// </summary>
        /// <value>
        /// The level of order preservation this inbox queue can guarantee.
        /// </value>
        /// <remarks>
        ///<para>
        ///See <see cref="OrderPreservation"/> for more details.
        /// </para>
        /// </remarks>
        OrderPreservation OrderPreservation { get; }

        /// <summary>
        /// Gets the level of duplicate avoidance this inbox queue can guarantee.
        /// </summary>
        /// <value>
        /// The level of duplicate avoidance this inbox queue can guarantee.
        /// </value>
        /// <remarks>
        ///<para>
        ///See <see cref="DuplicateAvoidance"/> for more details.
        /// </para>
        /// </remarks>
        DuplicateAvoidance DuplicateAvoidance { get; }

        /// <summary>
        /// Gets the integration or domain event processed by the current transaction.
        /// </summary>
        /// <value>
        /// The event processed by the current transaction or null if no event is available.
        /// </value>
        IEvent CurrentEvent { get; }

        /// <summary>
        /// Gets the serialized integration or domain event processed by the current transaction.
        /// </summary>
        /// <value>
        /// The serialized event processed by the current transaction or null if no event is available.
        /// </value>
        /// <remarks>
        ///     <note type="important">
        ///         Do not use <see cref="CurrentEventSerialized"/> for processing the event. <see cref="CurrentEventSerialized"/> is only intended to be used for logging.
        ///     </note>
        ///     <note type="security">
        ///         When logging <see cref="CurrentEventSerialized"/>, be aware that it might contain sensitive information.
        ///     </note>
        /// </remarks>
        SerializedEvent CurrentEventSerialized { get; }
    }
}
