using System;
using System.Collections.Generic;
using System.Linq;

using RI.DomainServices.Common.Builder;
using RI.DomainServices.Common.Composition;
using RI.DomainServices.Common.Logging;
using RI.DomainServices.Node.Inbox;
using RI.DomainServices.Node.Serialization;
using RI.Utilities.Collections;
using RI.Utilities.Text;




namespace RI.DomainServices.Node.Builder
{
    /// <summary>
    ///     Domain service builder used to configure and register all necessary types for using domain services as a node.
    /// </summary>
    /// <remarks>
    ///     <note type="important">
    ///         Domain services are only added to the used composition container and usable after <see cref="Build" /> is called.
    ///     </note>
    /// </remarks>
    /// <threadsafety static="false" instance="false" />
    public sealed class DomainServiceNodeBuilder : DomainServiceBuilder
    {
        #region Interface: IDomainServiceNodeBuilder

        /// <inheritdoc />
        public override void Build ()
        {
            this.ThrowIfAlreadyBuilt();

            try
            {
                this.AlreadyBuilt = true;

                this.ThrowIfNotExactContractCount(typeof(ILogSink), 1);
                //TODO: Add default null logger if none is available
                ILogSink logger = this.GetService<ILogSink>();
                logger.LogInformation("Building domain services");
                
                this.ThrowIfNotExactContractCount(typeof(DomainServiceBuilder), 0);
                this.ThrowIfNotExactContractCount(typeof(DomainServiceNodeBuilder), 0);
                
                this.AddBuildOnly(typeof(DomainServiceBuilder), this);
                this.AddBuildOnly(typeof(DomainServiceNodeBuilder), this);

                logger.LogInformation("Executing domain service builder tasks");
                IList<IDomainServiceBuilderTask> builderTasks = this.GetServices<IDomainServiceBuilderTask>();
                builderTasks.ForEach(x => x.Build(this));

                //Builder types
                this.ThrowIfNotMinContractCount(typeof(IRepositoryDiscoverer), 1);
                this.ThrowIfNotExactContractCount(typeof(ICompositionContainer), 1);
                this.ThrowIfNotMaxContractCount(typeof(IDomainServiceBackgroundTaskManager), 1);

                //Serialization types
                this.ThrowIfNotExactContractCount(typeof(IEventSerializer), 1);

                //Inbox types
                this.ThrowIfNotMaxContractCount(typeof(IInboxManager), 1);
                this.ThrowIfNotMaxContractCount(typeof(IInboxDispatcher), 1);
                this.ThrowIfNotExactContractCount(typeof(IInboxQueue), 1);

                //Messaging types

                //Default types
                //TODO: Add default inbox manager if none was registered + LOG
                //TODO: Add default inbox dispatcher if none was registered + LOG
                //TODO: Add default task manager if none was registered + LOG

                IList<IRepositoryDiscoverer> repositoryDiscoverers = this.GetServices<IRepositoryDiscoverer>();
                
                foreach (IRepositoryDiscoverer repositoryDiscoverer in repositoryDiscoverers)
                {
                    foreach (RepositoryDiscovery repositoryDiscovery in repositoryDiscoverer.DiscoverRepositoryTypes())
                    {
                        foreach (Type contract in repositoryDiscovery.Contracts)
                        {
                            this.AddTransient(contract, repositoryDiscovery.Implementation);
                        }
                    }
                }

                //TODO: Check that for the repositories, a matching UOW is available

                logger.LogInformation($"Registered domain services:{Environment.NewLine}{this.Registrations.Select(x => x.ToString()).Join(Environment.NewLine)}");

                ICompositionContainer compositionContainer = this.GetService<ICompositionContainer>();
                IDomainServiceBackgroundTaskManager backgroundTaskManager = this.GetService<IDomainServiceBackgroundTaskManager>();
                IList<IDomainServiceBackgroundTask> backgroundTasks = this.GetServices<IDomainServiceBackgroundTask>();

                this.RemoveContract(typeof(IDomainServiceBuilderTask));
                this.RemoveContract(typeof(IRepositoryDiscoverer));
                this.RemoveContract(typeof(ICompositionContainer));
                this.RemoveContract(typeof(IDomainServiceBackgroundTaskManager));
                this.RemoveContract(typeof(IDomainServiceBackgroundTask));
                this.RemoveBuildOnlyContracts();

                logger.LogInformation("Registering domain services with composition container");
                compositionContainer.Register(this.Registrations);

                logger.LogInformation("Setting up domain service background tasks");
                backgroundTaskManager.SetupAndRun(backgroundTasks);
            }

            catch (DomainServiceBuilderException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new DomainServiceBuilderException(exception);
            }
        }

        #endregion
    }
}
