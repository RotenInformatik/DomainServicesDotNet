using System.Collections.Generic;




namespace RI.DomainServices.Common.Builder
{
    /// <summary>
    /// Manages domain service background tasks.
    /// </summary>
    /// <remarks>
    ///<para>
    ///A domain service background task manager abstracts the runtime environments way of setting up background tasks (<see cref="IDomainServiceBackgroundTask"/>).
    /// </para>
    /// </remarks>
    public interface IDomainServiceBackgroundTaskManager
    {
        /// <summary>
        /// Sets up background tasks.
        /// </summary>
        /// <param name="tasks">The sequence of background tasks to set up and run.</param>
        void SetupAndRun(IEnumerable<IDomainServiceBackgroundTask> tasks);
    }
}
