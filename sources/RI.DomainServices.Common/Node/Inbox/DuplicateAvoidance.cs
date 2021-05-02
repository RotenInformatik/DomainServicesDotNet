using System;




namespace RI.DomainServices.Node.Inbox
{
    /// <summary>
    /// Indicates the level of duplicate avoidance an <see cref="IInboxQueue"/> implementation can guarantee.
    /// </summary>
    /// <remarks>
    ///<note type="important">
    /// Preserving the order of message processing is not defined by the inbox queues ability to avoid duplicate message processing.
    /// It is defined separately using <see cref="OrderPreservation"/>.
    /// </note>
    ///<note type="important">
    /// Even if duplicate avoidance is guaranteed, multiple messages can still be processed in parallel.
    /// </note>
    /// </remarks>
    [Serializable]
    public enum DuplicateAvoidance
    {
        /// <summary>
        /// Avoiding duplicate message processing is not guaranteed by the inbox queue. The same event could be processed repeatedly.
        /// </summary>
        None = 0,

        /// <summary>
        /// Avoiding duplicate message processing is not guaranteed by the inbox queue. The same event could be processed repeatedly. However, the chance is minimal as the inbox queue attempts (means: no 100% guarantee) to not deliver the same event twice.
        /// </summary>
        BestEffort = 1,

        /// <summary>
        /// Avoiding duplicate message processing is guaranteed as long as the inbox queue is not terminated unexpectedly or ungracefully (e.g. process crashes). After an unexpected termination, avoiding delivery of a previously delivered message cannot be avoided for any message received before the termination.
        /// </summary>
        AlwaysExceptCrash = 2,

        /// <summary>
        /// Avoiding duplicate message processing is guaranteed as long as the inbox queue is not terminated unexpectedly or ungracefully (e.g. process crashes). After an unexpected termination, avoiding delivery of a previously delivered message cannot be avoided for the messages which were in processing at the time of termination.
        /// </summary>
        AlwaysExceptRestart = 3,

        /// <summary>
        /// Avoiding duplicate message processing is guaranteed by the inbox queue. The same event can not be processed repeatedly.
        /// </summary>
        Inherent = 4,
    }
}
