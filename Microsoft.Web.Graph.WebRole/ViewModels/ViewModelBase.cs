using System.Web;

namespace Microsoft.Web.Graph.WebRole.ViewModels
{
    /// <summary>
    /// Base class for all the View models, all view models must be dervied from here
    /// </summary>
    public class ViewModelBase
    {
        /// <summary>
        /// context of the current request
        /// </summary>
        HttpContext _context = null;

        /// <summary>
        /// retruns true if current request is from Test environment false otherwise
        /// 
        /// Note:
        /// tbd: it should be coming from an Environment class
        /// </summary>
        private bool IsTestEnvironment
        {
            get
            {
                return (_context.Request.Url.Host.Contains("localhost") || _context.Request.RawUrl.Contains("devx.microsoft-tst.com"));
            }
        }

        /// <summary>
        /// Constrs the ViewModelBase class
        /// </summary>
        /// <param name="context">the http context of the currnt request</param>
        public ViewModelBase(HttpContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Page Title
        /// </summary>
        public string PageTitle { get; set; }

        /// <summary>
        /// returns the optimizlyUrl
        /// </summary>
        public string OptimizelyUrl
        {
            get
            {
                return IsTestEnvironment ? "https://cdn.optimizely.com/js/5703400067.js" : "https://cdn.optimizely.com/js/5657941311.js";
            }
        }

        /// <summary>
        /// returns the Appinsight instrumentation key
        /// </summary>
        public string AppInsightInstrumentationKey
        {
            get
            {
                return IsTestEnvironment ? "971fd8f4-65c9-44c6-90a9-070a98f1722e" : "0c5857de-44ee-44df-ab2d-3848812279b8";
            }
        }

        /// <summary>
        /// Google Analytics Key
        /// </summary>
        public string GoogleAnalyticsKey
        {
            get
            {
                return IsTestEnvironment ? "UA-69973107-1" : "UA-66884364-1";
            }
        }

        /// <summary>
        /// Cache control
        /// </summary>
        public string CacheControl
        {
            get
            {
                return IsTestEnvironment ? "no-cache" : "no-cache";// keeping caching disabled for all environments until we need cachings
            }
        }

        /// <summary>
        /// Pragma
        /// </summary>
        public string Pragma
        {
            get
            {
                return IsTestEnvironment ? "NO-CACHE" : "NO-CACHE";// keeping caching disabled for all environments until we need cachings
            }
        }
    }
}