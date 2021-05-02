using System.Threading.Tasks;

using RI.DomainServices.Node.Objects;




namespace RI.DomainServices.Node.Inbox
{
    /// <summary>
    /// Dispatches integration and domain events to their respective handlers.
    /// </summary>
    public interface IInboxDispatcher
    {
        Task Dispatch (IEvent @event);
    }
}
