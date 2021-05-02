using System;




namespace RI.DomainServices.Node.Transactions
{
    /// <summary>
    ///     Indicates the transaction state of an <see cref="ISupportTransaction" /> implementation.
    /// </summary>
    [Serializable,]
    public enum TransactionState
    {
        /// <summary>
        ///     Transaction not yet started. Not yet usable.
        /// </summary>
        NotStarted = 0,

        /// <summary>
        ///     Transaction is being started. Not yet usable.
        /// </summary>
        Starting = 1,

        /// <summary>
        ///     Transaction started and ready to be used.
        /// </summary>
        Started = 2,

        /// <summary>
        ///     Transaction being committed. No longer usable.
        /// </summary>
        Committing = 3,

        /// <summary>
        ///     Transaction committed. No longer usable.
        /// </summary>
        Committed = 4,

        /// <summary>
        ///     Transaction being rolled back. No longer usable.
        /// </summary>
        RollingBack = 5,

        /// <summary>
        ///     Transaction rolled back. No longer usable.
        /// </summary>
        RolledBack = 6,
    }
}
