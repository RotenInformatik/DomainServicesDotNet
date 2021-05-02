using System;

using RI.Utilities.ObjectModel;




namespace RI.DomainServices.Node.Inbox
{
    /// <summary>
    ///     Configures inbox queue using Microsoft SQL Server (<see cref="SqlInboxQueue" />).
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    public sealed class SqlInboxQueueOptions : ICloneable<SqlInboxQueueOptions>, ICloneable, ICopyable<SqlInboxQueueOptions>
    {
        #region Instance Properties/Indexer

        /// <summary>
        ///     Gets or sets the connection string used to connect to the database which stores the inbox and failure table.
        /// </summary>
        /// <value>
        /// The connection string used to connect to the database which stores the inbox and failure table or null if an external connection or transaction will be provided.
        /// </value>
        /// <remarks>
        ///     <para>
        ///         The default value for <see cref="ConnectionString" /> is null.
        ///     </para>
        /// </remarks>
        public string ConnectionString { get; set; } = null;

        /// <summary>
        ///     Gets or sets the table name of the inbox table.
        /// </summary>
        /// <value>
        /// The table name of the inbox table.
        /// </value>
        /// <remarks>
        ///     <para>
        ///         The default value for <see cref="InboxTableName" /> is <c> Inbox </c>.
        ///     </para>
        /// </remarks>
        public string InboxTableName { get; set; } = "Inbox";

        /// <summary>
        ///     Gets or sets the table name of the failure table.
        /// </summary>
        /// <value>
        /// The table name of the failure table.
        /// </value>
        /// <remarks>
        ///     <para>
        ///         The default value for <see cref="FailureTableName" /> is <c> Failures </c>.
        ///     </para>
        /// </remarks>
        public string FailureTableName { get; set; } = "Failures";

        /// <summary>
        ///     Gets or sets the name of the database schema which contains the inbox and failure table.
        /// </summary>
        /// <value>
        /// The name of the database schema which contains the inbox and failure table.
        /// </value>
        /// <remarks>
        ///     <para>
        ///         The default value for <see cref="SchemaName" /> is <c> dbo </c>.
        ///     </para>
        /// </remarks>
        public string SchemaName { get; set; } = "dbo";

        /// <summary>
        ///     Gets or sets the delay after which an inbox message processing is retried when its previous processing interrupted non-gracefully (e.g. the process terminated with no chance of updating the database to indicate failure).
        /// </summary>
        /// <value>
        /// The retry delay.
        /// </value>
        /// <remarks>
        ///     <note type="important">
        ///         In a scaled distributed setting, multiple computation nodes could process an inbox database in parallel (e.g. by running multiple instances of the same service).
        /// In such a scenario, the only shared element of the nodes is the same database, more precisely the inbox table.
        /// When dequeuing a message, a flag is set on the corresponding row in the database to indicate its being processed (so that a parallel process or node does not select the same message for processing).
        /// Now, if a node or process crashes without the opportunity to reset the flag, other nodes/processes only see the flag (indicating the message being processed) and have therefore no way to determine whether the flag is still valid.
        /// To prevent a message staying in the queue forever without being further processed, the processing for a message is retried after the delay specified by <see cref="NonGracefulRetryDelay"/> from the point in time the flag was set.
        /// Therefore, if the delay is too short, parallel processing of the same message could be triggered.
        /// Furthermore, this implies that for non-gracefully failed message processing, the order of the messages being processed is no longer preserved.
        ///     </note>
        ///     <para>
        ///         The default value for <see cref="SchemaName" /> is 10 minutes.
        ///     </para>
        /// </remarks>
        public TimeSpan NonGracefulRetryDelay { get; set; } = TimeSpan.FromMinutes(10);

        #endregion




        #region Interface: ICloneable<SqlUnitOfWorkOptions>

        /// <inheritdoc />
        public SqlInboxQueueOptions Clone()
        {
            SqlInboxQueueOptions clone = new SqlInboxQueueOptions();
            clone.ConnectionString = this.ConnectionString;
            clone.SchemaName = this.SchemaName;
            clone.InboxTableName = this.InboxTableName;
            clone.FailureTableName = this.FailureTableName;
            clone.NonGracefulRetryDelay = this.NonGracefulRetryDelay;
            return clone;
        }

        /// <inheritdoc />
        object ICloneable.Clone()
        {
            return this.Clone();
        }

        /// <inheritdoc />
        public void CopyTo(SqlInboxQueueOptions other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            other.ConnectionString = this.ConnectionString;
            other.SchemaName = this.SchemaName;
            other.InboxTableName = this.InboxTableName;
            other.FailureTableName = this.FailureTableName;
            other.NonGracefulRetryDelay = this.NonGracefulRetryDelay;
        }

        #endregion
    }
}
