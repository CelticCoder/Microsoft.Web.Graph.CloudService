//------------------------------------------------------------------------------
// <copyright file="ILogger.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
//     Developed by patrickp Office Developer Experience Engineering Team 
// </copyright>
// <summary>
//      Interface definition for the Logging Service
// </summary>
//------------------------------------------------------------------------------
namespace Microsoft.OfficeDevPortals.Shared.Logging
{ 
    /// <summary>
    /// Log level enumeration
    /// </summary>
    public enum LogLevel
        {
            /// <summary>
            /// Debugging logging level
            /// </summary>
            Debug,

            /// <summary>
            /// Information logging level
            /// </summary>
            Information,

            /// <summary>
            /// Warning logging level
            /// </summary>
            Warning,

            /// <summary>
            /// Error logging level
            /// </summary>
            Error,

            /// <summary>
            /// Fatal logging level
            /// </summary>
            Fatal
        }
    
    /// <summary>
    /// ILogger Interface
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs the message
        /// </summary>
        /// <param name="entry">log level</param>
        /// <param name="message">message to log</param>
        void Log(LogLevel entry, string message);
    }
}
