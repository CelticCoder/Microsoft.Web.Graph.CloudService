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
        //[FindsBy(How = How.XPath, Using = "//a[@class='c-logo c-top-nav-link' and contains(@href,'/graph')]")]
        [FindsBy(How = How.XPath, Using = "//a[@class='c-logo' and contains(@href,'/graph')]")]
        private IWebElement homeLinkElement;

        //[FindsBy(How = How.XPath, Using = "//ul[@class='c-menu-container shell-category-top-level shell-category-nav-wrapper']/li/a[contains(@href,'/graph/quick-start')]")]
        [FindsBy(How = How.XPath, Using = "//nav[@id='uhf-c-nav']/a[contains(@href,'/graph/quick-start')]")]
        private IWebElement getstartedLinkElement;

        //[FindsBy(How = How.XPath, Using = "//ul[@class='c-menu-container shell-category-top-level shell-category-nav-wrapper']/li/a[contains(@href,'/graph/docs')]")]
        [FindsBy(How = How.XPath, Using = "//nav[@id='uhf-c-nav']/a[contains(@href,'/graph/docs')]")]
        private IWebElement documentationLinkElement;
        
        //[FindsBy(How = How.XPath, Using = "//ul[@class='c-menu-container shell-category-top-level shell-category-nav-wrapper']/li/a[contains(@href,'/graph/graph-explorer')]")]
        [FindsBy(How = How.XPath, Using = "//nav[@id='uhf-c-nav']/a[contains(@href,'/graph/graph-explorer')]")]
        private IWebElement exploreLinkElement;

        //[FindsBy(How = How.XPath, Using = "//ul[@class='c-menu-container shell-category-top-level shell-category-nav-wrapper']/li/a[contains(@href,'/graph/code-samples-and-sdks')]")]
        [FindsBy(How = How.XPath, Using = "//nav[@id='uhf-c-nav']/a[contains(@href,'/graph/code-samples-and-sdks')]")]
        private IWebElement samplesandsdksLinkElement;

        //[FindsBy(How = How.XPath, Using = "//ul[@class='c-menu-container shell-category-top-level shell-category-nav-wrapper']/li/a[contains(@href,'/graph/docs/overview/changelog')]")]
        [FindsBy(How = How.XPath, Using = "//nav[@id='uhf-c-nav']/a[contains(@href,'/graph/docs/overview/changelog')]")]
        private IWebElement changelogLinkElement;

        //[FindsBy(How = How.XPath, Using = "//ul[@class='c-menu-container shell-category-top-level shell-category-nav-wrapper']/li/a[contains(@href,'/graph/examples')]")]
        [FindsBy(How = How.XPath, Using = "//nav[@id='uhf-c-nav']/a[contains(@href,'/graph/examples')]")]
        private IWebElement examplesLinkElement;

        public string Select(string menuName, bool prodSite = false)
        {
            IWebElement menuItem;
            string menuItemText = string.Empty;
            switch (menuName)
            {
                case ("Home"):
                    //if (prodSite)
                    //{
                    //    menuItem = GraphBrowser.FindElement(By.XPath("//a[@class='c-logo c-top-nav-link' and contains(@href,'/graph')"));
                    //}
                    //else
                    //{
                    //    menuItem = GraphBrowser.FindElement(By.XPath("//a[@class='c-logo' and contains(@href,'/graph')"));
                    //}
                    //menuItemText = menuItem.Text;
                    //GraphBrowser.Click(menuItem);
                    //break;

                    menuItemText = homeLinkElement.Text;
                    GraphBrowser.Click(homeLinkElement);
                    break;
                case ("Quick start"):
                    //if (prodSite)
                    //{
                    //    menuItem = GraphBrowser.FindElement(By.XPath("//ul[@class='c-menu-container shell-category-top-level shell-category-nav-wrapper']/li/a[contains(@href,'/graph/quick-start')"));
                    //}
                    //else
                    //{
                    //    menuItem = GraphBrowser.FindElement(By.XPath("//nav[@id='uhf-c-nav']/a[contains(@href,'/graph/quick-start')"));
                    //}
                    //menuItemText = menuItem.Text;
                    //GraphBrowser.Click(menuItem);
                    //break;

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