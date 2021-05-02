using System;
using System.Threading.Tasks;

using RI.DomainServices.Node.Objects;




namespace RI.DomainServices.Node.Repositories
{
    /// <summary>
    ///     Base implementation of <see cref="IRepository{TRoot,TUnitOfWork}" />.
    /// </summary>
    /// <typeparam name="TRoot"> The domain aggregate root type this repository deals with. </typeparam>
    /// <typeparam name="TUnitOfWork"> The unit-of-work type this repository uses. </typeparam>
    /// <remarks>
    ///     <note type="important">
    ///         <see cref="RepositoryBase{TRoot,TUnitOfWork}" /> is intended to be used with constructor dependency injection.
    ///     </note>
    /// </remarks>
    /// <threadsafety static="false" instance="false" />
    public abstract class RepositoryBase <TRoot, TUnitOfWork> : IRepository<TRoot, TUnitOfWork>
        where TRoot : class, IAggregateRoot
        where TUnitOfWork : class, IUnitOfWork
    {
        #region Instance Constructor/Destructor

        /// <summary>
        ///     Creates a new instance of <see cref="RepositoryBase{TRoot,TUnitOfWork}" />.
        /// </summary>
        /// <param name="unitOfWork"> An instance of the unit-of-work this repository uses (<typeparamref name="TUnitOfWork"/>). </param>
        /// <remarks>
        ///     <note type="important">
        ///         <see cref="RepositoryBase{TRoot,TUnitOfWork}" /> is intended to be used with constructor dependency injection.
        ///     </note>
        /// </remarks>
        /// <exception cref="ArgumentNullException"> <paramref name="unitOfWork" /> is null. </exception>
        protected RepositoryBase (TUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            this.UnitOfWork = unitOfWork;
        }

        /// <summary>
        ///     Finalizes this instance of <see cref="RepositoryBase{TRoot,TUnitOfWork}" />.
        /// </summary>
        ~RepositoryBase ()
        {
            this.Dispose(false);
        }

        #endregion




        #region Virtuals

        /// <inheritdoc cref="IDisposable.Dispose" />
        protected virtual void Dispose (bool disposing)
        {
            this.UnitOfWork?.Dispose();
        }

        #endregion




        #region Interface: IRepository<TRoot,TUnitOfWork>

        /// <inheritdoc />
        public TUnitOfWork UnitOfWork { get; }

        /// <inheritdoc />
        IUnitOfWork IRepository<TRoot>.UnitOfWork => this.UnitOfWork;

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

        #endregion
    }
}
