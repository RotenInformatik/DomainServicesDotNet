using System.Data.Common;




namespace RI.DomainServices.Node.Transactions
{
    /// <summary>
    ///     Provides support of <see cref="DbConnection" /> by transaction implementations.
    /// </summary>
    public interface ISupportDbConnection : ISupportTransaction
    {
        /// <summary>
        ///     Gets the <see cref="DbConnection" /> used by the transaction implementation.
        /// </summary>
        /// <value>
        ///     The <see cref="DbConnection" /> used by the transaction implementation.
        /// </value>
        /// <remarks>
        ///     <note type="implement">
        ///         This property must not be null when the transaction is in the <see cref="TransactionState.Started"/> state.
        ///     </note>
        /// </remarks>
        DbConnection Connection { get; }
    }

    /// <summary>
    ///     Provides support of <see cref="DbConnection" /> by transaction implementations.
    /// </summary>
    /// <typeparam name="TConnection"> The concrete <see cref="DbConnection" /> implementation. </typeparam>
    public interface ISupportDbConnection <out TConnection> : ISupportDbConnection
        where TConnection : DbConnection
    {
        /// <summary>
        ///     Gets the <typeparamref name="TConnection"/> used by the transaction implementation.
        /// </summary>
        /// <value>
        ///     The <typeparamref name="TConnection"/> used by the transaction implementation.
        /// </value>
        /// <remarks>
        ///     <note type="implement">
        ///         This property must not be null when the transaction is in the <see cref="TransactionState.Started"/> state.
        ///     </note>
        /// </remarks>
        new TConnection Connection { get; }
    }
}
