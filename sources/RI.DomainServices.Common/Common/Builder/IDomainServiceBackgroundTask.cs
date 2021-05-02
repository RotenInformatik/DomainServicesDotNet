using System.Threading;
using System.Threading.Tasks;




namespace RI.DomainServices.Common.Builder
{
    /// <summary>
    /// Represents a single domain service background task.
    /// </summary>
    /// <remarks>
    ///<para>
    ///A domain service background task encapsulates actions which which are run in the background after the domain services are built (after <see cref="DomainServiceBuilder.Build"/>).
    /// </para>
    ///<para>
    ///Domain service background tasks are set up and run by a <see cref="IDomainServiceBackgroundTaskManager"/> implementation.
    /// </para>
    /// </remarks>
    public interface IDomainServiceBackgroundTask
    {
        /// <summary>
        /// Called by the <see cref="IDomainServiceBackgroundTaskManager"/> to create a background task.
        /// </summary>
        /// <param name="builder">The used domain service builder.</param>
        /// <param name="ct">The cancellation token provided by the runtime environment to stop background tasks.</param>
        /// <returns>The task which either starts executing or is already finished (e.g. <see cref="Task.CompletedTask"/>) if no background task is necessary.</returns>
        Task Run (DomainServiceBuilder builder, CancellationToken ct);
    }
}
