using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;

using RI.DomainServices.Common.Logging;
using RI.DomainServices.Node.Objects;
using RI.DomainServices.Node.Serialization;
using RI.DomainServices.Node.Transactions;
using RI.Utilities.Exceptions;
using RI.Utilities.Text;




namespace RI.DomainServices.Node.Repositories
{
    /// <summary>
    ///     Unit-of-work for repositories using Microsoft SQL Server (<see cref="SqlRepository{TRoot}" />).
    /// </summary>
    /// <remarks>
    ///     <note type="important">
    ///         <see cref="SqlUnitOfWork" /> is intended to be used with constructor dependency injection.
    ///     </note>
    /// </remarks>
    /// <threadsafety static="false" instance="false" />
    public sealed class SqlUnitOfWork : UnitOfWorkBase, ISupportDbConnection<SqlConnection>, ISupportDbTransaction<SqlTransaction>, ISupportExternalDbConnection<SqlConnection>, ISupportExternalDbTransaction<SqlTransaction>, ISupportEntityFrameworkCore<SqlTransaction>
    {
        #region Instance Constructor/Destructor

        /// <summary>
        ///     Creates a new instance of <see cref="SqlUnitOfWork" />.
        /// </summary>
        /// <param name="options"> The options used to configure this unit-of-work (<see cref="SqlUnitOfWorkOptions" />). </param>
        /// <param name="eventSerializer"> The used integration and domain event serializer/deserializer (<see cref="IEventSerializer" />). </param>
        /// <param name="logger"> The logger the unit-of-work can use for logging.</param>
        /// <param name="serviceProvider"> The service provider the unit-of-work can use to resolve additional dependencies dynamically (<see cref="IServiceProvider" />). </param>
        /// <remarks>
        ///     <note type="important">
        ///         <see cref="SqlUnitOfWork" /> is intended to be used with constructor dependency injection.
        ///     </note>
        /// </remarks>
        /// <exception cref="ArgumentNullException"> <paramref name="options" />, <paramref name="eventSerializer" />, <paramref name="logger"/>, or <paramref name="serviceProvider" /> is null. </exception>
        public SqlUnitOfWork (SqlUnitOfWorkOptions options, IEventSerializer eventSerializer, ILogSink logger, IServiceProvider serviceProvider)
            : base(eventSerializer, logger, serviceProvider)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            this.Options = options;
        }

        #endregion




        #region Instance Fields

        private string _failInsertCommand;


        private string _inboxInsertCommand;


        private string _outboxInsertCommand;

        #endregion




        #region Instance Properties/Indexer

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

        private string InboxInsertCommand
        {
            get
            {
                if (this._inboxInsertCommand == null)
                {
                    this._inboxInsertCommand = this.ReplaceSchemaAndTableNames("INSERT INTO [@_schema].[@_inboxTable] " +
                                                                               "VALUES (@source, @timestamp, @type, @data, @taken)");
                }

                return this._inboxInsertCommand;
            }
        }

        private SqlUnitOfWorkOptions Options { get; }

        private string OutboxInsertCommand
        {
            get
            {
                if (this._outboxInsertCommand == null)
                {
                    this._outboxInsertCommand = this.ReplaceSchemaAndTableNames("INSERT INTO [@_schema].[@_outboxTable] " +
                                                                                "VALUES (@source, @timestamp, @type, @data, @taken)");
                }

                return this._outboxInsertCommand;
            }
        }

        #endregion




        #region Instance Methods

        private string ReplaceSchemaAndTableNames (string command)
        {
            return command.Replace("@_schema", this.Options.SchemaName, StringComparison.InvariantCultureIgnoreCase)
                          .Replace("@_failureTable", this.Options.FailureTableName, StringComparison.InvariantCultureIgnoreCase)
                          .Replace("@_inboxTable", this.Options.InboxTableName, StringComparison.InvariantCultureIgnoreCase)
                          .Replace("@_outboxTable", this.Options.OutboxTableName, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion




        #region Overrides

        /// <inheritdoc />
        public override async Task Publish (IEvent @event)
        {
            if (@event == null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            this.ThrowIfNotStarted();

            DateTime timestamp = DateTime.UtcNow;
            SerializedEvent serializedEvent = await this.EventSerializer.Serialize(@event);

            if (@event is IIntegrationEvent)
            {
                using (SqlCommand command = new SqlCommand(this.OutboxInsertCommand, this.Connection, this.Transaction))
                {
                    command.Parameters.Add("@source", SqlDbType.NVarChar)
                           .Value = this.GetType()
                                        .Name;

                    command.Parameters.Add("@timestamp", SqlDbType.DateTime2)
                           .Value = timestamp;

                    command.Parameters.Add("@type", SqlDbType.NVarChar)
                           .Value = serializedEvent.Type;

                    command.Parameters.Add("@data", SqlDbType.NVarChar)
                           .Value = serializedEvent.Data;

                    command.Parameters.Add("@taken", SqlDbType.DateTime2)
                           .Value = DBNull.Value;

                    await command.ExecuteNonQueryAsync();
                }
            }
            else if (@event is IDomainEvent)
            {
                using (SqlCommand command = new SqlCommand(this.InboxInsertCommand, this.Connection, this.Transaction))
                {
                    command.Parameters.Add("@source", SqlDbType.NVarChar)
                           .Value = this.GetType()
                                        .Name;

                    command.Parameters.Add("@timestamp", SqlDbType.DateTime2)
                           .Value = timestamp;

                    command.Parameters.Add("@type", SqlDbType.NVarChar)
                           .Value = serializedEvent.Type;

                    command.Parameters.Add("@data", SqlDbType.NVarChar)
                           .Value = serializedEvent.Data;

                    command.Parameters.Add("@taken", SqlDbType.DateTime2)
                           .Value = DBNull.Value;

                    await command.ExecuteNonQueryAsync();
                }
            }

            this.PublishedEvents.Add(@event);
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

            this.Transaction = (SqlTransaction)await this.Connection.BeginTransactionAsync();

            this.IsExternalConnection = false;
            this.IsExternalTransaction = false;

            this.State = TransactionState.Started;
        }

        /// <inheritdoc />
        public override async Task Commit ()
        {
            this.ThrowIfNotStarted();

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
            this.Logger.LogError("Unit-of-work transaction failed", exception: exception);

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

            await this.Rollback();
        }

        /// <inheritdoc />
        public override async Task Rollback ()
        {
            if ((this.State != TransactionState.Committed) && (this.State != TransactionState.RolledBack))
            {
                this.Logger.LogWarning("Rolling back unit-of-work transaction");
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

            this.Transaction = (SqlTransaction)await this.Connection.BeginTransactionAsync();

            this.IsExternalConnection = true;
            this.IsExternalTransaction = false;

            this.State = TransactionState.Started;
        }

        #endregion




        #region Interface: ISupportExternalDbTransaction<SqlTransaction>

        /// <inheritdoc />
        public bool? IsExternalTransaction { get; private set; }

        /// <inheritdoc />
        public Task Begin (SqlTransaction transaction)
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

            return Task.CompletedTask;
        }

        #endregion
    }
}
