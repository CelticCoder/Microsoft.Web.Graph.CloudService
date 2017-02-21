using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;

namespace TestFramework
{
    public class GraphNavigation : GraphBasePage
    {
        public GraphNavigation()
        { }
        [FindsBy(How = How.XPath, Using = "//a[@class='c-logo' and contains(@href,'/en-us/graph')]")]
        private IWebElement homeLinkElement;

        //[FindsBy(How = How.XPath, Using = "//ul[@class='c-menu-container shell-category-top-level shell-category-nav-wrapper']/li/a[contains(@href,'/en-us/graph/getting-started')]")]
        [FindsBy(How = How.XPath, Using = "//nav[@id='uhf-c-nav']/a[contains(@href,'/en-us/graph/quick-start')]")]
        private IWebElement getstartedLinkElement;

        //[FindsBy(How = How.XPath, Using = "//ul[@class='c-menu-container shell-category-top-level shell-category-nav-wrapper']/li/a[contains(@href,'/en-us/graph/docs')]")]
        [FindsBy(How = How.XPath, Using = "//nav[@id='uhf-c-nav']/a[contains(@href,'/en-us/graph/docs')]")]
        private IWebElement documentationLinkElement;
        
        //[FindsBy(How = How.XPath, Using = "//ul[@class='c-menu-container shell-category-top-level shell-category-nav-wrapper']/li/a[contains(@href,'/en-us/graph/graph-explorer')]")]
        [FindsBy(How = How.XPath, Using = "//nav[@id='uhf-c-nav']/a[contains(@href,'/en-us/graph/graph-explorer')]")]
        private IWebElement exploreLinkElement;

        //[FindsBy(How = How.XPath, Using = "//ul[@class='c-menu-container shell-category-top-level shell-category-nav-wrapper']/li/a[contains(@href,'/en-us/graph/code-samples-and-sdks')]")]
        [FindsBy(How = How.XPath, Using = "//nav[@id='uhf-c-nav']/a[contains(@href,'/en-us/graph/code-samples-and-sdks')]")]
        private IWebElement samplesandsdksLinkElement;

        //[FindsBy(How = How.XPath, Using = "//ul[@class='c-menu-container shell-category-top-level shell-category-nav-wrapper']/li/a[contains(@href,'/en-us/graph/docs/overview/changelog')]")]
        [FindsBy(How = How.XPath, Using = "//nav[@id='uhf-c-nav']/a[contains(@href,'/en-us/graph/docs/overview/changelog')]")]
        private IWebElement changelogLinkElement;

        [FindsBy(How = How.XPath, Using = "//nav[@id='uhf-c-nav']/a[contains(@href,'/en-us/graph/examples')]")]
        private IWebElement examplesLinkElement;

        public string Select(string menuName)
        {
            string menuItemText = string.Empty;
            switch (menuName)
            {
                case ("Home"):
                    menuItemText = homeLinkElement.Text;
                    GraphBrowser.Click(homeLinkElement);
                    break;
                case ("Quick start"):
                    menuItemText = getstartedLinkElement.Text;
                    GraphBrowser.Click(getstartedLinkElement);
                    break;
                case ("Documentation"):
                    menuItemText = documentationLinkElement.Text;
                    GraphBrowser.Click(documentationLinkElement);
                    break;
                case ("Graph explorer"):
                    menuItemText = exploreLinkElement.Text;
                    GraphBrowser.Click(exploreLinkElement);
                    break;
                case ("Samples & SDKs"):
                    menuItemText = samplesandsdksLinkElement.Text;
                    GraphBrowser.Click(samplesandsdksLinkElement);
                    break;
                case ("Changelog"):
                    menuItemText = changelogLinkElement.Text;
                    GraphBrowser.Click(changelogLinkElement);
                    break;
                case ("Examples"):
                    menuItemText = examplesLinkElement.Text;
                    GraphBrowser.Click(examplesLinkElement);
                    break;
                default:
                    break;
            }
            return menuItemText;
        }

        /// <summary>
        /// Verify whether the current graph page has the specific title
        /// </summary>
        /// <param name="graphTitle">The expected page title</param>
        /// <returns>True if yes, else no.</returns>
        public bool IsAtGraphPage(string graphTitle)
        {
            var graphPage = new GraphPage();
            string title = graphPage.GraphTitle.Replace(" ", "").ToLower();

            GraphBrowser.GoBack();
            return title.Contains(graphTitle.ToLower().Replace(" ", ""));
        }
    }
}