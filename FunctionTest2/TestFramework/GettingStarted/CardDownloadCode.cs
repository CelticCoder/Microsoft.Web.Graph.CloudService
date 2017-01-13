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

            // When the card indicates all thing is done is displayed, the click event can be considered as finished.
            GraphBrowser.Wait(By.Id("AllSet"));
        }

        public bool IsCodeDownloaded()
        {
            var downloadResult = GraphBrowser.Driver.FindElement(By.Id("post-download-instructions"));
            return downloadResult.Displayed;
        }
    }
}
