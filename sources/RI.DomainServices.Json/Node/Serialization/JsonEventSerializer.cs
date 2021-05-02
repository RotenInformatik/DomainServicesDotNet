using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

using RI.DomainServices.Node.Objects;
using RI.Utilities.Text;




namespace RI.DomainServices.Node.Serialization
{
    /// <summary>
    ///     Integration and domain event serializer/deserializer which uses JSON.
    /// </summary>
    /// <threadsafety static="true" instance="true" />
    public sealed class JsonEventSerializer : IEventSerializer
    {
        #region Instance Constructor/Destructor

        /// <summary>
        ///     Creates a new instance of <see cref="JsonEventSerializer" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Default JSON serializer options are used (<see cref="Options" /> is null).
        ///     </para>
        /// </remarks>
        public JsonEventSerializer ()
            : this(null) { }

        /// <summary>
        ///     Creates a new instance of <see cref="JsonEventSerializer" />.
        /// </summary>
        /// <param name="options"> The JSON serializer options to use. Can be null to use default options. </param>
        public JsonEventSerializer (JsonSerializerOptions options)
        {
            this.Options = options;
        }

        #endregion




        #region Instance Properties/Indexer

        /// <summary>
        ///     Gets the used JSON serializer options.
        /// </summary>
        /// <value>
        /// The used JSON serializer options or null if default options are used.
        /// </value>
        public JsonSerializerOptions Options { get; }

        #endregion




        #region Interface: IEventSerializer

        private Dictionary<string, Type> TypeCache { get; } = new Dictionary<string, Type>(StringComparer.InvariantCultureIgnoreCase);

        private Type GetTypeFromName (string name)
        {
            if (this.TypeCache.ContainsKey(name))
            {
                return this.TypeCache[name];
            }

            Type type = AppDomain.CurrentDomain
                                 .GetAssemblies()
                                 .Select(x => x.GetType(name, false))
                                 .FirstOrDefault(x => x != null);

            if (type != null)
            {
                this.TypeCache.Add(name, type);
            }

            return type;
        }

        /// <inheritdoc />
        public Task<IEvent> Deserialize (SerializedEvent @event)
        {
            if (@event == null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            if (@event.Type.IsNullOrEmptyOrWhitespace())
            {
                throw new ArgumentException("Serialized event type is null or empty.", nameof(@event));
            }

            if (@event.Data.IsNullOrEmptyOrWhitespace())
            {
                throw new ArgumentException("Serialized event data is null or empty.", nameof(@event));
            }

            Type type = this.GetTypeFromName(@event.Type);

            if (type == null)
            {
                throw new SerializationException("The event cannot be deserialized. Type not found: " + @event.Type);
            }

            object instance;

            try
            {
                instance = JsonSerializer.Deserialize(@event.Data, type, this.Options);
            }
            catch (Exception exception)
            {
                throw new SerializationException("The event cannot be deserialized. Exception occurred: " + exception.Message, exception);
            }

            if (instance is IEvent result)
            {
                return Task.FromResult(result);
            }

            throw new SerializationException("The event cannot be deserialized. Event is not of type " + nameof(IEvent) + ": " + instance.GetType()
                                                                                                                                         .FullName);
        }

        /// <inheritdoc />
        public Task<SerializedEvent> Serialize (IEvent @event)
        {
            if (@event == null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            try
            {
                return Task.FromResult(new SerializedEvent(@event.GetType()
                                                                 .Name, JsonSerializer.Serialize(@event, this.Options)));
            }
            catch (Exception exception)
            {
                throw new SerializationException("The event cannot be serialized. Exception occurred: " + exception.Message, exception);
            }
        }

        #endregion
    }
}
