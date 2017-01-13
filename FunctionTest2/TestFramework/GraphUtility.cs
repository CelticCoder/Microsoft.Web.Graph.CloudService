using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;

namespace TestFramework
{
    /// <summary>
    /// Static class for common functions of Graph site
    /// </summary>
    public static class GraphUtility
    {
        public static int DefaultWaitTime = int.Parse(GetConfigurationValue("DefaultWaitTime"));
        public static readonly int MinWidthToShowParam = 895;

        /// <summary>
        /// Verify if the toggle arrow is found on the page 
        /// </summary>
        /// <returns>Trye if yes, else no.</returns>
        public static bool IsToggleArrowDisplayed()
        {
            return GraphBrowser.FindElement(By.Id("toggleLeftPanelContainer")).Displayed;
        }

        /// <summary>
        /// Verify if the menu-content is found on the page
        /// </summary>
        /// <returns>Trye if yes, else no.</returns>
        public static bool IsMenuContentDisplayed()
        {
            return GraphBrowser.FindElement(By.CssSelector("#docMenu")).Displayed;
        }

        /// <summary>
        /// Execute the menu display toggle
        /// </summary>
        public static void ToggleMenu()
        {
            var element = GraphBrowser.FindElement(By.Id("toggleLeftPanel"));
            GraphBrowser.Click(element);
            GraphBrowser.Wait(TimeSpan.FromSeconds(2));
        }

        /// <summary>
        /// Get the document title in the current doc page
        /// </summary>
        /// <returns>The title of document</returns>
        public static string GetDocTitle()
        {
            string docTitle = GraphBrowser.FindElement(By.CssSelector("h1")).Text;
            //If docTitle includes a line break followed by "Edit in GitHub," only return the first part
            string[] titleParts = docTitle.Split('\r');
            return titleParts[0];
        }

        /// <summary>
        /// Get the banner image url of MS Graph site
        /// </summary>
        /// <returns>The url of the banner image</returns>
        public static string GetGraphBannerImageUrl()
        {
            var element = GraphBrowser.FindElement(By.Id("banner-image"));
            if (element == null)
            {
                element = GraphBrowser.FindElement(By.CssSelector("div#layout-featured>div>article>div>div>div>div"));
            }
            string styleString = element.GetAttribute("style");
            string[] styles = styleString.Split(';');

            string url = string.Empty;
            foreach (string style in styles)
            {
                if (style.Contains("background-image"))
                {
                    int startIndex = style.IndexOf("http");
                    //2 is the length of ") or ')
                    url = style.Substring(startIndex, style.Substring(startIndex).Length - 2);
                    break;
                }
            }
            return url;
        }

        /// <summary>
        /// Find an link or a button role span according to the specific text and click it
        /// </summary>
        /// <param name="text">The text of the element</param>
        public static void Click(string text)
        {
            var element = GraphBrowser.FindElement(By.LinkText(text));
            //a link
            if (element != null && element.Displayed)
            {
                GraphBrowser.Click(element);
            }
            else
            {
                IReadOnlyList<IWebElement> elements = GraphBrowser.webDriver.FindElements(By.XPath("//*[@role='button']"));
                foreach (IWebElement elementToClick in elements)
                {
                    if (elementToClick.GetAttribute("innerHTML").Equals(text) && (elementToClick.Displayed))
                    {
                        GraphBrowser.Click(elementToClick);
                        break;
                    }
                }
            }
        }

        public static void ClickMenuItem(string text)
        {
            GraphBrowser.Wait(By.XPath(text));
            var element = GraphBrowser.FindElement(By.XPath(text));
            //a link
            if (element != null && element.Displayed)
            {
                GraphBrowser.Click(element);
            }
            else
            {
                IReadOnlyList<IWebElement> elements = GraphBrowser.webDriver.FindElements(By.XPath("//*[@role='button']"));
                foreach (IWebElement elementToClick in elements)
                {
                    if (elementToClick.GetAttribute("innerHTML").Equals(text) && (elementToClick.Displayed))
                    {
                        GraphBrowser.Click(elementToClick);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Find a button according to the specific text and click it
        /// </summary>
        /// <param name="text">The text of the element</param>
        public static void ClickButton(string text)
        {
            IReadOnlyList<IWebElement> elements = GraphBrowser.webDriver.FindElements(By.TagName("button"));

            foreach (IWebElement elementToClick in elements)
            {
                if (elementToClick.GetAttribute("innerHTML").Trim().Contains(text) && elementToClick.Displayed)
                {
                    GraphBrowser.Click(elementToClick);
                    break;
                }
            }
        }

        /// <summary>
        /// Extracts the base address from the value in App.config
        /// Can no longer remove the locale, which is now a necessary part of the base URL as it comes before /graph
        /// </summary>
        /// <returns>The base address</returns>
        public static string RemoveRedundantPartsfromExtractBaseAddress()
        {
            string prefix = GraphBrowser.BaseAddress;

            //GraphBrowser.BaseAddress should end with ..-../graph, where ..-.. is a locale
            //remove everything after that first instance of /graph (which includes a querystring)
            prefix = prefix.Substring(0, prefix.IndexOf("/graph") + 6);

            //change to https if it is not already
            if (prefix.IndexOf("https") == -1) 
            {
                prefix = prefix.Replace("http", "https");
            }
            return prefix;
        }

        /// <summary>
        /// Randomly find a TOC item, which has a sub content menu, at a specific layer.
        /// </summary>
        /// <param name="layerIndex">The layer index. Starts from 0.</param>
        /// <returns>A list including the names of path items</returns>
        public static List<string> FindTOCParentItems(int layerIndex)
        {
            List<string> tocPath = new List<string>();
            string xpath = @"//nav[@id='home-nav-blade']";

            for (int i = 0; i <= layerIndex; i++)
            {
                xpath += "/ul/li";
            }
            var elements = GraphBrowser.webDriver.FindElements(By.XPath(xpath + "/a[@data-target]"));
            int randomIndex = new Random().Next(elements.Count);
            var element = elements[randomIndex];
            string title = element.GetAttribute("innerHTML");
            if (element.GetAttribute("style").Contains("text-transform: uppercase"))
            {
                title = title.ToUpper();
            }

            var ancestorElements = element.FindElements(By.XPath("ancestor::li/a")); //parent relative to current element
            for (int j = 0; j < ancestorElements.Count - 1; j++)
            {
                string ancestorTitle = ancestorElements[j].GetAttribute("innerHTML");
                if (ancestorElements[j].GetAttribute("style").Contains("text-transform: uppercase"))
                {
                    ancestorTitle = ancestorTitle.ToUpper();
                }
                tocPath.Add(ancestorTitle);
            }

            tocPath.Add(title);
            return tocPath;
        }

        /// <summary>
        /// Verify whether a TOC item's related sub layer is shown 
        /// </summary>
        /// <param name="item">The TOC item</param>
        /// <returns>True if yes, else no.</returns>
        public static bool SubLayerDisplayed(string item)
        {
            string xpath = @"//nav[@id='home-nav-blade']";
            var element = GraphBrowser.FindElement(By.XPath(xpath));
            var menuItem = element.FindElement(By.LinkText(item));
            string subMenuId = menuItem.GetAttribute("data-target");
            if (subMenuId != null && subMenuId != string.Empty)
            {
                var subMenu = element.FindElement(By.XPath("//ul[@id='" + subMenuId.Replace("#", string.Empty) + "']"));
                return subMenu.Displayed;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get The layer count of TOC
        /// </summary>
        /// <returns>The layer count</returns>
        public static int GetTOCLayer()
        {
            string xpath = "//nav[@id='home-nav-blade']";
            var menuElement = GraphBrowser.FindElement(By.XPath(xpath));
            int layer = 0;
            try
            {
                do
                {
                    layer++;
                    xpath += "/ul/li";
                    var element = menuElement.FindElement(By.XPath(xpath + "/a"));
                } while (true);
            }
            catch (NoSuchElementException)
            {
            }
            return layer - 1;
        }

        /// <summary>
        /// Login on a sign-in page 
        /// </summary>
        /// <param name="userName">The userName to input</param>
        /// <param name="password">The password to input</param>
        public static void Login(string userName, string password)
        {
            var userIdElement = GraphBrowser.FindElement(By.Id("cred-userid-inputtext"));
            if (userIdElement.Displayed)
            {
                userIdElement.SendKeys(userName);
            }
            else
            {
                var existentUser = GraphBrowser.webDriver.FindElement(By.CssSelector("li#login_user_chooser>a#" + userName.Replace("@", "_").Replace(".", "_") + "_link"));
                GraphBrowser.Click(existentUser);
            }
            var passwordElement = GraphBrowser.FindElement(By.XPath("/html/body/div/div[2]/div[3]/div/div[2]/div/div[5]/form/div[4]/div/input"));
            passwordElement.SendKeys(password);
            GraphBrowser.Wait(By.XPath("/html/body/div/div[2]/div[3]/div/div[2]/div/div[5]/form/div[6]/input[1]"));
            var signInElement = GraphBrowser.FindElement(By.XPath("/html/body/div/div[2]/div[3]/div/div[2]/div/div[5]/form/div[6]/input[1]"));
            int waitTime = Int32.Parse(GraphUtility.GetConfigurationValue("WaitTime"));
            int retryCount = Int32.Parse(GraphUtility.GetConfigurationValue("RetryCount"));
            int i = 0;
            do
            {
                GraphBrowser.Wait(TimeSpan.FromSeconds(waitTime));
                //Reload the element to avoid it timeout
                signInElement = GraphBrowser.FindElement(By.XPath("/html/body/div/div[2]/div[3]/div/div[2]/div/div[5]/form/div[6]/input[1]"));
                i++;
            } while (i < retryCount && !signInElement.Enabled);
            GraphBrowser.Click(signInElement);
        }

        /// <summary>
        /// Verify whether the logged in user is correct
        /// </summary>
        /// <param name="expectedUserName">The expected logged in user</param>
        /// <returns>True if yes, else no.</returns>
        public static bool IsLoggedIn(string expectedUserName = "")
        {
            GraphBrowser.Wait(By.XPath("//div[@ng-show='userInfo.isAuthenticated']/span"));
            var element = GraphBrowser.FindElement(By.XPath("//div[@ng-show='userInfo.isAuthenticated']/span"));
            if (element.Displayed && expectedUserName != "" && element.Text.Equals(expectedUserName))
            {
                return true;
            }
            else if (expectedUserName == "" && element.Displayed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Input a query string on Graph explorer page
        /// </summary>
        /// <param name="version">The target service version</param>
        /// <param name="request">The resource to access/manipulate in the Microsoft Graph API request</param>
        public static void InputExplorerQueryString(string version,string resource)
        {
            string lcn = GetLCN();
            string request;
            if(lcn.Equals("zh-cn"))
            {
                request = "https://microsoftgraph.chinacloudapi.cn/" + version + "/" + resource;
            }
            else
            {
                request = "https://graph.microsoft.com/" + version + "/" + resource;
            }
            GraphBrowser.Wait(By.XPath("/html/body/div[1]/div/div[2]/div/div/div/div/article[2]/div/div/div/div[3]/form/div/md-autocomplete/md-autocomplete-wrap/input"));
            var inputElement = GraphBrowser.Driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div/div/div/div/article[2]/div/div/div/div[3]/form/div/md-autocomplete/md-autocomplete-wrap/input"));
            inputElement.Clear();
            inputElement.SendKeys(request);
        }

        /// <summary>
        /// Format a property to JSON format  and put it in Explorer request field
        /// </summary>
        /// <param name="properties">The properties to format</param>
        public static void InputExplorerJSONBody(Dictionary<string, string> properties)
        {
            GraphBrowser.Wait(By.XPath("/html/body/div[1]/div/div[2]/div/div/div/div/article[2]/div/div/div/div/md-tabs[1]/md-tabs-content-wrapper/md-tab-content[2]/div/div/textarea"));
            var element = GraphBrowser.FindElement(By.CssSelector("div#jsonEditor>textarea"));
            element.SendKeys("{");
            int index = 0;
            foreach (KeyValuePair<string, string> property in properties)
            {
                index++;
                element.SendKeys("\"" + property.Key + "\":\"" + property.Value + "\"");
                if (index != properties.Count)
                {
                    element.SendKeys(",");
                }
            }
            element.SendKeys("}");
        }

        public static void InputExplorerHeaders(string properties)
        {
            GraphBrowser.Wait(By.XPath("/html/body/div[1]/div/div[2]/div/div/div/div/article[2]/div/div/div/div/md-tabs[1]/md-tabs-content-wrapper/md-tab-content[1]/div/div/textarea"));
            var element = GraphBrowser.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div/div/div/div/article[2]/div/div/div/div/md-tabs[1]/md-tabs-content-wrapper/md-tab-content[1]/div/div/textarea"));
            element.SendKeys(properties);
        }

        /// <summary>
        /// Get the response on Graph explorer page
        /// </summary>
        /// <returns>The composed response string</returns>
        public static string GetExplorerResponse()
        {
            GraphBrowser.Wait(By.XPath("//div[@id='jsonViewer']/div/div[contains(@class,'ace_content')]/div[contains(@class,'ace_text-layer')]"));
            StringBuilder responseBuilder = new StringBuilder();
            IReadOnlyList<IWebElement> responseElements = GraphBrowser.webDriver.FindElements(By.CssSelector("div#jsonViewer>div.ace_scroller>div>div.ace_layer.ace_text-layer>div.ace_line> span"));
            for (int i = 0; i < responseElements.Count; i++)
            {
                responseBuilder.Append(responseElements[i].Text);
            }
            //Remove the braces
            if (responseBuilder.ToString().StartsWith("{"))
            {
                int length = responseBuilder.Length;
                return responseBuilder.ToString().Substring(1, length - 2);
            }
            else
            {
                return responseBuilder.ToString();
            }
        }

        /// <summary>
        /// Get a specific property from the response
        /// </summary>
        /// <param name="jsonString">The response string</param>
        /// <param name="propertyName">The property's name</param>
        /// <returns>The property's value</returns>
        public static string GetProperty(string jsonString, string propertyName)
        {
            int propertyNameIndex = jsonString.IndexOf("\"" + propertyName + "\"");
            int propertyValueStartIndex;
            propertyValueStartIndex = propertyNameIndex + propertyName.Length + 2;
            string subJsonString = jsonString.Substring(propertyValueStartIndex);
            int propertyValueEndIndex;
            propertyValueEndIndex = subJsonString.IndexOf("\"\"");
            if (propertyValueEndIndex == -1)
            {
                propertyValueEndIndex = subJsonString.LastIndexOf("\"");
            }
            return subJsonString.Substring(1, propertyValueEndIndex - 1);
        }

        public static void SelectO365AppRegisstration()
        {
            var element = GraphBrowser.FindElement(By.XPath("//a[contains(@href,'dev.office.com/app-registration')]"));
            GraphBrowser.Click(element);
        }

        public static void SelectNewAppRegisstrationPortal()
        {
            var element = GraphBrowser.FindElement(By.XPath("//a[contains(@href,'apps.dev.microsoft.com')]"));
            GraphBrowser.Click(element);
        }

        /// <summary>
        /// Verify whether the page is at the expected app registration portal
        /// </summary>
        /// <param name="isNewPortal">The expected portal page type, true for
        /// new protal, false for O365 portal</param>
        /// <returns>True if yes, else no.</returns>
        public static bool IsAtApplicationRegistrationPortal(bool isNewPortal)
        {
            GraphBrowser.webDriver.SwitchTo().DefaultContent();
            string urlKeyWord = isNewPortal ? "apps.dev.microsoft.com" : "dev.office.com/app-registration";
            // get all window handles
            IList<string> handlers = GraphBrowser.webDriver.WindowHandles;
            foreach (var winHandler in handlers)
            {
                GraphBrowser.webDriver.SwitchTo().Window(winHandler);
                if (GraphBrowser.webDriver.Url.Contains(urlKeyWord))
                {
                    return true;
                }
                else
                {
                    GraphBrowser.webDriver.SwitchTo().DefaultContent();
                }
            }
            return false;
        }

        /// <summary>
        /// Select "See OverView" on Home page
        /// </summary>
        public static void SelectToSeeOverView()
        {
            var element = GraphBrowser.FindElement(By.XPath("//div/a[contains(@href,'/docs')]"));
            GraphBrowser.Click(element);
        }

        /// <summary>
        /// Select "Try the API" on Home page
        /// </summary>
        public static void SelectToTryAPI()
        {
            var element = GraphBrowser.FindElement(By.XPath("//div/a[contains(@href,'graph-explorer')]"));
            GraphBrowser.Click(element);
        }

        /// <summary>
        /// Return a random selection of items at TOC's specific level
        /// </summary>
        /// <param name="index">The specific level index. Starts from 0</param>
        /// <param name="hasDoc">Indicates whether only returns the items each of which has the related documents</param>
        /// <returns>TOC Items' title-and-links(separated by ,). The title part contains the item's whole path in TOC</returns>
        public static string GetTOCItem(int index, bool hasDoc = false)
        {
            string xPath = "//nav[@id='home-nav-blade']";
            for (int i = 0; i <= index; i++)
            {
                xPath += "/ul/li";
            }
            //Find all the toc items at the specific level
            IReadOnlyList<IWebElement> links = GraphBrowser.webDriver.FindElements(By.XPath(xPath + "/a"));
            string item = string.Empty;

            int randomIndex;
            do
            {
                randomIndex = new Random().Next(links.Count);

                string path = string.Empty;
                var ancestorElements = links[randomIndex].FindElements(By.XPath("ancestor::li/a")); //parent relative to current element
                for (int j = 0; j < ancestorElements.Count - 1; j++)
                {
                    string ancestorTitle = ancestorElements[j].GetAttribute("innerHTML");
                    if (ancestorElements[j].GetAttribute("style").Contains("text-transform: uppercase"))
                    {
                        ancestorTitle = ancestorTitle.ToUpper();
                    }
                    path += ancestorTitle + ">";
                }
                string title = links[randomIndex].GetAttribute("innerHTML");
                if (links[randomIndex].GetAttribute("style").Contains("text-transform: uppercase"))
                {
                    title = title.ToUpper();
                }
                if (hasDoc)
                {
                    if (!links[randomIndex].GetAttribute("href").EndsWith("/"))
                    {
                        item = path + title + "," + links[randomIndex].GetAttribute("href");
                    }
                }
                else
                {
                    item = path + title + "," + links[randomIndex].GetAttribute("href");
                }
            } while (links[randomIndex].GetAttribute("href").EndsWith("/")
                //Beta reference->onenote doesn't have related document
                || links[randomIndex].GetAttribute("href").EndsWith("api-reference/beta/resources/note")
                );
            return item;
        }

        /// <summary>
        /// Verify if the document displayed matches TOC item's link
        /// </summary>
        /// <param name="tocLink">TOC item's link</param>
        /// <returns>True if yes, else no.</returns>
        public static bool ValidateDocument(string tocLink)
        {
            return FileExist(tocLink);
        }


        public static void ClickLogin()
        {
            GraphBrowser.Wait(By.XPath("/html/body/div[1]/div/div[2]/div/div/div/div/article[2]/div/div/div/div[1]/button"));
            var element = GraphBrowser.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div/div/div/div/article[2]/div/div/div/div[1]/button"));
            GraphBrowser.Click(element);
        }

        public static void ClickLogout()
        {
            GraphBrowser.Wait(By.XPath("//a[@ng-click='logout()']"));
            var element = GraphBrowser.FindElement(By.XPath("//a[@ng-click='logout()']"));
            GraphBrowser.Click(element);
        }

        /// <summary>
        /// Get a property's value from App.config
        /// </summary>
        /// <param name="propertyName">The property's key</param>
        /// <returns>The property's value</returns>
        public static string GetConfigurationValue(string propertyName)
        {
            return ConfigurationManager.AppSettings[propertyName];
        }

        /// <summary>
        /// Verify whether a url refer to a valid file/image
        /// </summary>
        /// <param name="Url">The file/image url</param>
        /// <returns>True if yes, else no</returns>
        public static bool FileExist(string Url)
        {
            //Url = AppendQueryStringToBaseAddress(Url);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            //request.CookieContainer = new CookieContainer();
            //request.CookieContainer.Add(new Uri(RemoveRedundantPartsfromExtractBaseAddress()), new System.Net.Cookie("setvar", "fltpreview:1"));
            request.Timeout = 15000;
            request.Method = "HEAD";

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NotModified);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string GetLCN()
        {
            string url = GraphBrowser.Url;
            string restPart = GraphBrowser.Url.Substring(url.IndexOf("://") + 3);
            string lcnName = restPart.Split('/')[1];
            return lcnName;
        }

        /// <summary>
        /// Try to find a cooperation note on Chinese Explorer page.
        /// </summary>
        /// <returns>True if found, else no.</returns>
        public static bool FindCHNExplorerNote()
        {
            var noteElement = GraphBrowser.FindElement(By.XPath("//div[contains(text(),'注意')]"));
            return (noteElement != null);
        }

        /// <summary>
        /// Verify whether the requests on Chinese Explorer page are valid
        /// </summary>
        /// <param name="incorrectRequest">The invalid request (if any) for chinese endpoint</param>
        /// <returns>True if all requests are valid, else false.</returns>
        public static bool VerifyExplorerRequestListOnCHNEndpoint(out string incorrectRequest)
        {
            incorrectRequest = string.Empty;
            var requestCount = GraphBrowser.webDriver.FindElements(By.CssSelector("datalist#requestList>option")).Count;
            for (int i = 0; i < requestCount; i++)
            {
                var requestOption = GraphBrowser.webDriver.FindElements(By.CssSelector("datalist#requestList>option"))[i];
                string request = requestOption.GetAttribute("value");
                if (!request.StartsWith("https://microsoftgraph.chinacloudapi.cn/"))
                {
                    incorrectRequest = request;
                    return false;
                }
            }
            return true;
        }

        public static List<SearchedResult> SearchText(string keyWord)
        {
            List<SearchedResult> searchedResults = new List<SearchedResult>();

            var element = GraphBrowser.FindElement(By.CssSelector("input#q"));
            element.Clear();
            element.SendKeys(keyWord);
            var searchButton = GraphBrowser.FindElement(By.XPath("//button[text()='Search']"));
            GraphBrowser.Click(searchButton);

            GraphBrowser.Wait(By.CssSelector("ul#local-docs-ul>li"));
            int resultCount = GraphBrowser.webDriver.FindElements(By.CssSelector("ul#local-docs-ul>li")).Count;
            for (int i = 0; i < resultCount; i++)
            {
                SearchedResult result = new SearchedResult();
                result.Name = GraphBrowser.webDriver.FindElement(By.CssSelector("ul#local-docs-ul>li:nth-child(" + (int)(i + 2) + ")>div > div.event-info > div > div.col-xs-8.name.cp1")).Text;
                result.Description = GraphBrowser.webDriver.FindElement(By.CssSelector("ul#local-docs-ul>li:nth-child(" + (int)(i + 2) + ")> div > div> div.desc")).Text;
                result.DetailLink = GraphBrowser.webDriver.FindElement(By.CssSelector("ul#local-docs-ul>li:nth-child(" + (int)(i + 2) + ") > div > div.event-info > div > div.col-xs-8.event-links > a")).GetAttribute("href");
                searchedResults.Add(result);
            }
            return searchedResults;
        }

        public static bool CheckUrl(string url)
        {
            bool success = false;
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var response = client.SendAsync(request).Result;
                string content = response.Content.ReadAsStringAsync().Result;
                success = !content.Contains("NotFound.htm");
            }
            return success;
        }

        /// <summary>
        /// Check whether in this article control is displayed
        /// </summary>
        /// <returns>True if yes, else no.</returns>
        public static bool IsInThisArticleDisplayed()
        { 
            //Wait for in this article to render. Will time out after 45 seconds if failed             
            GraphBrowser.Wait(By.CssSelector("#inThisArticle"));
            return GraphBrowser.FindElement(By.CssSelector("#sidebarcontrol")).GetCssValue("display") != "none";
        }

        public static bool CheckThreeCardsDisplayed()
        {
            GraphBrowser.Wait(By.Id("setup"));
            GraphBrowser.Wait(By.Id("try-it-out"));

            var elements = GraphBrowser.Driver.FindElements(By.ClassName("card"));
            if (elements.Count > 0)
            {
                foreach (IWebElement item in elements)
                {
                    string itemId = item.GetAttribute("id");
                    if ((itemId == "intro" || itemId == "try-it-out" || itemId == "setup") && !item.Displayed)
                    {
                        return false;
                    }

                    if (itemId != "intro" && itemId != "try-it-out" && itemId != "setup" && item.Displayed)
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<string> TraverseTocAndGetFailedDocs()
        {
            List<string> failedDocs = new List<string>();
            string xPath = "//nav[@id='home-nav-blade']/ul/li";
            IWebElement currElement;
            IReadOnlyList<IWebElement> nextLevelElements;
            Stack elementsToTraverse = new Stack();

            //Initialize stack with each top level element in TOC
            IReadOnlyList<IWebElement> topLevelElements = GraphBrowser.webDriver.FindElements(By.XPath(xPath + "/a"));
            foreach (IWebElement elem in topLevelElements)
            {
                elementsToTraverse.Push(elem);
            }

            while (elementsToTraverse.Count != 0)
            {
                currElement = (IWebElement)elementsToTraverse.Pop();
                //click currElement
                if (currElement.Displayed)
                {
                    GraphBrowser.Click(currElement);
                }
                else
                {
                    return new List<string> { "The element was not displayed and clickable!", currElement.GetAttribute("href") };
                }

                //Validate that document title exists and isn't "Document not found."
                //Note: even if the current container does not have an associated doc, the user should not see "document not found"
                string validationResult = ValidateDocumentByTitle();
                if (validationResult != "")
                {
                    failedDocs.Add(validationResult + currElement.GetAttribute("href"));
                }

                //Make an HttpWebRequest to ensure that the file exists and does not have "Document not found." title
                validationResult = ValidateDocumentByHttpRequest(currElement.GetAttribute("href"));
                if (validationResult != "")
                {
                    failedDocs.Add(validationResult + currElement.GetAttribute("href"));
                }

                //Find all elements one level down in TOC and add to stack
                nextLevelElements = currElement.FindElements(By.XPath("../ul/li/a"));
                foreach (IWebElement elem in nextLevelElements)
                {
                    elementsToTraverse.Push(elem);
                }
            }

            return failedDocs;
        }

        //Wait for doc title to appear, check whether it is "Document not found."
        //If document not found, return "'Document not found' displays to user: "
        //If exception is caught, return "Could not find doc title: "
        //If document title exists and is valid, return ""
        public static string ValidateDocumentByTitle()
        {
            try
            {
                GraphBrowser.Wait(By.CssSelector("h1"));
                if (GraphBrowser.FindElement(By.CssSelector("h1")).Text == "Document not found.")
                {
                    return "'Document not found' displays to user: ";
                }
            }
            catch (NoSuchElementException)
            {
                return "Could not find doc title: ";
            }
            return "";
        }

        public static string ValidateDocumentByHttpRequest(string Url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Timeout = 15000;
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NotModified)
                    {
                        //Read the returned html to see if it is "Document not found."
                        Stream receiveStream = response.GetResponseStream();
                        StreamReader readStream = new StreamReader(receiveStream);
                        string readResponse = readStream.ReadToEnd();
                        if (readResponse.Contains("Document not found.</h1>"))
                        {
                            return "Document not found from httpwebrequest: ";
                        }
                        return "";
                    }
                    else
                    {
                        return "Bad status code: ";
                    }
                }
            }
            catch (Exception)
            {
                return "Exception in HttpWebRequest: ";
            }
        }

        public static string AppendQueryStringToBaseAddress(string Url)
        {
            return Url + GraphBrowser.QueryString;
        }

        public static string GetLocaleFromUrl(string Url)
        {
            Regex localePattern = new Regex("/[a-z][a-z]-[a-z][a-z]/");
            Match m = localePattern.Match(Url);
            return m.ToString();
        }
    }
}
