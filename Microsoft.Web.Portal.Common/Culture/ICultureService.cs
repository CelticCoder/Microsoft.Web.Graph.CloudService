using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Microsoft.Web.Portal.Common.Culture
{
    /// <summary>
    /// Interface for Culture Service
    /// </summary>
    public interface ICultureService
    {
        /// <summary>
        /// Returns the current culture
        /// e.g. en-us, ja-jp etc.
        /// </summary>
        string CurrentCulture { get;}

        /// <summary>
        /// Sets the culture in the current execution thread
        /// </summary>
        /// <param name="httpRequest"></param>
        void SetCurrentCulture(HttpRequest httpRequest);
    }
}
