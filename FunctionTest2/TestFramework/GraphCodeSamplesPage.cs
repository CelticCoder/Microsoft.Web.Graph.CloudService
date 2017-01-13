

namespace TestFramework
{
    using System.Linq;
    using System.Collections.Generic;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// Represents the Code Samples and SDKs page
    /// </summary>
    public class GraphCodeSamplesPage : GraphBasePage
    {
        /// <summary>
        /// The title of the Code Samples and SDKs page
        /// </summary>
        private static string _pageTitle = "Microsoft Graph - Samples & SDKs";

        [FindsBy(How = How.Id, Using = "body-content")]
        public IWebElement _bodyElement;

        /// <summary>
        /// Gets the title of the Code Samples and SDKs page
        /// </summary>
        public static string PageTitle
        {
            get
            {
                return _pageTitle;
            }
        }

        /// <summary>
        /// Main content of CS and SDK page
        /// </summary>
        public IWebElement BodyElement { get { return _bodyElement; } }
    }
}