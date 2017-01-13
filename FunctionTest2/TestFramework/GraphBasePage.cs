using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework
{
    using OpenQA.Selenium;

    public class GraphBasePage
    {

        [FindsBy(How = How.Id, Using = "fb-btn")]
        private IWebElement shareFacebookButton;

        [FindsBy(How = How.Id, Using = "twitter-btn")]
        private IWebElement shareTwitterButton;

        [FindsBy(How = How.Id, Using = "yammer-btn")]
        private IWebElement shareYammerButton;

        
        /// <summary>
        /// The constructor method
        /// </summary>
        public GraphBasePage()
        {
            PageFactory.InitElements(GraphBrowser.Driver, this);
        }

        /// <summary>
        /// Select the share on Facebook button
        /// </summary>
        public void SelectShareFacebook()
        {
            shareFacebookButton.Click();
        }

        /// <summary>
        /// Select the share on Twitter button
        /// </summary>
        public void SelectShareTwitter()
        {
            shareTwitterButton.Click();
        }

        /// <summary>
        /// Select the share on Yammer button
        /// </summary>
        public void SelectShareYammer()
        {
            shareYammerButton.Click();
        }
    }
}
