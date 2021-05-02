using System;
using System.Data.Common;




namespace RI.DomainServices.Node.Transactions
{
    /// <summary>
    ///     Provides support of Entity Framework Core by transaction implementations.
    /// </summary>
    /// <remarks>
    ///     <note type="important">
    ///         <see cref="ISupportEntityFrameworkCore" /> does not actually implement any Entity Framework Core related functionality but merely acts as a contract so that all required properties are available to provide support for Entity Framework Core.
    ///     </note>
    /// </remarks>
    public interface ISupportEntityFrameworkCore : ISupportDbTransaction
    {
        /// <summary>
        ///     Gets the <see cref="IServiceProvider" /> provided by the transaction implementation.
        /// </summary>
        /// <value>
        ///     The <see cref="IServiceProvider" /> provided by the transaction implementation.
        /// </value>
        /// <remarks>
        ///     <note type="implement">
        ///         This property must not be null when the transaction is in the <see cref="TransactionState.Started"/> state.
        ///     </note>
        /// </remarks>
        IServiceProvider Services { get; }
    }

    /// <summary>
    ///     Provides support of Entity Framework Core by transaction implementations.
    /// </summary>
    /// <typeparam name="TTransaction"> The concrete <see cref="DbTransaction" /> implementation. </typeparam>
    /// <remarks>
    ///     <note type="important">
    ///         <see cref="ISupportEntityFrameworkCore{TTransaction}" /> does not actually implement any Entity Framework Core related functionality but merely acts as a contract so that all required properties are available to provide support for Entity Framework Core.
    ///     </note>
    /// </remarks>
    public interface ISupportEntityFrameworkCore<out TTransaction> : ISupportEntityFrameworkCore, ISupportDbTransaction<TTransaction>
        where TTransaction : DbTransaction
    {
    }
}
