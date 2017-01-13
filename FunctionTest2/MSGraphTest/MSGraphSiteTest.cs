using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using TestFramework;

namespace MSGraphTest
{
    /// <summary>
    /// Site Test class for Microsoft Graph
    /// </summary>
    [TestClass]
    public class MSGraphSiteTest
    {
        #region Additional test attributes
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            GraphBrowser.Initialize();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            GraphBrowser.Close();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            GraphBrowser.Goto(GraphBrowser.BaseAddress);
            GraphBrowser.SetWindowSize(0, 0, true);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            GraphBrowser.Goto(GraphBrowser.BaseAddress);
        }
        #endregion

        /// <summary>
        /// Verify whether Graph explorer neutral URL redirects to language specific page
        /// </summary>
        [TestMethod]
        [Owner("jishao")]
        public void BVT_Graph_S06_TC02_CanGoToLanguageSpecificExplorerPage()
        {
            //get the current language
            string prefix = GraphUtility.RemoveRedundantPartsfromExtractBaseAddress();
            string locale = GraphUtility.GetLocaleFromUrl(prefix);

            //Find substring of base address ending before locale
            string addressWithoutLocale = prefix.Replace(locale, "/");

            //string homePageLanguage = GraphBrowser.Url.Replace(prefix, "");
            GraphBrowser.Goto(addressWithoutLocale+"/graph-explorer");
            string currentUrl = GraphBrowser.Url;
            if (prefix.StartsWith("http:"))
            {
                currentUrl = currentUrl.Replace("https","http");
            }
            string explorerLanguage = GraphUtility.GetLocaleFromUrl(currentUrl);
            Assert.AreEqual(locale,
                explorerLanguage,
                "Graph explorer neutral URL should redirect to language specific page");
        }
    }
}
