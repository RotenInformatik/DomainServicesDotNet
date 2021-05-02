namespace RI.DomainServices.Common.Builder
{
    /// <summary>
    /// Represents a single domain service builder task.
    /// </summary>
    /// <remarks>
    ///<para>
    ///A domain service builder task encapsulates actions which which are run when the domain services are built (during <see cref="DomainServiceBuilder.Build"/>).
    /// </para>
    ///<para>
    ///Domain service builder tasks are run directly by <see cref="DomainServiceBuilder"/>.
    /// </para>
    /// </remarks>
    public interface IDomainServiceBuilderTask
    {
        /// <summary>
        /// Called by the <see cref="DomainServiceBuilder"/> during <see cref="DomainServiceBuilder.Build"/>.
        /// </summary>
        /// <param name="builder">The used domain service builder.</param>
        void Build (DomainServiceBuilder builder);
    }
}
