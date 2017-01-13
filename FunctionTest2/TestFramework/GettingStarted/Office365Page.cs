using System;
using OpenQA.Selenium;

namespace TestFramework.Office365Page
{
    public class Office365Page
    {
        public CardSetupPlatform CardSetupPlatform
        {
            get
            {
                return new CardSetupPlatform();
            }
        }

        public CardTryItOut CardTryItOut
        {
            get
            {
                return new CardTryItOut();
            }
        }

        public CardDownloadCode CardDownloadCode
        {
            get
            {
                return new CardDownloadCode();
            }
        }

        public bool OnlyDefaultCardsDisplayed()
        {
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

        public bool IsCardDisplayed(string CardId)
        {
            if (CardId.Contains("AllSet"))
            {
                var element = GraphBrowser.Driver.FindElement(By.Id("AllSet"));
                return element.Displayed;
            }
            else
            {
                var elements = GraphBrowser.Driver.FindElements(By.ClassName("card"));
                if (elements.Count > 0)
                {
                    foreach (IWebElement item in elements)
                    {
                        string itemId = item.GetAttribute("id");
                        if (itemId == CardId)
                        {
                            return item.Displayed;
                        }
                    }

                    return false;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool CanLoadImages()
        {
            var elements = GraphBrowser.Driver.FindElements(By.CssSelector("#pickPlatform > div > button"));
            foreach (IWebElement item in elements)
            {
                IWebElement subItem = item.FindElement(By.CssSelector("img"));
                string Url = subItem.GetAttribute("src");
                string whiteUrl = Url.Replace("grey_", ""); //Check both the grey and white versions of the images
                if (!GraphUtility.FileExist(Url) || !GraphUtility.FileExist(whiteUrl))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
