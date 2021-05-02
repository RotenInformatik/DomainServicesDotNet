using System;

using RI.DomainServices.Common.Builder;




namespace RI.DomainServices.Broker.Builder
{
    /// <summary>
    ///     Domain service builder used to configure and register all necessary types for using domain services as a broker.
    /// </summary>
    /// <remarks>
    ///     <note type="important">
    ///         Domain services are only added to the used composition container and usable after <see cref="Build" /> is called.
    ///     </note>
    /// </remarks>
    /// <threadsafety static="false" instance="false" />
    public sealed class DomainServiceBrokerBuilder : DomainServiceBuilder
    {
        #region Interface: IDomainServiceBrokerBuilder

        /// <inheritdoc />
        public override void Build ()
        {
            this.ThrowIfAlreadyBuilt();

            //TODO: Log (INFO)

            throw new NotImplementedException();
        }

        #endregion
    }
}
