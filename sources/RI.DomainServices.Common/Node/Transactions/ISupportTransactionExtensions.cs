using System;




namespace RI.DomainServices.Node.Transactions
{
    /// <summary>
    ///     Provides utility/extension methods for the <see cref="ISupportTransaction" /> type.
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    public static class ISupportTransactionExtensions
    {
        /// <summary>
        /// Checks whether the specified transaction is already started or throws a <see cref="InvalidOperationException" /> if not.
        /// </summary>
        /// <param name="transaction">The used transaction.</param>
        /// <remarks>
        /// <para>
        /// A transaction is considered started if it is in the <see cref="TransactionState.Started"/> state.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="transaction"/> is null.</exception>
        /// <exception cref="InvalidOperationException"> The transaction is not started or was already committed or rolled back. </exception>
        public static void ThrowIfNotStarted (this ISupportTransaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            if (transaction.State != TransactionState.Started)
            {
                throw new InvalidOperationException($"Transaction not started or was already committed or rolled back ({transaction.State}).");
            }
        }

        /// <summary>
        /// Checks whether the specified transaction is not yet started and can be started or throws a <see cref="InvalidOperationException" /> if it is already started or was started before.
        /// </summary>
        /// <param name="transaction">The used transaction.</param>
        /// <remarks>
        /// <para>
        /// A transaction is considered not started if it is in the <see cref="TransactionState.NotStarted"/> state.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="transaction"/> is null.</exception>
        /// <exception cref="InvalidOperationException"> The transaction is already started, committed, or rolled back. </exception>
        public static void ThrowIfNotStartable(this ISupportTransaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            if (transaction.State != TransactionState.NotStarted)
            {
                throw new InvalidOperationException($"Transaction already started, committed, or rolled back ({transaction.State}).");
            }
        }
    }
}
