using System;
using System.Data.Common;
using System.Threading.Tasks;




namespace RI.DomainServices.Node.Transactions
{
    /// <summary>
    ///     Provides support of using an external transaction by transaction implementations.
    /// </summary>
    /// <typeparam name="TTransaction"> The concrete <see cref="DbTransaction" /> implementation. </typeparam>
    public interface ISupportExternalDbTransaction<in TTransaction> : ISupportTransaction
        where TTransaction : DbTransaction
    {
        /// <summary>
        ///     Starts the transaction using an external transaction.
        /// </summary>
        /// <param name="transaction">The external transaction to use by the transaction implementation.</param>
        /// <returns> The task to await until the transaction is started. </returns>
        /// <exception cref="ArgumentNullException"><paramref name="transaction"/> is null.</exception>
        /// <exception cref="InvalidOperationException"> The transaction is already started, committed, or rolled back. </exception>
        Task Begin(TTransaction transaction);

        /// <summary>
        /// Gets whether the used transaction is an external transaction.
        /// </summary>
        /// <value>
        /// true if the used transaction is an external transaction (provided to <see cref="Begin"/>), false if the used transaction is an internal transaction (created privately by <see cref="ISupportTransaction.Begin"/>), null if the transaction was not started.
        /// </value>
        bool? IsExternalTransaction { get; }
    }
}
