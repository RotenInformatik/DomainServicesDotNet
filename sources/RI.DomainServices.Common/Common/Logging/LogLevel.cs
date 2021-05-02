using System;




namespace RI.DomainServices.Common.Logging
{
    /// <summary>
    /// Indicates the level of a log message.
    /// </summary>
    [Serializable]
    public enum LogLevel
    {
        /// <summary>
        /// Debug.
        /// </summary>
        Debug = 0,

        /// <summary>
        /// Information.
        /// </summary>
        Information = 1,

        /// <summary>
        /// Warning.
        /// </summary>
        Warning = 2,

        /// <summary>
        /// Recoverable error.
        /// </summary>
        Error = 3,

        /// <summary>
        /// Unrecoverable error.
        /// </summary>
        Fatal = 4,
    }
}
