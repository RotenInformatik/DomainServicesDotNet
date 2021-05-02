using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;

using RI.DomainServices.Common.Logging;
using RI.DomainServices.Node.Objects;
using RI.DomainServices.Node.Serialization;
using RI.DomainServices.Node.Transactions;
using RI.Utilities.Dates;
using RI.Utilities.Exceptions;
using RI.Utilities.Text;




namespace RI.DomainServices.Node.Inbox
{
    /// <summary>
    ///     Inbox queue using Microsoft SQL Server.
    /// </summary>
    /// <remarks>
    ///     <note type="important">
    ///         <see cref="SqlInboxQueue" /> is intended to be used with constructor dependency injection.
    ///     </note>
    /// </remarks>
    /// <threadsafety static="false" instance="false" />
    public sealed class SqlInboxQueue : InboxQueueBase, ISupportDbConnection<SqlConnection>, ISupportDbTransaction<SqlTransaction>, ISupportExternalDbConnection<SqlConnection>, ISupportExternalDbTransaction<SqlTransaction>, ISupportEntityFrameworkCore<SqlTransaction>
    {
        #region Instance Constructor/Destructor

        /// <summary>
        ///     Creates a new instance of <see cref="SqlInboxQueue" />.
        /// </summary>
        /// <param name="options"> The options used to configure this inbox queue (<see cref="SqlInboxQueueOptions" />). </param>
        /// <param name="eventSerializer"> The used integration and domain event serializer/deserializer (<see cref="IEventSerializer" />). </param>
        /// <param name="logger"> The logger the inbox queue can use for logging.</param>
        /// <param name="serviceProvider"> The service provider the inbox queue can use to resolve additional dependencies dynamically (<see cref="IServiceProvider" />). </param>
        /// <remarks>
        ///     <note type="important">
        ///         <see cref="SqlInboxQueue" /> is intended to be used with constructor dependency injection.
        ///     </note>
        /// </remarks>
        /// <exception cref="ArgumentNullException"> <paramref name="options" />, <paramref name="eventSerializer" />, <paramref name="logger"/>, or <paramref name="serviceProvider" /> is null. </exception>
        public SqlInboxQueue (SqlInboxQueueOptions options, IEventSerializer eventSerializer, ILogSink logger, IServiceProvider serviceProvider)
            : base(eventSerializer, logger, serviceProvider)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            this.Options = options;

            this.OrderPreservation = OrderPreservation.AlwaysExceptRestart;
            this.DuplicateAvoidance = DuplicateAvoidance.AlwaysExceptRestart;
        }

        #endregion




        #region Instance Fields

        private string _failInsertCommand;

        private string _fetchUpdateCommand;

        private string _finishDeleteCommand;

        private string _finishUpdateCommand;

        #endregion




        #region Instance Properties/Indexer

        private long? CurrentEventId { get; set; }

        private string FailInsertCommand
        {
            get
            {
                if (this._failInsertCommand == null)
                {
                    this._failInsertCommand = this.ReplaceSchemaAndTableNames("INSERT INTO [@_schema].[@_failureTable] " +
                                                                              "VALUES (@source, @timestamp, @eventType, @eventData, @exception)");
                }

                return this._failInsertCommand;
            }
        }

        private string FetchUpdateCommand
        {
            get
            {
                if (this._fetchUpdateCommand == null)
                {
                    this._fetchUpdateCommand = this.ReplaceSchemaAndTableNames("UPDATE [@_schema].[@_inboxTable] " +
                                                                               "SET taken = @now " +
                                                                               "OUTPUT INSERTED.id, INSERTED.type, INSERTED.data" +
                                                                               "FROM (SELECT TOP 1 id, type, data FROM [@_schema].[@_inboxTable] WHERE ([@_schema].[@_inboxTable].taken = NULL) OR ([@_schema].[@_inboxTable].taken < @timeout) ORDER BY id ASC) AS _inner " +
                                                                               "WHERE [@_schema].[@_inboxTable].id = _inner.id");
                }

                return this._fetchUpdateCommand;
            }
        }

        private string FinishDeleteCommand
        {
            get
            {
                if (this._finishDeleteCommand == null)
                {
                    this._finishDeleteCommand = this.ReplaceSchemaAndTableNames("DELETE FROM [@_schema].[@_inboxTable] " +
                                                                                "WHERE id = @id");
                }

                return this._finishDeleteCommand;
            }
        }

        private string FinishUpdateCommand
        {
            get
            {
                if (this._finishUpdateCommand == null)
                {
                    this._finishUpdateCommand = this.ReplaceSchemaAndTableNames("UPDATE [@_schema].[@_inboxTable] " +
                                                                                "SET taken = NULL " +
                                                                                "WHERE id = @id");
                }

                return this._finishUpdateCommand;
            }
        }

        private SqlInboxQueueOptions Options { get; }

        #endregion




        #region Instance Methods

        private void ClearCurrentEvent ()
        {
            this.CurrentEventId = null;
            this.CurrentEventSerialized = null;
            this.CurrentEvent = null;
        }

        private async Task FetchNextEvent ()
        {
            this.ClearCurrentEvent();

            if (this.Options.NonGracefulRetryDelay.IsNegative() || this.Options.NonGracefulRetryDelay.IsZero())
            {
                throw new Exception($"{nameof(this.Options.NonGracefulRetryDelay)} is zero or less.");
            }

            using (SqlCommand command = new SqlCommand(this.FetchUpdateCommand, this.Connection))
            {
                DateTime now = DateTime.UtcNow;
                DateTime timeout = now.Subtract(this.Options.NonGracefulRetryDelay);

                command.Parameters.Add("@now", SqlDbType.DateTime2)
                       .Value = now;

                command.Parameters.Add("@timeout", SqlDbType.DateTime2)
                       .Value = timeout;

                using (SqlDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if (await reader.ReadAsync())
                    {
                        long id = reader.GetInt64("id");
                        string type = reader.GetString("type");
                        string data = reader.GetString("data");

                        SerializedEvent serializedEvent = new SerializedEvent(type, data);
                        IEvent @event = await this.EventSerializer.Deserialize(serializedEvent);

                        this.CurrentEventId = id;
                        this.CurrentEventSerialized = serializedEvent;
                        this.CurrentEvent = @event;
                    }
                }
            }
        }

        private async Task FinishCurrentEvent (FinishEvent finishEvent)
        {
            long? id = this.CurrentEventId;

            this.ClearCurrentEvent();

            if ((this.State == TransactionState.Started) && id.HasValue)
            {
                SqlCommand command = null;

                try
                {
                    switch (finishEvent)
                    {
                        case FinishEvent.Success:
                            command = new SqlCommand(this.FinishDeleteCommand, this.Connection, this.Transaction);
                            break;

                        case FinishEvent.Fail:
                            command = new SqlCommand(this.FinishDeleteCommand, this.Connection);
                            break;

                        case FinishEvent.Rollback:
                            command = new SqlCommand(this.FinishUpdateCommand, this.Connection);
                            break;

                        default:
                            throw new ArgumentException("Invalid enum value", nameof(finishEvent));
                    }

                    command.Parameters.Add("@id", SqlDbType.BigInt)
                           .Value = id.Value;

                    await command.ExecuteNonQueryAsync();
                }
                finally
                {
                    command?.Dispose();
                }
            }
        }

        private string ReplaceSchemaAndTableNames (string command)
        {
            return command.Replace("@_schema", this.Options.SchemaName, StringComparison.InvariantCultureIgnoreCase)
                          .Replace("@_failureTable", this.Options.FailureTableName, StringComparison.InvariantCultureIgnoreCase)
                          .Replace("@_inboxTable", this.Options.InboxTableName, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion




        #region Interface: ISupportDbConnection<SqlConnection>

        /// <inheritdoc />
        public SqlConnection Connection { get; private set; }

        /// <inheritdoc />
        DbConnection ISupportDbConnection.Connection => this.Connection;

        /// <inheritdoc />
        public override async Task Begin ()
        {
            this.ThrowIfNotStartable();

            if (this.Options.ConnectionString.IsNullOrEmptyOrWhitespace())
            {
                throw new InvalidOperationException("No connection string is provided.");
            }

            this.State = TransactionState.Starting;

            this.Connection = new SqlConnection(this.Options.ConnectionString);
            await this.Connection.OpenAsync();

            this.Transaction = (SqlTransaction)await this.Connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            this.IsExternalConnection = false;
            this.IsExternalTransaction = false;

            this.State = TransactionState.Started;

            await this.FetchNextEvent();
        }

        /// <inheritdoc />
        public override async Task Commit ()
        {
            this.ThrowIfNotStarted();

            await this.FinishCurrentEvent(FinishEvent.Success);

            this.State = TransactionState.Committing;

            if (!this.IsExternalTransaction.GetValueOrDefault())
            {
                await this.Transaction.CommitAsync();
            }

            if (!this.IsExternalConnection.GetValueOrDefault())
            {
                await this.Connection.CloseAsync();
            }

            this.State = TransactionState.Committed;
        }

        /// <inheritdoc />
        public override async Task Fail (Exception exception)
        {
            this.Logger.LogError("Inbox transaction failed", exception: exception);

            if ((this.Connection?.State).GetValueOrDefault(ConnectionState.Closed) == ConnectionState.Open)
            {
                DateTime timestamp = DateTime.UtcNow;
                string details = exception?.ToDetailedString();

                using (SqlCommand command = new SqlCommand(this.FailInsertCommand, this.Connection))
                {
                    command.Parameters.Add("@source", SqlDbType.NVarChar)
                           .Value = this.GetType()
                                        .Name;

                    command.Parameters.Add("@timestamp", SqlDbType.DateTime2)
                           .Value = timestamp;

                    command.Parameters.Add("@eventType", SqlDbType.NVarChar)
                           .Value = DBNull.Value;

                    command.Parameters.Add("@eventData", SqlDbType.NVarChar)
                           .Value = DBNull.Value;

                    command.Parameters.Add("@exception", SqlDbType.NVarChar)
                           .Value = details ?? (object)DBNull.Value;

                    await command.ExecuteNonQueryAsync();
                }
            }

            await this.FinishCurrentEvent(FinishEvent.Fail);

            await this.Rollback();
        }

        /// <inheritdoc />
        public override async Task Rollback ()
        {
            await this.FinishCurrentEvent(FinishEvent.Rollback);

            if ((this.State != TransactionState.Committed) && (this.State != TransactionState.RolledBack))
            {
                this.Logger.LogWarning("Rolling back inbox transaction");
                this.State = TransactionState.RollingBack;
            }

            if ((this.Transaction != null) && !this.IsExternalTransaction.GetValueOrDefault())
            {
                await this.Transaction.RollbackAsync();
            }

            if ((this.Connection != null) && !this.IsExternalConnection.GetValueOrDefault())
            {
                await this.Connection.CloseAsync();
            }

            if (this.State == TransactionState.RollingBack)
            {
                this.State = TransactionState.RolledBack;
            }
        }

        #endregion




        #region Interface: ISupportDbTransaction<SqlTransaction>

        /// <inheritdoc />
        public SqlTransaction Transaction { get; private set; }

        /// <inheritdoc />
        DbTransaction ISupportDbTransaction.Transaction => this.Transaction;

        #endregion




        #region Interface: ISupportEntityFrameworkCore<SqlTransaction>

        /// <inheritdoc />
        IServiceProvider ISupportEntityFrameworkCore.Services => this.ServiceProvider;

        #endregion




        #region Interface: ISupportExternalDbConnection<SqlConnection>

        /// <inheritdoc />
        public bool? IsExternalConnection { get; private set; }


        /// <inheritdoc />
        public async Task Begin (SqlConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            this.ThrowIfNotStartable();

            this.State = TransactionState.Starting;

            this.Connection = connection;

            if (this.Connection.State != ConnectionState.Open)
            {
                await this.Connection.OpenAsync();
            }

            this.Transaction = (SqlTransaction)await this.Connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            this.IsExternalConnection = true;
            this.IsExternalTransaction = false;

            this.State = TransactionState.Started;

            await this.FetchNextEvent();
        }

        #endregion




        #region Interface: ISupportExternalDbTransaction<SqlTransaction>

        /// <inheritdoc />
        public bool? IsExternalTransaction { get; private set; }

        /// <inheritdoc />
        public async Task Begin (SqlTransaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            this.ThrowIfNotStartable();

            this.State = TransactionState.Starting;

            this.Connection = transaction.Connection;

            this.Transaction = transaction;

            this.IsExternalConnection = true;
            this.IsExternalTransaction = true;

            this.State = TransactionState.Started;

            await this.FetchNextEvent();
        }

        #endregion




        #region Type: FinishEvent

        private enum FinishEvent
        {
            Success,

            Fail,

            Rollback,
        }

        #endregion
    }
}
