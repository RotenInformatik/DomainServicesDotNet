using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;




namespace RI.DomainServices.Node.Transactions
{
    /// <summary>
    ///     Provides utility/extension methods for the <see cref="ISupportExternalDbConnection{TConnection}" /> type.
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    public static class ISupportExternalDbConnectionExtensions
    {
        /// <summary>
        ///     Attempts to start the transaction using an external connection.
        /// </summary>
        /// <typeparam name="TConnection"> The concrete <see cref="DbConnection" /> implementation. </typeparam>
        /// <param name="transaction">The used transaction.</param>
        /// <param name="connection">The external connection to use by the transaction implementation.</param>
        /// <returns> true if <paramref name="connection"/> is already open, false if <paramref name="connection"/> is not open and the transaction was therefore not started. </returns>
        /// <remarks>
        ///<para>
        ///If the specified connection is not started, it will not be started.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="transaction"/> or <paramref name="connection"/> is null.</exception>
        /// <exception cref="InvalidOperationException"> The transaction is already started, committed, or rolled back. </exception>
        public static async Task<bool> TryBegin<TConnection>(this ISupportExternalDbConnection<TConnection> transaction, TConnection connection)
            where TConnection : DbConnection
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            transaction.ThrowIfNotStartable();

            if (connection.State != ConnectionState.Open)
            {
                return false;
            }

            await transaction.Begin(connection);

            return true;
        }
    }
}
