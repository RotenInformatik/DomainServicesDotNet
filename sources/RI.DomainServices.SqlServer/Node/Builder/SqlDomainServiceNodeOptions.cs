using System;

using RI.DomainServices.Node.Inbox;
using RI.DomainServices.Node.Repositories;
using RI.Utilities.ObjectModel;




namespace RI.DomainServices.Node.Builder
{
    /// <summary>
    ///     Configures domain services using Microsoft SQL Server (<see cref="SqlInboxQueue" />, <see cref="SqlUnitOfWork"/>).
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    public sealed class SqlDomainServiceNodeOptions : ICloneable<SqlDomainServiceNodeOptions>, ICloneable, ICopyable<SqlDomainServiceNodeOptions>
    {
        #region Instance Properties/Indexer

        /// <summary>
        /// Gets the options for the unit-of-work.
        /// </summary>
        /// <value>
        /// The options for the unit-of-work.
        /// </value>
        public SqlUnitOfWorkOptions UnitOfWork { get; private set; } = new SqlUnitOfWorkOptions();

        /// <summary>
        /// Gets the options for the inbox queue.
        /// </summary>
        /// <value>
        /// The options for the inbox queue.
        /// </value>
        public SqlInboxQueueOptions InboxQueue { get; private set; } = new SqlInboxQueueOptions();

        #endregion

        




        #region Interface: ICloneable<SqlUnitOfWorkOptions>

        /// <inheritdoc />
        public SqlDomainServiceNodeOptions Clone()
        {
            SqlDomainServiceNodeOptions clone = new SqlDomainServiceNodeOptions();
            clone.UnitOfWork = this.UnitOfWork.Clone();
            clone.InboxQueue = this.InboxQueue.Clone();
            return clone;
        }

        /// <inheritdoc />
        object ICloneable.Clone()
        {
            return this.Clone();
        }

        #endregion




        /// <inheritdoc />
        public void CopyTo (SqlDomainServiceNodeOptions other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            this.UnitOfWork.CopyTo(other.UnitOfWork);
            this.InboxQueue.CopyTo(other.InboxQueue);
        }
    }
}
