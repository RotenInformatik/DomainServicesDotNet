using System;

using RI.Utilities.ObjectModel;




namespace RI.DomainServices.Node.Repositories
{
    /// <summary>
    ///     Configures unit-of-work for repositories using Microsoft SQL Server (<see cref="SqlUnitOfWork" />).
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    public sealed class SqlUnitOfWorkOptions : ICloneable<SqlUnitOfWorkOptions>, ICloneable, ICopyable<SqlUnitOfWorkOptions>
    {
        #region Instance Properties/Indexer

        /// <summary>
        ///     Gets or sets the connection string used to connect to the database which stores the inbox, outbox, and failure table.
        /// </summary>
        /// <value>
        /// The connection string used to connect to the database which stores the inbox, outbox, and failure table or null if an external connection or transaction will be provided.
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
        ///     Gets or sets the table name of the outbox table.
        /// </summary>
        /// <value>
        /// The table name of the outbox table.
        /// </value>
        /// <remarks>
        ///     <para>
        ///         The default value for <see cref="OutboxTableName" /> is <c> Outbox </c>.
        ///     </para>
        /// </remarks>
        public string OutboxTableName { get; set; } = "Outbox";

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
        ///     Gets or sets the name of the database schema which contains the inbox, outbox, and failure table.
        /// </summary>
        /// <value>
        /// The name of the database schema which contains the inbox, outbox, and failure table.
        /// </value>
        /// <remarks>
        ///     <para>
        ///         The default value for <see cref="SchemaName" /> is <c> dbo </c>.
        ///     </para>
        /// </remarks>
        public string SchemaName { get; set; } = "dbo";

        #endregion




        #region Interface: ICloneable<SqlUnitOfWorkOptions>

        /// <inheritdoc />
        public SqlUnitOfWorkOptions Clone ()
        {
            SqlUnitOfWorkOptions clone = new SqlUnitOfWorkOptions();
            clone.ConnectionString = this.ConnectionString;
            clone.SchemaName = this.SchemaName;
            clone.InboxTableName = this.InboxTableName;
            clone.OutboxTableName = this.OutboxTableName;
            clone.FailureTableName = this.FailureTableName;
            return clone;
        }

        /// <inheritdoc />
        object ICloneable.Clone ()
        {
            return this.Clone();
        }

        /// <inheritdoc />
        public void CopyTo(SqlUnitOfWorkOptions other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            other.ConnectionString = this.ConnectionString;
            other.SchemaName = this.SchemaName;
            other.InboxTableName = this.InboxTableName;
            other.OutboxTableName = this.OutboxTableName;
            other.FailureTableName = this.FailureTableName;
        }

        #endregion
    }
}
