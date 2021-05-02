using System;
using System.Data.Common;
using System.Threading.Tasks;




namespace RI.DomainServices.Node.Transactions
{
    /// <summary>
    ///     Provides support of using an external connection by transaction implementations.
    /// </summary>
    /// <typeparam name="TConnection"> The concrete <see cref="DbConnection" /> implementation. </typeparam>
    public interface ISupportExternalDbConnection<in TConnection> : ISupportTransaction
        where TConnection : DbConnection
    {
        /// <summary>
        ///     Starts the transaction using an external connection.
        /// </summary>
        /// <param name="connection">The external connection to use by the transaction implementation.</param>
        /// <returns> The task to await until the transaction is started. </returns>
        /// <remarks>
        ///<para>
        ///If the specified connection is not started, it will be started.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="connection"/> is null.</exception>
        /// <exception cref="InvalidOperationException"> The transaction is already started, committed, or rolled back. </exception>
        Task Begin(TConnection connection);

        /// <summary>
        /// Gets whether the used connection is an external connection.
        /// </summary>
        /// <value>
        /// true if the used connection is an external connection (provided to <see cref="Begin"/>), false if the used connection is an internal connection (created privately by <see cref="ISupportTransaction.Begin"/>), null if the transaction was not started.
        /// </value>
        bool? IsExternalConnection { get; }
    }
}
