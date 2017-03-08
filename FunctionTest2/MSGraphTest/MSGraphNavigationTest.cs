using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFramework;

namespace MSGraphTest
{
    /// <summary>
    /// Test Class for Microsoft Graph site
    /// </summary>
    [TestClass]
    public class MSGraphNavigationTest
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

        /// <summary>
        /// Verify whether Get started page can be navigated to.
        /// </summary>
        [TestMethod]
        [Owner("ashirs")]
        public void BVT_Graph_S01_TC02_CanGoToGetstartedPage()
        {
            string title = GraphPages.Navigation.Select("Quick start", true);
            Assert.IsTrue(
                GraphPages.Navigation.IsAtGraphPage(title),
                @"The opened page should be {0}",
                title);
        }

        /// <summary>
        /// Verify whether Documentation page can be navigated to.
        /// </summary>
        [TestMethod]
        [Owner("ashirs")]
        public void BVT_Graph_S01_TC03_CanGoToDocumentationPage()
        {
            string title = GraphPages.Navigation.Select("Documentation");
            Assert.IsTrue(
                GraphPages.Navigation.IsAtGraphPage(title),
                @"The opened page should be {0}",
                title);
        }

        /// <summary>
        /// Verify whether Graph explorer page can be navigated to.
        /// </summary>
        [TestMethod]
        [Owner("ashirs")]
        public void BVT_Graph_S01_TC04_CanGoToGraphExplorerPage()
        {
            string title = TestHelper.VerifyAndSelectExplorerOnNavBar();
            Assert.IsTrue(
                GraphPages.Navigation.IsAtGraphPage(title),
                @"The opened page should be ""Graph explorer""");
        }

        /// <summary>
        /// Verify whether Samples and SDKs page can be navigated to.
        /// </summary>
        [TestMethod]
        [Owner("ashirs")]
        public void BVT_Graph_S01_TC05_CanGoToSamplesAndSDKsPage()
        {
            string title = GraphPages.Navigation.Select("Samples & SDKs");
            Assert.IsTrue(
                GraphPages.Navigation.IsAtGraphPage(title),
                @"The opened page should be {0}",
                title);
        }

        /// <summary>
        /// Verify whether Changelog page can be navigated to.
        /// </summary>        
        [TestMethod]
        [Owner("ashirs")]
        public void BVT_Graph_S01_TC06_CanGoToChangelogPage()
        {
            string title = GraphPages.Navigation.Select("Changelog");
            Assert.IsTrue(
                GraphPages.Navigation.IsAtGraphPage(title),
                @"The opened page should be ""Changelog""");
        }

        /// <summary>
        /// Verify whether Changelog page can be navigated to.
        /// </summary>        
        [TestMethod]
        [Owner("ashirs")]
        public void BVT_Graph_S01_TC07_CanGoToExamplesPage()
        {
            string title = GraphPages.Navigation.Select("Examples");
            Assert.IsTrue(
                GraphPages.Navigation.IsAtGraphPage(title),
                @"The opened page should be ""Examples""");
        }

        /// <summary>
        /// Verify whether the default banner image can be loaded.
        /// </summary>
        [TestMethod]
        [Owner("ashirs")]
        public void BVT_Graph_S01_TC08_CanLoadGraphPageImages()
        {
            //Currently ignore Graph explorer and Documentation, since these pages don't have banner image
            //Graph branding image
            string[] navOptions = new string[] {
                "Home",
                "Get started", 
                "Documentation", 
                //"Graph explorer", 
                //"App registration",
                "Samples & SDKs"};

            foreach (string navPage in navOptions)
            {
                GraphPages.Navigation.Select(navPage);
                if (navPage == "Home")
                {
                    foreach (GraphPageImages item in Enum.GetValues(typeof(GraphPageImages)))
                    {
                        Assert.IsTrue(GraphPages.HomePage.CanLoadImages(item));
                    }
                }
                else
                {
                    var graphPage = new GraphPage();
                    foreach (GraphPageImages item in Enum.GetValues(typeof(GraphPageImages)))
                    {
                        //No pages except Home have a banner image anymore
                        if (!item.ToString().Equals("MainBanner"))
                        {
                            Assert.IsTrue(graphPage.CanLoadImages(item));
                        }
                    }
                }
            }
        }
    }
}
