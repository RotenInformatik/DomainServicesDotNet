using System;

using RI.DomainServices.Common.Builder;
using RI.DomainServices.Node.Inbox;
using RI.DomainServices.Node.Repositories;
using RI.Utilities.Exceptions;
using RI.Utilities.Text;




namespace RI.DomainServices.Node.Builder
{
    /// <summary>
    ///     Provides utility/extension methods for the <see cref="DomainServiceNodeBuilder" /> type.
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    public static class SqlDomainServiceNodeBuilderExtensions
    {
        #region Static Methods

        /// <summary>
        ///     Registers services for repositories using Microsoft SQL Server (<see cref="SqlRepository{TRoot}" />).
        /// </summary>
        /// <param name="builder"> The service builder being configured. </param>
        /// <param name="configure"> The configuration callback used to configure unit-of-work options. </param>
        /// <returns> The service builder being configured. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> or <paramref name="configure" /> is null. </exception>
        public static DomainServiceNodeBuilder UseSqlServerRepositories (this DomainServiceNodeBuilder builder, Action<SqlUnitOfWorkOptions> configure)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            builder.AddTransient(typeof(SqlUnitOfWorkOptions), _ =>
            {
                SqlUnitOfWorkOptions options = new SqlUnitOfWorkOptions();
                configure(options);
                return options;
            });

            builder.AddTransient(typeof(SqlUnitOfWork), typeof(SqlUnitOfWork));

            return builder;
        }

        /// <summary>
        ///     Registers services for repositories using Microsoft SQL Server (<see cref="SqlRepository{TRoot}" />).
        /// </summary>
        /// <param name="builder"> The service builder being configured. </param>
        /// <param name="options"> The used unit-of-work options. </param>
        /// <returns> The service builder being configured. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> or <paramref name="options" /> is null. </exception>
        public static DomainServiceNodeBuilder UseSqlServerRepositories (this DomainServiceNodeBuilder builder, SqlUnitOfWorkOptions options)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            builder.AddTransient(typeof(SqlUnitOfWorkOptions), _ => options.Clone());

            builder.AddTransient(typeof(SqlUnitOfWork), typeof(SqlUnitOfWork));

            return builder;
        }

        /// <summary>
        ///     Registers services for repositories using Microsoft SQL Server (<see cref="SqlRepository{TRoot}" />).
        /// </summary>
        /// <param name="builder"> The service builder being configured. </param>
        /// <param name="connectionString"> The connection string used by the unit-of-work. </param>
        /// <returns> The service builder being configured. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> or <paramref name="connectionString" /> is null. </exception>
        /// <exception cref="EmptyStringArgumentException"> <paramref name="connectionString" /> is empty or only whitespaces. </exception>
        public static DomainServiceNodeBuilder UseSqlServerRepositories (this DomainServiceNodeBuilder builder, string connectionString)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            if (connectionString.IsNullOrEmptyOrWhitespace())
            {
                throw new EmptyStringArgumentException(nameof(connectionString));
            }

            SqlUnitOfWorkOptions options = new SqlUnitOfWorkOptions();
            options.ConnectionString = connectionString;

            builder.AddTransient(typeof(SqlUnitOfWorkOptions), _ => options.Clone());

            builder.AddTransient(typeof(SqlUnitOfWork), typeof(SqlUnitOfWork));

            return builder;
        }

        /// <summary>
        ///     Registers services for inbox queues using Microsoft SQL Server.
        /// </summary>
        /// <param name="builder"> The service builder being configured. </param>
        /// <param name="configure"> The configuration callback used to configure inbox queue options. </param>
        /// <returns> The service builder being configured. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> or <paramref name="configure" /> is null. </exception>
        public static DomainServiceNodeBuilder UseSqlServerInboxQueue (this DomainServiceNodeBuilder builder, Action<SqlInboxQueueOptions> configure)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            builder.AddTransient(typeof(SqlInboxQueueOptions), _ =>
            {
                SqlInboxQueueOptions options = new SqlInboxQueueOptions();
                configure(options);
                return options;
            });

            builder.AddTransient(typeof(SqlInboxQueue), typeof(SqlInboxQueue));

            return builder;
        }

        /// <summary>
        ///     Registers services for inbox queues using Microsoft SQL Server.
        /// </summary>
        /// <param name="builder"> The service builder being configured. </param>
        /// <param name="options"> The used inbox queue options. </param>
        /// <returns> The service builder being configured. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> or <paramref name="options" /> is null. </exception>
        public static DomainServiceNodeBuilder UseSqlServerInboxQueue (this DomainServiceNodeBuilder builder, SqlInboxQueueOptions options)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            builder.AddTransient(typeof(SqlInboxQueueOptions), _ => options.Clone());

            builder.AddTransient(typeof(SqlInboxQueue), typeof(SqlInboxQueue));

            return builder;
        }

        /// <summary>
        ///     Registers services for inbox queues using Microsoft SQL Server.
        /// </summary>
        /// <param name="builder"> The service builder being configured. </param>
        /// <param name="connectionString"> The connection string used by the inbox queue. </param>
        /// <returns> The service builder being configured. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> or <paramref name="connectionString" /> is null. </exception>
        /// <exception cref="EmptyStringArgumentException"> <paramref name="connectionString" /> is empty or only whitespaces. </exception>
        public static DomainServiceNodeBuilder UseSqlServerInboxQueue (this DomainServiceNodeBuilder builder, string connectionString)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            if (connectionString.IsNullOrEmptyOrWhitespace())
            {
                throw new EmptyStringArgumentException(nameof(connectionString));
            }

            SqlInboxQueueOptions options = new SqlInboxQueueOptions();
            options.ConnectionString = connectionString;

            builder.AddTransient(typeof(SqlInboxQueueOptions), _ => options.Clone());

            builder.AddTransient(typeof(SqlInboxQueue), typeof(SqlInboxQueue));

            return builder;
        }

        /// <summary>
        ///     Registers services for using Microsoft SQL Server for all node domain services.
        /// </summary>
        /// <param name="builder"> The service builder being configured. </param>
        /// <param name="configure"> The configuration callback used to configure SQL Server options. </param>
        /// <returns> The service builder being configured. </returns>
        /// <remarks>
        ///<note type="note">
        /// <see cref="UseSqlServerNode(DomainServiceNodeBuilder,Action{SqlDomainServiceNodeOptions})"/> uses Microsoft SQL Server for all the following node domain services: repositories, inbox queues.
        /// If any of those requires its own configuration, configure the services individually (<see cref="UseSqlServerRepositories(DomainServiceNodeBuilder,Action{SqlUnitOfWorkOptions})"/>, <see cref="UseSqlServerInboxQueue(DomainServiceNodeBuilder,Action{SqlInboxQueueOptions})"/>).
        /// </note>
        /// </remarks>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> or <paramref name="configure" /> is null. </exception>
        public static DomainServiceNodeBuilder UseSqlServerNode (this DomainServiceNodeBuilder builder, Action<SqlDomainServiceNodeOptions> configure)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            builder.UseSqlServerRepositories(x =>
            {
                SqlDomainServiceNodeOptions options = new SqlDomainServiceNodeOptions();
                configure(options);
                options.UnitOfWork.CopyTo(x);
            });

            builder.UseSqlServerInboxQueue(x =>
            {
                SqlDomainServiceNodeOptions options = new SqlDomainServiceNodeOptions();
                configure(options);
                options.InboxQueue.CopyTo(x);
            });

            return builder;
        }

        /// <summary>
        ///     Registers services for using Microsoft SQL Server for all node domain services.
        /// </summary>
        /// <param name="builder"> The service builder being configured. </param>
        /// <param name="options"> The used SQL Server options. </param>
        /// <returns> The service builder being configured. </returns>
        /// <remarks>
        ///<note type="note">
        /// <see cref="UseSqlServerNode(DomainServiceNodeBuilder,SqlDomainServiceNodeOptions)"/> uses Microsoft SQL Server for all the following node domain services: repositories, inbox queues.
        /// If any of those requires its own configuration, configure the services individually (<see cref="UseSqlServerRepositories(DomainServiceNodeBuilder,SqlUnitOfWorkOptions)"/>, <see cref="UseSqlServerInboxQueue(DomainServiceNodeBuilder,SqlInboxQueueOptions)"/>).
        /// </note>
        /// </remarks>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> or <paramref name="options" /> is null. </exception>
        public static DomainServiceNodeBuilder UseSqlServerNode(this DomainServiceNodeBuilder builder, SqlDomainServiceNodeOptions options)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            builder.UseSqlServerRepositories(options.UnitOfWork);

            builder.UseSqlServerInboxQueue(options.InboxQueue);

            return builder;
        }

        /// <summary>
        ///     Registers services for using Microsoft SQL Server for all node domain services.
        /// </summary>
        /// <param name="builder"> The service builder being configured. </param>
        /// <param name="connectionString"> The used SQL Server connection string. </param>
        /// <returns> The service builder being configured. </returns>
        /// <remarks>
        ///<note type="note">
        /// <see cref="UseSqlServerNode(DomainServiceNodeBuilder,string)"/> uses Microsoft SQL Server for all the following node domain services: repositories, inbox queues.
        /// If any of those requires its own configuration, configure the services individually (<see cref="UseSqlServerRepositories(DomainServiceNodeBuilder,string)"/>, <see cref="UseSqlServerInboxQueue(DomainServiceNodeBuilder,string)"/>).
        /// </note>
        /// </remarks>
        /// <exception cref="ArgumentNullException"> <paramref name="builder" /> or <paramref name="connectionString" /> is null. </exception>
        /// <exception cref="EmptyStringArgumentException"> <paramref name="connectionString" /> is empty or only whitespaces. </exception>
        public static DomainServiceNodeBuilder UseSqlServerNode(this DomainServiceNodeBuilder builder, string connectionString)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            if (connectionString.IsNullOrEmptyOrWhitespace())
            {
                throw new EmptyStringArgumentException(nameof(connectionString));
            }

            SqlDomainServiceNodeOptions options = new SqlDomainServiceNodeOptions();
            options.UnitOfWork.ConnectionString = connectionString;
            options.InboxQueue.ConnectionString = connectionString;

            builder.UseSqlServerRepositories(options.UnitOfWork);

            builder.UseSqlServerInboxQueue(options.InboxQueue);

            return builder;
        }

        #endregion
    }
}
