using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using RI.DomainServices.Common.Logging;
using RI.DomainServices.Node.Objects;
using RI.DomainServices.Node.Serialization;
using RI.DomainServices.Node.Transactions;




namespace RI.DomainServices.Node.Repositories
{
    /// <summary>
    ///     Base implementation of <see cref="IUnitOfWork" />.
    /// </summary>
    /// <remarks>
    ///     <note type="important">
    ///         <see cref="UnitOfWorkBase" /> is intended to be used with constructor dependency injection.
    ///     </note>
    /// </remarks>
    /// <threadsafety static="false" instance="false" />
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        #region Instance Constructor/Destructor

        /// <summary>
        ///     Creates a new instance of <see cref="UnitOfWorkBase" />.
        /// </summary>
        /// <param name="eventSerializer"> The used integration and domain event serializer/deserializer (<see cref="IEventSerializer"/>). </param>
        /// <param name="logger"> The logger the unit-of-work can use for logging.</param>
        /// <param name="serviceProvider"> The service provider the unit-of-work can use to resolve additional dependencies dynamically (<see cref="IServiceProvider"/>). </param>
        /// <remarks>
        ///     <note type="important">
        ///         <see cref="UnitOfWorkBase" /> is intended to be used with constructor dependency injection.
        ///     </note>
        /// </remarks>
        /// <exception cref="ArgumentNullException"> <paramref name="eventSerializer" />, <paramref name="logger"/>, or <paramref name="serviceProvider" /> is null. </exception>
        protected UnitOfWorkBase (IEventSerializer eventSerializer, ILogSink logger, IServiceProvider serviceProvider)
        {
            if (eventSerializer == null)
            {
                throw new ArgumentNullException(nameof(eventSerializer));
            }

            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            this.EventSerializer = eventSerializer;
            this.Logger = logger;
            this.ServiceProvider = serviceProvider;
        }

        /// <summary>
        ///     Finalizes this instance of <see cref="UnitOfWorkBase" />.
        /// </summary>
        ~UnitOfWorkBase ()
        {
            this.Dispose(false);
        }

        #endregion




        #region Instance Properties/Indexer

        /// <summary>
        ///     Gets the used event serializer/deserializer.
        /// </summary>
        /// <value>
        /// The used event serializer/deserializer.
        /// </value>
        protected IEventSerializer EventSerializer { get; }

        /// <summary>
        ///     Gets the logger the unit-of-work can use for logging.
        /// </summary>
        /// <value>
        /// The logger the unit-of-work can use for logging.
        /// </value>
        protected ILogSink Logger { get; }

        /// <inheritdoc cref="IUnitOfWork.PublishedEvents" />
        protected List<IEvent> PublishedEvents { get; } = new List<IEvent>();

        /// <summary>
        ///     Gets the service provider the unit-of-work can use to resolve additional dependencies dynamically.
        /// </summary>
        /// <value>
        /// The service provider the unit-of-work can use to resolve additional dependencies dynamically.
        /// </value>
        protected IServiceProvider ServiceProvider { get; }

        #endregion




        #region Virtuals

        /// <inheritdoc cref="IDisposable.Dispose" />
        protected virtual void Dispose (bool disposing)
        {
            this.Rollback();
        }

        #endregion




        #region Interface: IUnitOfWork

        /// <inheritdoc />
        IReadOnlyList<IEvent> IUnitOfWork.PublishedEvents => this.PublishedEvents;

        /// <inheritdoc />
        public abstract Task Fail(Exception exception);

        /// <inheritdoc />
        public TransactionState State { get; protected set; } = TransactionState.NotStarted;

        /// <inheritdoc />
        public abstract Task Begin ();

        /// <inheritdoc />
        public abstract Task Commit ();

        /// <inheritdoc />
        void IDisposable.Dispose ()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc />
        ValueTask IAsyncDisposable.DisposeAsync ()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
            return new ValueTask();
        }

        /// <inheritdoc />
        public abstract Task Publish (IEvent @event);

        /// <inheritdoc />
        public abstract Task Rollback ();

        #endregion
    }
}
