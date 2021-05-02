using System;
using System.Data.Common;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using RI.Utilities.Exceptions;




namespace RI.DomainServices.Node.Transactions
{
    /// <summary>
    ///     Provides utility/extension methods for the <see cref="ISupportEntityFrameworkCore"/> and <see cref="ISupportEntityFrameworkCore{TTransaction}" /> type.
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    public static class ISupportEntityFrameworkCoreExtensions
    {
        /// <summary>
        /// Requests a <see cref="DbContext"/> which uses the transaction provided by the current transaction.
        /// </summary>
        /// <typeparam name="TContext">The requested <see cref="DbContext"/> type.</typeparam>
        /// <param name="transaction">The transaction which supports Entity Framework Core.</param>
        /// <returns>The requested <see cref="DbContext"/> or null if it could not be resolved.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="transaction"/> is null.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="transaction"/> is not in the <see cref="TransactionState.Started"/> state.</exception>
        public static async Task<TContext> GetDbContextAsync<TContext> (this ISupportEntityFrameworkCore transaction)
        where TContext : DbContext
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            if (transaction.State != TransactionState.Started)
            {
                throw new InvalidOperationException($"{typeof(TContext).Name} could not be resolved because {transaction.GetType().Name} is not in the {nameof(TransactionState.Started)} state but in {transaction.State}");
            }

            TContext context = transaction.Services.GetService(typeof(TContext)) as TContext;

            if (context == null)
            {
                return null;
            }

            await context.Database.UseTransactionAsync(transaction.Transaction);

            return context;
        }

        /// <summary>
        /// Requests a <see cref="DbContext"/> which uses the transaction provided by the current transaction.
        /// </summary>
        /// <typeparam name="TContext">The requested <see cref="DbContext"/> type.</typeparam>
        /// <param name="transaction">The transaction which supports Entity Framework Core.</param>
        /// <returns>The requested <see cref="DbContext"/> or null if it could not be resolved.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="transaction"/> is null.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="transaction"/> is not in the <see cref="TransactionState.Started"/> state.</exception>
        public static TContext GetDbContext<TContext>(this ISupportEntityFrameworkCore transaction)
            where TContext : DbContext
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            if (transaction.State != TransactionState.Started)
            {
                throw new InvalidOperationException($"{typeof(TContext).Name} could not be resolved because {transaction.GetType().Name} is not in the {nameof(TransactionState.Started)} state but in {transaction.State}");
            }

            TContext context = transaction.Services.GetService(typeof(TContext)) as TContext;

            if (context == null)
            {
                return null;
            }

            context.Database.UseTransaction(transaction.Transaction);

            return context;
        }

        /// <summary>
        ///     Starts the transaction using an existing <see cref="DbContext"/>.
        /// </summary>
        /// <param name="transaction">The transaction which supports Entity Framework Core and external transactions.</param>
        /// <param name="context">The existing <see cref="DbContext"/> whose transaction to use by the transaction.</param>
        /// <returns> The task to await until the transaction is started. </returns>
        /// <remarks>
        ///<para>
        ///If the specified <see cref="DbContext"/> has no current transaction, one will be started.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="transaction"/> or <paramref name="context"/> is null.</exception>
        /// <exception cref="InvalidOperationException"> The transaction is already started, committed, or rolled back. </exception>
        /// <exception cref="InvalidGenericTypeArgumentException">The used transaction of <paramref name="context"/> is not compatible with <typeparamref name="TDbTransaction"/>.</exception>
        public static async Task Begin<TTransaction, TDbTransaction> (this TTransaction transaction, DbContext context)
        where TTransaction : ISupportEntityFrameworkCore<TDbTransaction>, ISupportExternalDbTransaction<TDbTransaction>
            where TDbTransaction : DbTransaction
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            transaction.ThrowIfNotStartable();

            IDbContextTransaction dbContextTransaction = context.Database.CurrentTransaction ?? await context.Database.BeginTransactionAsync();
            DbTransaction dbTransaction = dbContextTransaction.GetDbTransaction();

            if (!(dbTransaction is TDbTransaction))
            {
                throw new InvalidGenericTypeArgumentException(nameof(TDbTransaction));
            }

            await transaction.Begin((TDbTransaction)dbTransaction);
        }

        /// <summary>
        ///     Attempts to start the transaction using an existing <see cref="DbContext"/>.
        /// </summary>
        /// <param name="transaction">The transaction which supports Entity Framework Core and external transactions.</param>
        /// <param name="context">The existing <see cref="DbContext"/> whose transaction to use by the transaction.</param>
        /// <returns> true if <paramref name="context"/> has a current transaction which was used, false if <paramref name="context"/> does not have a current transaction and the transaction was therefore not started. </returns>
        /// <remarks>
        ///<para>
        ///If the specified <see cref="DbContext"/> has no current transaction, none will be started.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="transaction"/> or <paramref name="context"/> is null.</exception>
        /// <exception cref="InvalidOperationException"> The transaction is already started, committed, or rolled back. </exception>
        /// <exception cref="NotSupportedException"><typeparamref name="TDbTransaction"/> is not supported by <paramref name="context"/>.</exception>
        public static async Task<bool> TryBegin<TTransaction, TDbTransaction>(this TTransaction transaction, DbContext context)
        where TTransaction : ISupportEntityFrameworkCore<TDbTransaction>, ISupportExternalDbTransaction<TDbTransaction>
            where TDbTransaction : DbTransaction
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            transaction.ThrowIfNotStartable();

            IDbContextTransaction dbContextTransaction = context.Database.CurrentTransaction;

            if (dbContextTransaction == null)
            {
                return false;
            }

            DbTransaction dbTransaction = dbContextTransaction.GetDbTransaction();

            if (dbTransaction is TDbTransaction concrete)
            {
                await transaction.Begin(concrete);
                return true;
            }

            throw new NotSupportedException($"{dbTransaction.GetType().Name} does not support {typeof(TDbTransaction).Name}");
        }
    }
}
