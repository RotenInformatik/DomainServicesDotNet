using System;

using RI.DomainServices.Node.Objects;




namespace RI.DomainServices.Node.Repositories
{
    /// <summary>
    ///     Domain repository (domain layer contract).
    /// </summary>
    /// <typeparam name="TRoot"> The domain aggregate root type this repository deals with. </typeparam>
    /// <remarks>
    ///     <para>
    ///         In the domain layer, <see cref="IRepository{TRoot}" /> should be used instead of <see cref="IRepository{TRoot,TUnitOfWork}" /> in order to hide the concrete <see cref="IUnitOfWork" /> implementation (defined in the infrastructure layer) from the domain layer.
    ///     </para>
    ///     <para>
    ///         Typically, the repository contracts (interfaces) in the domain layer inherit <see cref="IRepository{TRoot}" />.
    ///     </para>
    ///     <note type="important">
    ///         In the concrete repository implementation, in the infrastructure layer, the implementation of <see cref="IRepository{TRoot}" /> is not enough, <see cref="IRepository{TRoot,TUnitOfWork}" /> must also be implemented to be recognized as a repository.
    ///     </note>
    ///     <note type="implement">
    ///         <see cref="IDisposable.Dispose" /> and <see cref="IAsyncDisposable.DisposeAsync" /> should also dispose <see cref="UnitOfWork" />.
    ///     </note>
    /// </remarks>
    public interface IRepository <TRoot> : IDisposable, IAsyncDisposable
        where TRoot : class, IAggregateRoot
    {
        /// <summary>
        ///     Gets the unit-of-work used by the repository.
        /// </summary>
        /// <value>
        ///     The unit-of-work used by the repository.
        /// </value>
        /// <remarks>
        ///     <note type="implement">
        ///         This property must not be null.
        ///     </note>
        /// </remarks>
        IUnitOfWork UnitOfWork { get; }
    }

    /// <summary>
    ///     Domain repository (infrastructure layer contract).
    /// </summary>
    /// <typeparam name="TRoot"> The domain aggregate root type this repository deals with. </typeparam>
    /// <typeparam name="TUnitOfWork"> The unit-of-work type this repository uses. </typeparam>
    /// <remarks>
    ///     <para>
    ///         In the domain layer, <see cref="IRepository{TRoot}" /> should be used instead of <see cref="IRepository{TRoot,TUnitOfWork}" /> in order to hide the concrete <see cref="IUnitOfWork" /> implementation (defined in the infrastructure layer) from the domain layer.
    ///     </para>
    ///     <para>
    ///         Typically, the repository implementations in the infrastructure layer implement <see cref="IRepository{TRoot,TUnitOfWork}" />.
    ///     </para>
    ///     <para>
    ///         Repository implementations can inherit from <see cref="RepositoryBase{TRoot,TUnitOfWork}" /> to implement basic boilerplate.
    ///     </para>
    ///     <note type="important">
    ///         In the concrete repository implementation, in the infrastructure layer, the implementation of <see cref="IRepository{TRoot}" /> is not enough, <see cref="IRepository{TRoot,TUnitOfWork}" /> must also be implemented to be recognized as a repository.
    ///     </note>
    ///     <note type="implement">
    ///         <see cref="IDisposable.Dispose" /> and <see cref="IAsyncDisposable.DisposeAsync" /> should also dispose <see cref="UnitOfWork" />.
    ///     </note>
    /// </remarks>
    public interface IRepository <TRoot, out TUnitOfWork> : IRepository<TRoot>
        where TRoot : class, IAggregateRoot
        where TUnitOfWork : class, IUnitOfWork
    {
        /// <summary>
        ///     Gets the unit-of-work used by the repository.
        /// </summary>
        /// <value>
        ///     The unit-of-work used by the repository.
        /// </value>
        /// <remarks>
        ///     <note type="implement">
        ///         This property must not be null.
        ///     </note>
        /// </remarks>
        new TUnitOfWork UnitOfWork { get; }
    }
}
