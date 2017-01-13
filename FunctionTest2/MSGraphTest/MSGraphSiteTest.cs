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
        /// Verify whether robots.txt specifies the site is accessible.
        /// Commented out for new graph architecture, since swapping slots (which could affect robots) is no longer a practice
        /// </summary>
        /*
        [TestMethod]
        [Owner("jishao")]
        public void BVT_Graph_S06_TC01_CanAccessSiteRobots()
        {
            string prefix = GraphUtility.RemoveRedundantPartsfromExtractBaseAddress();
            string url = prefix + "/robots.txt";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            // Define a cache policy for this request only. 
            HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Reload);
            request.CachePolicy = noCachePolicy;
            WebResponse response = request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream);
            string readResponse = readStream.ReadToEnd();
            bool disallowed = readResponse.Contains("Disallow:");
            bool allowed = readResponse.Contains("Allow:");
            if (GraphBrowser.BaseAddress.Contains("graph.microsoft.io"))
            {
                //Production site
                Assert.IsTrue(!disallowed && allowed, "The site should be allowed to access by search engines.");
            }
            else
            {
                Assert.IsTrue(disallowed, "The site {0} should not be allowed to accessby search engines", prefix);
            }
        }
        */

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


        /// <summary>
        /// Check whether robots.txt is correctly set for the major slots.
        /// Commented out for new architecture because there is no concern about swapping slots affecting robots
        /// </summary>
        /*
        [TestMethod]
        [Owner("jishao")]
        public void Acceptance_Graph_S06_TC03_VerifySiteRobotsForMajorSlots()
        {
            Array values = Enum.GetValues(typeof(DeploymentSlot));
            
            foreach (DeploymentSlot value in values)
            {
                string url = EnumExtension.GetDescription(value);
               
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "/robots.txt");
                request.Method = "GET";

                // Define a cache policy for this request only. 
                HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Reload);
                request.CachePolicy = noCachePolicy;
                WebResponse response = request.GetResponse();
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream);
                string readResponse = readStream.ReadToEnd();
                bool disallowed = readResponse.Contains("Disallow:");
                bool allowed = readResponse.Contains("Allow:");
                if (url.Contains("graph.microsoft.io"))
                {
                    //Production site
                    Assert.IsTrue(!disallowed && allowed, "The production site {0} should be allowed to access by search engines.", url);
                }
                else
                {
                    Assert.IsTrue(disallowed, "The site {0} should not be allowed to accessby search engines.", url);
                }
            }
        }
        */
    }
}
