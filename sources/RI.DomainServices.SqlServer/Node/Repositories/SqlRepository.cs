using System;

using RI.DomainServices.Node.Objects;




namespace RI.DomainServices.Node.Repositories
{
    /// <summary>
    ///     Base implementation of <see cref="IRepository{TRoot,TUnitOfWork}" /> for repositories using Microsoft SQL Server.
    /// </summary>
    /// <typeparam name="TRoot"> The domain aggregate root type this repository deals with. </typeparam>
    /// <remarks>
    ///     <note type="important">
    ///         <see cref="SqlRepository{TRoot}" /> is intended to be used with constructor dependency injection.
    ///     </note>
    /// </remarks>
    /// <threadsafety static="false" instance="false" />
    public abstract class SqlRepository <TRoot> : RepositoryBase<TRoot, SqlUnitOfWork>
        where TRoot : class, IAggregateRoot
    {
        #region Instance Constructor/Destructor

        /// <summary>
        ///     Creates a new instance of <see cref="SqlRepository{TRoot}" />.
        /// </summary>
        /// <param name="unitOfWork"> An instance of the unit-of-work this repository uses (<see cref="SqlUnitOfWork" />). </param>
        /// <remarks>
        ///     <note type="important">
        ///         <see cref="SqlRepository{TRoot}" /> is intended to be used with constructor dependency injection.
        ///     </note>
        /// </remarks>
        /// <exception cref="ArgumentNullException"> <paramref name="unitOfWork" /> is null. </exception>
        protected SqlRepository (SqlUnitOfWork unitOfWork) : base(unitOfWork) { }

        #endregion
    }
}
