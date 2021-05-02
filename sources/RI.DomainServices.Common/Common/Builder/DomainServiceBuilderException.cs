using System;
using System.Runtime.Serialization;




namespace RI.DomainServices.Common.Builder
{
    /// <summary>
    ///     The <see cref="DomainServiceBuilderException" /> is thrown when an <see cref="DomainServiceBuilder" /> could not successfully configure or register the necessary types.
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    [Serializable,]
    public class DomainServiceBuilderException : Exception
    {
        #region Constants

        private const string ExceptionMessageWithException = "Failed to configure and register domain services: {0}";

        private const string ExceptionMessageWithoutException = "Failed to configure and register domain services.";

        #endregion




        #region Instance Constructor/Destructor

        /// <summary>
        ///     Creates a new instance of <see cref="DomainServiceBuilderException" />.
        /// </summary>
        public DomainServiceBuilderException ()
            : base(DomainServiceBuilderException.ExceptionMessageWithoutException) { }

        /// <summary>
        ///     Creates a new instance of <see cref="DomainServiceBuilderException" />.
        /// </summary>
        /// <param name="message"> The message which describes the failure. </param>
        public DomainServiceBuilderException (string message)
            : base(message) { }

        /// <summary>
        ///     Creates a new instance of <see cref="DomainServiceBuilderException" />.
        /// </summary>
        /// <param name="innerException"> The exception which triggered this exception. </param>
        public DomainServiceBuilderException (Exception innerException)
            : base(string.Format(DomainServiceBuilderException.ExceptionMessageWithException, innerException.Message), innerException) { }

        /// <summary>
        ///     Creates a new instance of <see cref="DomainServiceBuilderException" />.
        /// </summary>
        /// <param name="message"> The message which describes the failure. </param>
        /// <param name="innerException"> The exception which triggered this exception. </param>
        public DomainServiceBuilderException (string message, Exception innerException)
            : base(message, innerException) { }

        /// <summary>
        ///     Creates a new instance of <see cref="DomainServiceBuilderException" />.
        /// </summary>
        /// <param name="info"> The serialization data. </param>
        /// <param name="context"> The type of the source of the serialization data. </param>
        protected DomainServiceBuilderException (SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        #endregion
    }
}
