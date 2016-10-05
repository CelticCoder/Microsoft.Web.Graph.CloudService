﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Web.Portal.Common.Logging
{
    /// <summary>
    /// ILogger Interface
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs the message
        /// </summary>
        /// <param name="level">log level</param>
        /// <param name="message">message to log</param>
        void Log(LogLevel entry, string message);
    }
}
