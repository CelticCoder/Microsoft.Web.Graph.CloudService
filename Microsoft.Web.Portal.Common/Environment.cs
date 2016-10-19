using System;
using System.Data.Common;


namespace Microsoft.Web.Portal.Common
{
    /// <summary>
    /// Class representing Runt time environment configuration
    /// </summary>
    public class Environment
    {

        /// <summary>
        /// Utility method that reads the web/appconfig file and gets the value for the specified key
        /// if that key not found, ti returns the default value stated for  that key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="anyDefaultValue"></param>
        /// <returns></returns>
        private static string GetValue(string key, string anyDefaultValue = "")
        {
            string result = anyDefaultValue; // Default Value
            // to be very safe, because if sth goes wrong
            try
            {
                result = System.Configuration.ConfigurationManager.AppSettings[key];
                if (result == null)
                {
                    result = anyDefaultValue; // Default Value
                }
            }
            catch (Exception)
            {
                result = anyDefaultValue; // Default Value
            }
            return result;
        }

        #region Application Name

        /// <summary>
        /// ApplicationName
        /// </summary>
        private string _applicationName;

        /// <summary>
        /// Name for currently running application
        /// </summary>
        public string ApplicationName
        {
            get
            {
                if (_applicationName == null)
                {
                    _applicationName = GetValue("Microsoft.Web.Portal.Common.Environment.ApplicationName", "Warning.Applicaiton.Name.Missing");
                }
                return _applicationName;
            }
        }
        #endregion

        #region Default Culture
        private string _defaultCulture;

        public string DefaultCulture
        {
            get
            {
                if (_defaultCulture == null)
                {
                    _defaultCulture = GetValue("Microsoft.Web.Portal.DefaultCulture", "en-us");
                }
                return _defaultCulture;
            }
        }
        #endregion

        #region Supported Culture
        private string _supportedCultures;

        public string SupportedCultures
        {
            get
            {
                if (_supportedCultures == null)
                {
                    _supportedCultures = GetValue("Microsoft.Web.Portal.SupportedCulture", "[/-]en-us/?");
                }
                return _supportedCultures;
            }
        }
        #endregion


    }
}