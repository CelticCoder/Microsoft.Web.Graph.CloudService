using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFramework;
using System;
using TestFramework.Office365Page;

namespace MSGraphTest
{
    /// <summary>
    /// Test Class for Microsoft Get started page
    /// </summary>
    [TestClass]
    public class MSGraphGetstartedTest
    {
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

        [TestMethod]
        [Owner("ambate")]
        public void BVT_Graph_S03_TC01_CanChoosePlatform()
        {
            foreach (Platform item in Enum.GetValues(typeof(Platform)))
            {
                GraphPages.Office365Page.CardSetupPlatform.ChoosePlatform(item);
                Assert.IsTrue(GraphPages.Office365Page.CardSetupPlatform.IsShowingPlatformSetup(item), "Failed to choose platform {0}.", item.ToString());
            }
        }
        
        // should only show first 3 cards on initial page load with no querystring parameters
        // The 3 desired cards: intro, try-it-out, setup
        [TestMethod]
        [Owner("ambate")]
        public void BVT_Graph_S03_TC02_ShowTwoCardsByDefault()
        {
            GraphBrowser.Goto(GraphUtility.RemoveRedundantPartsfromExtractBaseAddress() + "/quick-start");
            Assert.IsTrue(GraphUtility.CheckTwoCardsDisplayed(), "Default cards in Getting Started are not displayed correctly.");
        }

        /// <summary>
        /// Since getting started now uses an external app registration, simulation of this process is the best testing option
        /// Fake app registration, then download Node.js rest sample
        /// </summary>
        [TestMethod]
        [Owner("ambate")]
        public void Acceptance_Graph_S03_TC09_CanDownloadCode_Node()
        {
            Platform platform = Platform.Node;
            //Simulate app registration via hardcoded querystring
            GraphBrowser.Goto(GraphUtility.RemoveRedundantPartsfromExtractBaseAddress() + "/quick-start?appID=c4664f74-aec4-4462-93e9-fb84a25d1f28&appName=My%20Node.js%20App&redirectUrl=http://localhost:3000/login&platform=option-node");
            Assert.IsTrue(GraphPages.Office365Page.CardSetupPlatform.IsShowingPlatformSetup(platform), "Failed to choose platform {0}, which should be picked from querystring and selected on page load", platform.ToString());

            GraphPages.Office365Page.CardDownloadCode.DownloadCode();
            Assert.IsTrue(GraphPages.Office365Page.CardDownloadCode.IsCodeDownloaded(), "Failed to download code and display post-download instructions.");
        }

        [TestMethod]
        [Owner("ambate")]
        public void BVT_Graph_S03_TC10_CanLoadGettingStartedPlatformImages()
        {
            //Platform platform = Platform.PHP;
            //GraphPages.Office365Page.CardSetupPlatform.ChoosePlatform(platform);
            GraphBrowser.Goto(GraphUtility.RemoveRedundantPartsfromExtractBaseAddress() + "/quick-start#setup");
            Assert.IsTrue(GraphPages.Office365Page.CanLoadImages());
        }
    }
}
