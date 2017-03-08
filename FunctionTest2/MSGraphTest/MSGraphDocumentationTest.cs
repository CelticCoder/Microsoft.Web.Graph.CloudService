using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFramework;
using System.Drawing;
using System.Collections.Generic;

namespace MSGraphTest
{
    /// <summary>
    /// Summary description for DocumentationTest
    /// </summary>
    [TestClass]
    public class MSGraphDocumentationTest
    {
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
        }

        [TestCleanup]
        public void TestCleanup()
        {
            GraphBrowser.Goto(GraphBrowser.BaseAddress);
            GraphBrowser.ZoomToPercent(100);
        }

        /// <summary>
        /// Verify whether there is a toggle arrow which work correctly when the window is small.
        /// </summary>
        [TestMethod]
        [Owner("jnlxu")]
        public void Comps_Graph_S04_TC01_CanToggleArrowWorkInSmallDocumentaionPage()
        {
            int currentWidth = 0;
            int currentHeight = 0;
            GraphBrowser.GetWindowSize(out currentWidth, out currentHeight);
            GraphPages.Navigation.Select("Documentation");

            Size windowSize;
            //Set as the screen size of IPad2
            double deviceScreenSize = double.Parse(GraphUtility.GetConfigurationValue("IPad2Size"));
            GraphBrowser.TransferPhysicalSizeToPixelSize(
                deviceScreenSize,
                new Size
                {
                    Width = int.Parse(GraphUtility.GetConfigurationValue("IPad2ScreenResolutionWidth")),
                    Height = int.Parse(GraphUtility.GetConfigurationValue("IPad2ScreenResolutionHeight"))
                },
                out windowSize);
            GraphBrowser.SetWindowSize(windowSize.Width, windowSize.Height);

            Assert.IsTrue(
                GraphUtility.IsToggleArrowDisplayed(),
                "An IPad2 window size ({0} inches) can make table of content arrow appear.",
                deviceScreenSize);
            Assert.IsFalse(GraphUtility.IsMenuContentDisplayed(),
                "When the arrows exists, table of content should be hidden.");

            GraphUtility.ToggleMenu();
            Assert.IsTrue(GraphUtility.IsMenuContentDisplayed(),
                "When the arrows exists and table of content is hidden,clicking the arrow should show table of content.");

            GraphUtility.ToggleMenu();
            Assert.IsFalse(GraphUtility.IsMenuContentDisplayed(),
                "When the arrows exists and table of content is shown,clicking the arrow should hide table of content.");

            //Set as the screen size of IPhone6 plus
            deviceScreenSize = double.Parse(GraphUtility.GetConfigurationValue("IPhone6PlusSize"));
            //Since mobile phone width<Height, invert the output values
            GraphBrowser.TransferPhysicalSizeToPixelSize(
               deviceScreenSize,
               new Size
               {
                   Width = int.Parse(GraphUtility.GetConfigurationValue("IPhone6PlusScreenResolutionWidth")),
                   Height = int.Parse(GraphUtility.GetConfigurationValue("IPhone6PlusScreenResolutionHeight"))
               },
               out windowSize);
            //Since mobile phone widh<height, invert height and width
            GraphBrowser.SetWindowSize(windowSize.Height, windowSize.Width);

            Assert.IsTrue(
                GraphUtility.IsToggleArrowDisplayed(),
                "An IPhone6 Plus window size ({0} inches) can make table of content arrow appear.",
                deviceScreenSize);

            //Recover the window size
            GraphBrowser.SetWindowSize(currentWidth, currentHeight);
        }

        /// <summary>
        /// Verify whether toggle arrow hides when the window is large.
        /// </summary>
        [TestMethod]
        [Owner("jnlxu")]
        public void Acceptance_Graph_S04_TC02_CanToggleArrowHideInLargeDocumentaionPage()
        {
            GraphPages.Navigation.Select("Documentation");
            int currentWidth = 0;
            int currentHeight = 0;
            GraphBrowser.GetWindowSize(out currentWidth, out currentHeight);

            GraphBrowser.ZoomToPercent(50);

            Assert.IsFalse(
                GraphUtility.IsToggleArrowDisplayed(),
                "An large window size ({0} x {1}) can make table of content arrow hide.",
                currentWidth, currentHeight);

            //Change the zoom level back
            GraphBrowser.ZoomToPercent(100);
        }

        ///// <summary>
        ///// Verify whether clicking different subject on Documentation page's
        ///// table of content will show the correct doc content.
        ///// </summary>
        //[TestMethod]
        //public void Comps_Graph_S04_TC03_CanDisplayCorrectContentOnDocumentaionPage()
        //{
        //    GraphBrowser.SetWindowSize(0, 0, true);
        //    GraphPages.Navigation.Select("Documentation");
        //    //If the table of content is replaced by the toggle arrow, click the arrow to display table of content
        //    if (GraphUtility.IsToggleArrowDisplayed())
        //    {
        //        GraphUtility.ToggleMenu();
        //    }

        //    string[] testPaths = { "OVERVIEW>Call the API", "GET STARTED>iOS", "USERS>USER>List users", "WEBHOOKS>SUBSCRIPTION>Get subscription" };
        //    string[] tocPath = { };

        //    for (int i = 0; i < testPaths.Length; i++)
        //    {
        //        tocPath = testPaths[i].Split(new char[] { '>' });

        //        for (int j = 0; j < tocPath.Length; j++)
        //        {
        //            //Avoid to fold the sublayer
        //            if (!GraphUtility.SubLayerDisplayed(tocPath[j]))
        //            {
        //                GraphUtility.Click(tocPath[j]);
        //            }
        //        }
        //        GraphBrowser.Wait(TimeSpan.FromSeconds(int.Parse(GraphUtility.GetConfigurationValue("WaitTime"))));
        //        string docTitle = GraphUtility.GetDocTitle();
        //        bool isCorrectDoc = docTitle != "Document not found.";

        //        Assert.IsTrue(
        //           isCorrectDoc,
        //           @"The shown content is {0} when {1} is chosen in the table of content on Documentation page",
        //           docTitle, tocPath);
        //    }

        //}

        // disabled temporary as Keyur is running an optimizely experiment on "PREVIEW APIS" or "/BETA REFERENCE"
        ///// <summary>
        ///// Verify whether a sub menu can appear by clicking its parent layer.
        ///// </summary>
        //[TestMethod]
        //public void Acceptance_Graph_S04_TC04_CanShowTOCSubLayer()
        //{
        //    GraphPages.Navigation.Select("Documentation");
        //    if (!GraphUtility.IsMenuContentDisplayed())
        //    {
        //        GraphUtility.ToggleMenu();
        //    }

        //    int tocLayerCount = GraphUtility.GetTOCLayer();
        //    //Random generate a layer index, check a menu item at this layer from its top layers          
        //    //Because the last layer menu item doesn't have sub menu, use tocLayerCount-1 as the max value
        //    int index = new Random().Next(tocLayerCount - 1);

        //    List<string> tocPath = GraphUtility.FindTOCParentItems(index);
        //    string itemPath = string.Empty;
        //    for (int j = 0; j < tocPath.Count; j++)
        //    {
        //        GraphUtility.Click(tocPath[j]);
        //        itemPath += (j == 0 ? string.Empty : "->");
        //        itemPath += tocPath[j];
        //        Assert.IsTrue(GraphUtility.SubLayerDisplayed(tocPath[j]), "Clicking {0} can display its sub layer");
        //    }
        //}

        /// <summary>
        /// Verify whether the arrow hide table of content, 
        /// and whether clicking it can hide/show table of content alternatively
        /// </summary>
        private void VerifyArrowAvailability()
        {
            Assert.IsFalse(
                 GraphUtility.IsMenuContentDisplayed(),
                 "Without clicking the toogle arrow, the menu content should not appear.");
            //Click the arrow to show the menu content
            GraphUtility.ToggleMenu();
            Assert.IsTrue(
                 GraphUtility.IsMenuContentDisplayed(),
                 "After clicking the toogle arrow, the menu content should appear.");

            //Click the arrow again to hide the menu content
            GraphUtility.ToggleMenu();
            Assert.IsFalse(
                 GraphUtility.IsMenuContentDisplayed(),
                 "After clicking the toogle arrow for the second time, the menu content should disappear.");
        }

        [TestMethod]
        [Owner("ambate")]
        public void Comps_Graph_S07_TC01_CheckInThisArticle()
        {
            GraphPages.Navigation.Select("Documentation");
            GraphBrowser.ZoomToPercent(50);
            GraphBrowser.Wait(TimeSpan.FromSeconds(3)); //In this article renders after the doc renders, so must wait
            Assert.IsTrue(GraphUtility.IsInThisArticleDisplayed(),
                "On a large screen, in this article should be displayed");
            GraphBrowser.ZoomToPercent(100);

            Size windowSize;
            //Set as the screen size of IPad2
            double deviceScreenSize = double.Parse(GraphUtility.GetConfigurationValue("IPad2Size"));
            GraphBrowser.TransferPhysicalSizeToPixelSize(
                deviceScreenSize,
                new Size
                {
                    Width = int.Parse(GraphUtility.GetConfigurationValue("IPad2ScreenResolutionWidth")),
                    Height = int.Parse(GraphUtility.GetConfigurationValue("IPad2ScreenResolutionHeight"))
                },
                out windowSize);
            GraphBrowser.SetWindowSize(windowSize.Width, windowSize.Height);

            Assert.IsFalse(GraphUtility.IsInThisArticleDisplayed(),
                "On an iPad, in this article should not be displayed");

            //Set as the screen size of IPhone6 plus
            deviceScreenSize = double.Parse(GraphUtility.GetConfigurationValue("IPhone6PlusSize"));
            //Since mobile phone width<Height, invert the output values
            GraphBrowser.TransferPhysicalSizeToPixelSize(
               deviceScreenSize,
               new Size
               {
                   Width = int.Parse(GraphUtility.GetConfigurationValue("IPhone6PlusScreenResolutionWidth")),
                   Height = int.Parse(GraphUtility.GetConfigurationValue("IPhone6PlusScreenResolutionHeight"))
               },
               out windowSize);
            //Since mobile phone widh<height, invert height and width
            GraphBrowser.SetWindowSize(windowSize.Height, windowSize.Width);

            Assert.IsFalse(GraphUtility.IsInThisArticleDisplayed(),
                "On an iPhone, in this article should not be displayed");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Owner("ambate")]
        public void CanTraverseTocAndAccessDocs()
        {
            GraphBrowser.SetWindowSize(0, 0, true);
            GraphPages.Navigation.Select("Documentation");
            //If the table of content is replaced by the toggle arrow, click the arrow to display table of content
            //if (GraphUtility.IsToggleArrowDisplayed())
            //{
            //    GraphUtility.ToggleMenu();
            //}

            //int layerCount = Utility.GetTOCLayer();
            //var failedDocList = Utility.CheckAllDocuments(layerCount);
            var failedDocList = GraphUtility.TraverseTocAndGetFailedDocs();
            Assert.IsTrue(failedDocList.Count == 0, "Failed documents: " + String.Join("; ", failedDocList));
        }

    }
}
