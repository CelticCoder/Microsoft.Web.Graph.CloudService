//------------------------------------------------------------------------------
// <copyright file="ICultureService.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
//     Developed by patrickp Office Developer Experience Engineering Team 
// </copyright>
// <summary>
//      Interface definition for the Culture Service
// </summary>
//------------------------------------------------------------------------------

namespace Microsoft.OfficeDevPortals.Shared.Culture
{
    using System.Web;

    /// <summary>
    /// Interface for Culture Service
    /// </summary>
    public interface ICultureService
    {
        /// <summary>
        /// Gets the current culture
        /// </summary>
        string CurrentCulture { get; }

        /// <summary>
        /// Sets the culture in the current execution thread
        /// </summary>
        /// <param name="httpRequest"> setting the httpRequest object</param>
        void SetCurrentCulture(HttpRequest httpRequest);
    }
}
