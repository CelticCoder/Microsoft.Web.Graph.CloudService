using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFramework;

namespace MSGraphTest
{
    /// <summary>
    /// Test Class for Microsoft Graph Home page
    /// </summary>
    [TestClass]
    public class MSGraphHomePageTest
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
        /// Verify whether Home page can be navigated to, 
        /// first from home page itself and then by navigating to another page and back via nav bar.
        /// </summary>
        [TestMethod]
        [Owner("arvindsa")]
        public void BVT_Graph_S02_TC01_CanGoToHomePage()
        {
            string title = GraphPages.Navigation.Select("Home");
            Assert.IsTrue(
                GraphPages.Navigation.IsAtGraphPage(title),
                @"The opened page should be {0} when clicking it",
                title);

            string[] navOptions = new string[] { 
                "Get started", 
                "Documentation", 
                "Graph explorer", 
                "Samples & SDKs", 
                "Changelog" };

            //Go to the other page to click "Home" on nav bar
            string navPage = navOptions[new Random().Next(navOptions.Length)];
            GraphPages.Navigation.Select(navPage);

            title = GraphPages.Navigation.Select("Home");
            Assert.IsTrue(
                GraphPages.Navigation.IsAtGraphPage(title),
                @"The opened page should be {0} when clicking it {1} page's nav bar",
                title,
                navPage);
        }
        
    }
}