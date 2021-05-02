using System;

using RI.DomainServices.Node.Objects;
using RI.Utilities.ObjectModel;




namespace RI.DomainServices.Node.Serialization
{
    /// <summary>
    ///     Envelope for serialized domain and integration events.
    /// </summary>
    /// <remarks>
    ///     <note type="security">
    ///         When logging <see cref="SerializedEvent"/>, be aware that it might contain sensitive information.
    ///     </note>
    /// </remarks>
    /// <threadsafety static="false" instance="false" />
    [Serializable,]
    public sealed class SerializedEvent : ICloneable<SerializedEvent>, ICloneable
    {
        #region Instance Constructor/Destructor

        /// <summary>
        ///     Creates a new instance of <see cref="SerializedEvent" />.
        /// </summary>
        public SerializedEvent () { }

        /// <summary>
        ///     Creates a new instance of <see cref="SerializedEvent" />.
        /// </summary>
        /// <param name="type"> The type information of the serialized <see cref="IEvent" /> implementation. </param>
        /// <param name="data"> The serialized <see cref="IEvent" /> implementation. </param>
        public SerializedEvent (string type, string data)
        {
            this.Type = type;
            this.Data = data;
        }

        #endregion




        #region Instance Properties/Indexer

        /// <summary>
        ///     Gets or sets the serialized <see cref="IEvent" /> implementation.
        /// </summary>
        /// <value>
        ///     The serialized <see cref="IEvent" /> implementation.
        /// </value>
        /// <remarks>
        ///     <note type="security">
        ///         When logging <see cref="Data"/>, be aware that it might contain sensitive information.
        ///     </note>
        /// </remarks>
        public string Data { get; set; }

        /// <summary>
        ///     Gets or sets the type information of the serialized <see cref="IEvent" /> implementation.
        /// </summary>
        /// <value>
        ///     The type information of the serialized <see cref="IEvent" /> implementation.
        /// </value>
        public string Type { get; set; }

        #endregion




        #region Interface: ICloneable<SerializedEvent>

        /// <inheritdoc />
        public SerializedEvent Clone ()
        {
            return new SerializedEvent(this.Type, this.Data);
        }

        /// <inheritdoc />
        object ICloneable.Clone ()
        {
            return this.Clone();
        }

        /// <inheritdoc />
        public override string ToString() => $"{nameof(SerializedEvent)}; Type={this.Type ?? "[null]"}; Data={this.Data ?? "[null]"}";

        #endregion
    }
}
