using System;
using System.Data.Common;
using System.Threading.Tasks;




namespace RI.DomainServices.Node.Transactions
{
    /// <summary>
    ///     Provides utility/extension methods for the <see cref="ISupportExternalDbTransaction{TTransaction}" /> type.
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    public static class ISupportExternalDbTransactionExtensions
    {
        /// <summary>
        /// Attaches a transaction to another transaction as an inner transaction.
        /// </summary>
        /// <typeparam name="TTransaction"> The concrete <see cref="DbTransaction" /> implementation. </typeparam>
        /// <param name="innerTransaction">The inner transaction to be attached.</param>
        /// <param name="outerTransaction">The outer transaction the inner transaction is attached to.</param>
        /// <returns> The task to await until the transaction is attached. </returns>
        /// <remarks>
        ///<note type="important">
        /// The inner transaction will not have its own connection or transaction.
        /// It will use the connection and transaction shared by the outer transaction.
        /// </note>
        ///<para>
        /// The outer transaction must already be started while the inner transaction must not yet be started but startable.
        /// The inner transaction will be started using the outer transaction.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="innerTransaction"/> or <paramref name="outerTransaction"/> is null.</exception>
        /// <exception cref="InvalidOperationException"> <paramref name="innerTransaction"/> is already started, committed, or rolled back or <paramref name="outerTransaction"/> is not started or was already committed or rolled back.</exception>
        public static async Task AttachTo<TTransaction>(this ISupportExternalDbTransaction<TTransaction> innerTransaction, ISupportDbTransaction<TTransaction> outerTransaction)
            where TTransaction : DbTransaction
        {
            if (innerTransaction == null)
            {
                throw new ArgumentNullException(nameof(innerTransaction));
            }

            if (outerTransaction == null)
            {
                throw new ArgumentNullException(nameof(outerTransaction));
            }

            innerTransaction.ThrowIfNotStartable();
            outerTransaction.ThrowIfNotStarted();

            await innerTransaction.Begin(outerTransaction.Transaction);
        }

        /// <summary>
        /// Attaches a transaction to another transaction as an inner transaction.
        /// </summary>
        /// <typeparam name="TTransaction"> The concrete <see cref="DbTransaction" /> implementation. </typeparam>
        /// <param name="innerTransaction">The inner transaction to be attached.</param>
        /// <param name="outerTransaction">The outer transaction the inner transaction is attached to.</param>
        /// <returns> The task to await until the transaction is attached. </returns>
        /// <remarks>
        ///<note type="important">
        /// The inner transaction will not have its own connection or transaction.
        /// It will use the connection and transaction shared by the outer transaction.
        /// </note>
        ///<para>
        /// The outer transaction must already be started while the inner transaction must not yet be started but startable.
        /// The inner transaction will be started using the outer transaction.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="innerTransaction"/> or <paramref name="outerTransaction"/> is null.</exception>
        /// <exception cref="InvalidOperationException"> <paramref name="innerTransaction"/> is already started, committed, or rolled back or <paramref name="outerTransaction"/> is not started or was already committed or rolled back.</exception>
        /// <exception cref="NotSupportedException"><typeparamref name="TTransaction"/> is not supported by <paramref name="outerTransaction"/>.</exception>
        public static async Task AttachTo<TTransaction>(this ISupportExternalDbTransaction<TTransaction> innerTransaction, ISupportTransaction outerTransaction)
            where TTransaction : DbTransaction
        {
            if (innerTransaction == null)
            {
                throw new ArgumentNullException(nameof(innerTransaction));
            }

            if (outerTransaction == null)
            {
                throw new ArgumentNullException(nameof(outerTransaction));
            }

            innerTransaction.ThrowIfNotStarted();
            outerTransaction.ThrowIfNotStartable();

            if (outerTransaction is ISupportDbTransaction<TTransaction> concreteOuter)
            {
                await innerTransaction.AttachTo(concreteOuter);
                return;
            }

            throw new NotSupportedException($"{outerTransaction.GetType().Name} does not support {innerTransaction.GetType().Name}");
        }
    }
}
