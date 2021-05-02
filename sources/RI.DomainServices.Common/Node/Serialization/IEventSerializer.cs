using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;

using RI.DomainServices.Node.Objects;




namespace RI.DomainServices.Node.Serialization
{
    /// <summary>
    ///     Serializes and deserializes domain and integration events for storage and transmission.
    /// </summary>
    public interface IEventSerializer
    {
        /// <summary>
        ///     Deserializes an event.
        /// </summary>
        /// <param name="event"> The event to deserialize. </param>
        /// <returns> The deserialized event. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="event" /> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="event" /> has invalid contents (e.g. strings are null or empty). </exception>
        /// <exception cref="SerializationException"> <paramref name="event" /> cannot be deserialized. </exception>
        Task<IEvent> Deserialize (SerializedEvent @event);

        /// <summary>
        ///     Serializes an event.
        /// </summary>
        /// <param name="event"> The event to serialize. </param>
        /// <returns> The serialized event. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="event" /> is null. </exception>
        /// <exception cref="SerializationException"> <paramref name="event" /> cannot be serialized. </exception>
        Task<SerializedEvent> Serialize (IEvent @event);
    }
}
