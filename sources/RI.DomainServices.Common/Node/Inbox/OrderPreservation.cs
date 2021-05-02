using System;




namespace RI.DomainServices.Node.Inbox
{
    /// <summary>
    /// Indicates the level of order preservation an <see cref="IInboxQueue"/> implementation can guarantee.
    /// </summary>
    /// <remarks>
    ///<note type="important">
    /// Avoidance of duplicate message processing is not defined by the inbox queues ability to preserve order.
    /// It is defined separately using <see cref="DuplicateAvoidance"/>.
    /// </note>
    ///<note type="important">
    /// Even if order of message processing is guaranteed, multiple messages can still be processed in parallel.
    /// Order preservation only defines whether the message processing is STARTED in the order as received.
    /// </note>
    /// </remarks>
    [Serializable]
    public enum OrderPreservation
    {
        /// <summary>
        /// Order of message processing is not guaranteed by the inbox queue.
        /// </summary>
        None = 0,

        /// <summary>
        /// Order of message processing is not guaranteed by the inbox queue. However, out-of-order processing is minimal as the inbox queue attempts (means: no 100% guarantee) to deliver the messages in the order as they arrived.
        /// </summary>
        BestEffort = 1,

        /// <summary>
        /// Order of message processing is guaranteed as long as the inbox queue is not terminated unexpectedly or ungracefully (e.g. process crashes). After an unexpected termination, the order of message processing can no longer be guaranteed for any message received before the termination.
        /// </summary>
        AlwaysExceptCrash = 2,

        /// <summary>
        /// Order of message processing is guaranteed as long as the inbox queue is not terminated unexpectedly or ungracefully (e.g. process crashes). After an unexpected termination, the order of message processing can no longer be guaranteed for the messages which were in processing at the time of termination.
        /// </summary>
        AlwaysExceptRestart = 3,

        /// <summary>
        /// Order of message processing is guaranteed by the inbox queue. The messages are always delivered in the order they arrived.
        /// </summary>
        Inherent = 4,
    }
}
