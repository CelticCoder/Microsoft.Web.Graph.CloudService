

namespace MSGraphTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using System.Globalization;
    using TestFramework;

    /// <summary>
    /// Test Class for Code Samples and SDK page
    /// </summary>
    [TestClass]
    public class MSGraphCodeSamplePageTest
    {
        /// <summary>
        /// GraphCodeSamplesPage test framework object
        /// </summary>
        private GraphCodeSamplesPage _codeSamplesPage;

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
            GraphBrowser.Goto(GraphUtility.RemoveRedundantPartsfromExtractBaseAddress() + "/code-samples-and-sdks");
            this._codeSamplesPage = new GraphCodeSamplesPage();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            GraphBrowser.Goto(GraphBrowser.BaseAddress);
        }

        /// <summary>
        /// Verify the Code Samples and SDKs page loads
        /// </summary>
        [TestMethod]
        [Owner("kenick")]
        public void BVT_Graph_S09_TC01_CanLoadCodeSamplesPage()
        {
            Assert.IsTrue(
                GraphPages.Navigation.IsAtGraphPage(GraphCodeSamplesPage.PageTitle),
                "The opened page title should be '{0}'",
                GraphCodeSamplesPage.PageTitle);

            Assert.IsNotNull(_codeSamplesPage.BodyElement, "Verify we could find the body element on '{0}'", GraphCodeSamplesPage.PageTitle);
        }
    }
}
