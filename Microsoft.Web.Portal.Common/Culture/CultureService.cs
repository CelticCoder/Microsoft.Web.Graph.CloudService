using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.OfficeDevPortals.Shared.Culture;

namespace Microsoft.Web.Portal.Common.Culture
{
    public class CultureService : ICultureService
    {
        /// <summary>
        /// Supported culture by current service
        /// </summary>
        private readonly string _supportCultures = null;

        /// <summary>
        /// Default culture in case unsupported culture is requested
        /// </summary>
        private readonly string _defaultCulture = null;

        /// <summary>
        /// Current culture
        /// </summary>
        public string CurrentCulture
        {
            get
            {
                return Thread.CurrentThread.CurrentUICulture.Name;
            }
            private set
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="defaultCulture"></param>
        /// <param name="supportedCulture"></param>
        public CultureService(string defaultCulture, string supportedCulture)
        {
            _defaultCulture = defaultCulture;
            _supportCultures = supportedCulture;
        }

        /// <summary>
        /// Sets the current culture based on current Http Request
        /// </summary>
        /// <param name="httpRequest">current http request</param>
        public void SetCurrentCulture(HttpRequest httpRequest)
        {
            string culture = _defaultCulture;
            Match mt = Regex.Match(httpRequest.Url.AbsolutePath, _supportCultures, RegexOptions.IgnoreCase);
            if (mt.Length > 0)
            {
                culture = mt.Value.Trim(new char[] { '/', '-' }).ToLowerInvariant();
            }
            CurrentCulture = culture;
        }

    }
}
