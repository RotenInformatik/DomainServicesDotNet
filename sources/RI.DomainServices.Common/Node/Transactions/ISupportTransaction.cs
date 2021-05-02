using System;
using System.Threading.Tasks;




namespace RI.DomainServices.Node.Transactions
{
    /// <summary>
    /// Implements a transaction.
    /// </summary>
    public interface ISupportTransaction
    {
        /// <summary>
        ///     Starts the transaction.
        /// </summary>
        /// <returns> The task to await until the transaction is started. </returns>
        /// <exception cref="InvalidOperationException"> The transaction is already started, committed, or rolled back. </exception>
        Task Begin();

        /// <summary>
        ///     Commits the transaction.
        /// </summary>
        /// <returns> The task to await until the transaction is committed. </returns>
        /// <exception cref="InvalidOperationException"> The transaction is not started or was already committed or rolled back. </exception>
        Task Commit();

        /// <summary>
        ///     Rolls back the transaction.
        /// </summary>
        /// <returns> The task to await until the transaction is rolled back. </returns>
        /// <remarks>
        ///     <note type="implement">
        ///         <see cref="Rollback" /> must be callable at any time and also repeatedly, regardless of the state of the transaction.
        ///     </note>
        /// </remarks>
        Task Rollback();

        /// <summary>
        ///     Fails the transaction.
        /// </summary>
        /// <param name="exception">The exception which caused the transaction to fail.</param>
        /// <returns> The task to await until the transaction is failed. </returns>
        /// <remarks>
        ///     <note type="implement">
        ///         <see cref="Fail" /> must be callable at any time and also repeatedly, regardless of the state of the transaction.
        ///     </note>
        ///     <note type="implement">
        ///         <see cref="Fail" /> must be able to deal with <paramref name="exception"/> being null.
        /// In such cases, the same behaviour as <see cref="Rollback"/> is expected.
        ///     </note>
        /// </remarks>
        Task Fail(Exception exception);

        /// <summary>
        ///     Gets the current transaction state of the unit-of-work.
        /// </summary>
        /// <value>
        /// The current transaction state of the unit-of-work.
        /// </value>
        TransactionState State { get; }
    }
}
