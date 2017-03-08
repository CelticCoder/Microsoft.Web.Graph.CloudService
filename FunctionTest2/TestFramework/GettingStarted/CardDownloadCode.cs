using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Office365Page
{
    public class CardDownloadCode : GraphBasePage
    {
        public void DownloadCode()
        {
            var downloadBtn = GraphBrowser.Driver.FindElement(By.Id("downloadCodeSampleButtonRest"));
            GraphBrowser.Click(downloadBtn);

            // Give 3 seconds for download to finish before checking for postdownload instructions
            GraphBrowser.Wait(TimeSpan.FromSeconds(int.Parse(GraphUtility.GetConfigurationValue("WaitTime"))));
        }

        public bool IsCodeDownloaded()
        {
            var downloadResult = GraphBrowser.Driver.FindElement(By.Id("post-download-instructions"));
            return downloadResult.Displayed;
        }
    }
}