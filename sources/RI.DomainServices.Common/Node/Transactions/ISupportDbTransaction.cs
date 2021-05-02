using System.Data.Common;




namespace RI.DomainServices.Node.Transactions
{
    /// <summary>
    ///     Provides support of <see cref="DbTransaction" /> by transaction implementations.
    /// </summary>
    public interface ISupportDbTransaction : ISupportTransaction
    {
        /// <summary>
        ///     Gets the <see cref="DbTransaction" /> used by the transaction implementation.
        /// </summary>
        /// <value>
        ///     The <see cref="DbTransaction" /> used by the transaction implementation.
        /// </value>
        /// <remarks>
        ///     <note type="implement">
        ///         This property must not be null when the transaction is in the <see cref="TransactionState.Started"/> state.
        ///     </note>
        /// </remarks>
        DbTransaction Transaction { get; }
    }

    /// <summary>
    ///     Provides support of <see cref="DbTransaction" /> by transaction implementations.
    /// </summary>
    /// <typeparam name="TTransaction"> The concrete <see cref="DbTransaction" /> implementation. </typeparam>
    public interface ISupportDbTransaction <out TTransaction> : ISupportDbTransaction
        where TTransaction : DbTransaction
    {
        /// <summary>
        ///     Gets the <typeparamref name="TTransaction"/> used by the transaction implementation.
        /// </summary>
        /// <value>
        ///     The <typeparamref name="TTransaction"/> used by the transaction implementation.
        /// </value>
        /// <remarks>
        ///     <note type="implement">
        ///         This property must not be null when the transaction is in the <see cref="TransactionState.Started"/> state.
        ///     </note>
        /// </remarks>
        new TTransaction Transaction { get; }
    }
}
