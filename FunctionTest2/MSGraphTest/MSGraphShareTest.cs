
namespace MSGraphTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using TestFramework;

    [TestClass]
    public class MSGraphShareTest
    {
        /// <summary>
        /// GraphHomePage test framework object
        /// </summary>
        private GraphHomePage _homePage;

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
            _homePage = new GraphHomePage();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            GraphBrowser.Goto(GraphBrowser.BaseAddress);
        }

        /// <summary>
        /// Selecting Facebook button launches a new browser window for Facebook
        /// </summary>
/*
 * Commented out for new architecture, until we reimplement the share feature
[TestMethod]
[Owner("kenick")]
public void BVT_S10_TC01_CanShareFacebook()
{
    this._homePage.SelectShareFacebook();

    GraphBrowser.SwitchToNewWindow();
    Assert.IsTrue(GraphBrowser.Title.Contains("Facebook"), "Verify Facebook share button launches new window for Facebook");

    GraphBrowser.SwitchBack();
}
*/

/// <summary>
/// Selecting Twitter button launches a new browser window for Twitter
/// </summary>
/*
 * Commented out for new architecture, until we reimplement the share feature
[TestMethod]
[Owner("kenick")]
public void BVT_S10_TC02_CanShareTwitter()
{
    string expectedShareText = GraphBrowser.Title;

    this._homePage.SelectShareTwitter();

    GraphBrowser.SwitchToNewWindow();
    Assert.IsTrue(GraphBrowser.Title.Contains("Twitter"), "Verify Twitter share button launches new window for Twitter");

    IWebElement shareText = GraphBrowser.FindElement(By.Id("status"));
    Assert.IsNotNull(shareText, "Found the share text for Twitter window");

    Assert.IsTrue(shareText.Text.Contains(expectedShareText), "Verify Twitter share text contains the page title");

    GraphBrowser.SwitchBack();
}
*/

/// <summary>
/// Selecting Yammer button launches a new browser window for Yammer
/// </summary>
/*
 * Commented out for new architecture, until we reimplement the share feature
[TestMethod]
[Owner("kenick")]
public void BVT_S10_TC03_CanShareYammer()
{
    this._homePage.SelectShareYammer();

    GraphBrowser.SwitchToNewWindow();
    Assert.IsTrue(GraphBrowser.Title.Contains("Yammer"), "Verify Yammer share button launches new window for Yammer");

    GraphBrowser.SwitchBack();
}
*/
    }
}
