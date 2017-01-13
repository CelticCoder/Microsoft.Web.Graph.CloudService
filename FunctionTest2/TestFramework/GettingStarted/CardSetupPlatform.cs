using System;
using OpenQA.Selenium;

namespace TestFramework.Office365Page
{
    public class CardSetupPlatform : GraphBasePage
    {
        public void ChoosePlatform(Platform platformName)
        {
            if (!GraphBrowser.Url.Contains("/getting-started"))
            {
                GraphBrowser.Goto(GraphUtility.RemoveRedundantPartsfromExtractBaseAddress() + "/getting-started#setup");
            }
            
            //To account for iOS_Swift and iOS_Objective_C enums, since enum cannot contain - character but the div IDs contain -
            var platform = GraphBrowser.Driver.FindElement(By.Id("option-"+platformName.ToString().ToLower().Replace("_","-")));
            GraphBrowser.Click(platform);

            // Need refactor: Sometimes case failed for the platform setup text is not changed in time
            GraphBrowser.Wait(TimeSpan.FromSeconds(2));
        }

        public bool IsShowingPlatformSetup(Platform platformName)
        {
            var setupPlatformDoc = GraphBrowser.Driver.FindElement(By.CssSelector("#ShowDocumentationDiv>h1"));
            string platformDescription = EnumExtension.GetDescription(platformName).ToLower();
            //iOS swift and objective C descriptions only contain the word "ios," not full platform name
            if (platformDescription.Contains("ios"))
            {
                return setupPlatformDoc.Text.ToLower().Contains("ios");
            }
            return setupPlatformDoc.Text.ToLower().Contains(platformDescription);
        }
    }
}