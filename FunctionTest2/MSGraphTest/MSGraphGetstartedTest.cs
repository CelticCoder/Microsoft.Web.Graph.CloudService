using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFramework;
using System;
using TestFramework.Office365Page;

namespace MSGraphTest
{
    /// <summary>
    /// Test Class for Microsoft Get started page
    /// </summary>
    [TestClass]
    public class MSGraphGetstartedTest
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
        }

        [TestMethod]
        [Owner("ambate")]
        public void BVT_Graph_S03_TC01_CanChoosePlatform()
        {
            foreach (Platform item in Enum.GetValues(typeof(Platform)))
            {
                GraphPages.Office365Page.CardSetupPlatform.ChoosePlatform(item);
                Assert.IsTrue(GraphPages.Office365Page.CardSetupPlatform.IsShowingPlatformSetup(item), "Failed to choose platform {0}.", item.ToString());
            }
        }
        
        // should only show first 3 cards on initial page load with no querystring parameters
        // The 3 desired cards: intro, try-it-out, setup
        [TestMethod]
        [Owner("ambate")]
        public void BVT_Graph_S03_TC02_ShowThreeCardsByDefault()
        {
            GraphBrowser.Goto(GraphUtility.RemoveRedundantPartsfromExtractBaseAddress() + "/getting-started");
            Assert.IsTrue(GraphUtility.CheckThreeCardsDisplayed(), "Default cards in Getting Started are not displayed correctly.");
        }

        [TestMethod]
        [Owner("ambate")]
        public void Acceptance_Graph_S03_TC03_TryItOut_GetUsers()
        {
            ServiceToTry service = ServiceToTry.GetUsers;
            GraphPages.Office365Page.CardTryItOut.ChooseService(service);
            int currentWidth = 0;
            int currentHeight = 0;
            GraphBrowser.GetWindowSize(out currentWidth, out currentHeight);
            if (currentWidth > GraphUtility.MinWidthToShowParam)
            {
                foreach (GetUsersValue item in Enum.GetValues(typeof(GetUsersValue)))
                {
                    GraphPages.Office365Page.CardTryItOut.ChooseServiceValue(item);
                    GraphPages.Office365Page.CardTryItOut.ClickTry();
                    Assert.IsTrue(GraphPages.Office365Page.CardTryItOut.CanGetResponse(item), string.Format("The service {0} with parameter {1} is not work.", service.ToString(), item.ToString()));
                    Assert.IsTrue(GraphPages.Office365Page.CardTryItOut.UrlContainsParameterValue(), string.Format("The parameter {0} of service {1} is not contained in the url.", item.ToString(), service.ToString()));
                }
            }
            else
            {
                GraphPages.Office365Page.CardTryItOut.ClickTry();
                Assert.IsTrue(GraphPages.Office365Page.CardTryItOut.CanGetResponse(GetUsersValue.me), string.Format("The service {0} with parameter {1} is not work.", service.ToString(), GetUsersValue.me.ToString()));
                Assert.IsTrue(GraphPages.Office365Page.CardTryItOut.UrlContainsParameterValue(), string.Format("The parameter {0} of service {1} is not contained in the url.", GetUsersValue.me.ToString(), service.ToString()));
            }
        }

        [TestMethod]
        [Owner("ambate")]
        public void Comps_Graph_S03_TC04_TryItOut_GetGroups()
        {
            ServiceToTry service = ServiceToTry.GetGroups;
            GraphPages.Office365Page.CardTryItOut.ChooseService(service);
            int currentWidth = 0;
            int currentHeight = 0;
            GraphBrowser.GetWindowSize(out currentWidth, out currentHeight);
            if (currentWidth > GraphUtility.MinWidthToShowParam)
            {
                foreach (GetGroupValue item in Enum.GetValues(typeof(GetGroupValue)))
                {
                    GraphPages.Office365Page.CardTryItOut.ChooseServiceValue(item);
                    GraphPages.Office365Page.CardTryItOut.ClickTry();
                    Assert.IsTrue(GraphPages.Office365Page.CardTryItOut.CanGetResponse(item), string.Format("The service {0} with parameter {1} is not work.", service.ToString(), item.ToString()));
                    Assert.IsTrue(GraphPages.Office365Page.CardTryItOut.UrlContainsParameterValue(), string.Format("The parameter {0} of service {1} is not contained in the url.", item.ToString(), service.ToString()));
                }
            }
            else
            {
                GraphPages.Office365Page.CardTryItOut.ClickTry();
                Assert.IsTrue(GraphPages.Office365Page.CardTryItOut.CanGetResponse(GetGroupValue.me_memberOf), string.Format("The service {0} with parameter {1} is not work.", service.ToString(), GetGroupValue.me_memberOf.ToString()));
                Assert.IsTrue(GraphPages.Office365Page.CardTryItOut.UrlContainsParameterValue(), string.Format("The parameter {0} of service {1} is not contained in the url.", GetGroupValue.me_memberOf.ToString(), service.ToString()));
            }
        }

        [TestMethod]
        [Owner("ambate")]
        public void Comps_Graph_S03_TC05_TryItOut_GetMessages()
        {
            ServiceToTry service = ServiceToTry.GetMessages;
            GraphPages.Office365Page.CardTryItOut.ChooseService(service);
            int currentWidth = 0;
            int currentHeight = 0;
            GraphBrowser.GetWindowSize(out currentWidth, out currentHeight);
            if (currentWidth > GraphUtility.MinWidthToShowParam)
            {
                foreach (GetMessagesValue item in Enum.GetValues(typeof(GetMessagesValue)))
                {
                    GraphPages.Office365Page.CardTryItOut.ChooseServiceValue(item);
                    GraphPages.Office365Page.CardTryItOut.ClickTry();
                    Assert.IsTrue(GraphPages.Office365Page.CardTryItOut.CanGetResponse(item), string.Format("The service {0} with parameter {1} is not work.", service.ToString(), item.ToString()));
                    Assert.IsTrue(GraphPages.Office365Page.CardTryItOut.UrlContainsServiceName(), string.Format("The name of service {0} is not contained in the url.", service.ToString()));
                    Assert.IsTrue(GraphPages.Office365Page.CardTryItOut.UrlContainsParameterValue(), string.Format("The parameter {0} of service {1} is not contained in the url.", item.ToString(), service.ToString()));
                }
            }
            else
            {
                GraphPages.Office365Page.CardTryItOut.ClickTry();
                Assert.IsTrue(GraphPages.Office365Page.CardTryItOut.CanGetResponse(GetMessagesValue.Inbox), string.Format("The service {0} with parameter {1} is not work.", service.ToString(), GetMessagesValue.Inbox.ToString()));
                Assert.IsTrue(GraphPages.Office365Page.CardTryItOut.UrlContainsServiceName(), string.Format("The name of service {0} is not contained in the url.", service.ToString()));
                Assert.IsTrue(GraphPages.Office365Page.CardTryItOut.UrlContainsParameterValue(), string.Format("The parameter {0} of service {1} is not contained in the url.", GetMessagesValue.Inbox.ToString(), service.ToString()));
            }
        }

        [TestMethod]
        [Owner("ambate")]
        public void Comps_Graph_S03_TC06_TryItOut_GetFiles()
        {
            ServiceToTry service = ServiceToTry.GetFiles;
            GraphPages.Office365Page.CardTryItOut.ChooseService(service);
            int currentWidth = 0;
            int currentHeight = 0;
            GraphBrowser.GetWindowSize(out currentWidth, out currentHeight);
            if (currentWidth > GraphUtility.MinWidthToShowParam)
            {
                foreach (GetFilesValue item in Enum.GetValues(typeof(GetFilesValue)))
                {
                    GraphPages.Office365Page.CardTryItOut.ChooseServiceValue(item);
                    GraphPages.Office365Page.CardTryItOut.ClickTry();
                    Assert.IsTrue(GraphPages.Office365Page.CardTryItOut.CanGetResponse(item), string.Format("The service {0} with parameter {1} is not work.", service.ToString(), item.ToString()));
                    Assert.IsTrue(GraphPages.Office365Page.CardTryItOut.UrlContainsParameterValue(), string.Format("The parameter {0} of service {1} is not contained in the url.", item.ToString(), service.ToString()));
                }
            }
            else
            {
                GraphPages.Office365Page.CardTryItOut.ClickTry();
                Assert.IsTrue(GraphPages.Office365Page.CardTryItOut.CanGetResponse(GetFilesValue.drive_root_children), string.Format("The service {0} with parameter {1} is not work.", service.ToString(), GetFilesValue.drive_root_children.ToString()));
                Assert.IsTrue(GraphPages.Office365Page.CardTryItOut.UrlContainsParameterValue(), string.Format("The parameter {0} of service {1} is not contained in the url.", GetFilesValue.drive_root_children.ToString(), service.ToString()));
            }
        }

        [TestMethod]
        [Owner("ambate")]
        public void Comps_Graph_S03_TC07_TryItOut_GetEvents()
        {
            ServiceToTry service = ServiceToTry.GetEvents;
            GraphPages.Office365Page.CardTryItOut.ChooseService(service);
            GraphPages.Office365Page.CardTryItOut.ClickTry();
            Assert.IsTrue(GraphPages.Office365Page.CardTryItOut.CanGetResponse(null));
            Assert.IsTrue(GraphPages.Office365Page.CardTryItOut.UrlContainsServiceName(), string.Format("The name of service {0} is not contained in the url.", service.ToString()));
        }

        [TestMethod]
        [Owner("ambate")]
        public void Comps_Graph_S03_TC08_TryItOut_GetContacts()
        {
            ServiceToTry service = ServiceToTry.GetContacts;
            GraphPages.Office365Page.CardTryItOut.ChooseService(service);
            GraphPages.Office365Page.CardTryItOut.ClickTry();
            Assert.IsTrue(GraphPages.Office365Page.CardTryItOut.CanGetResponse(null));
            Assert.IsTrue(GraphPages.Office365Page.CardTryItOut.UrlContainsServiceName(), string.Format("The name of service {0} is not contained in the url.", service.ToString()));
        }

        [TestMethod]
        [Owner("ambate")]
        public void Comps_Graph_S03_TC08_ParameterChangedBySwitchingService()
        {
            foreach (ServiceToTry service in Enum.GetValues(typeof(ServiceToTry)))
            {
                GraphPages.Office365Page.CardTryItOut.ChooseService(service);
                bool correctUrl = false;
                bool correctParameter = false;
                switch (service)
                {
                    case ServiceToTry.GetMessages:
                        correctParameter = GraphPages.Office365Page.CardTryItOut.ChooseServiceValue(GetMessagesValue.Inbox);
                        correctUrl = GraphPages.Office365Page.CardTryItOut.UrlContainsServiceName();
                        break;
                    case ServiceToTry.GetEvents:
                    case ServiceToTry.GetContacts:
                        correctParameter = !GraphPages.Office365Page.CardTryItOut.IsParameterTableDisplayed();
                        correctUrl = GraphPages.Office365Page.CardTryItOut.UrlContainsServiceName();
                        break;
                    case ServiceToTry.GetFiles:
                        correctParameter = GraphPages.Office365Page.CardTryItOut.ChooseServiceValue(GetFilesValue.drive_root_children);
                        correctUrl = GraphPages.Office365Page.CardTryItOut.UrlContainsParameterValue();
                        break;
                    case ServiceToTry.GetUsers:
                        correctParameter = GraphPages.Office365Page.CardTryItOut.ChooseServiceValue(GetUsersValue.me);
                        correctUrl = GraphPages.Office365Page.CardTryItOut.UrlContainsParameterValue();
                        break;
                    case ServiceToTry.GetGroups:
                        correctParameter = GraphPages.Office365Page.CardTryItOut.ChooseServiceValue(GetGroupValue.me_memberOf);
                        correctUrl = GraphPages.Office365Page.CardTryItOut.UrlContainsParameterValue();
                        break;
                    default:
                        break;
                }

                Assert.IsTrue(correctParameter, string.Format("The parameter of service {0} is not changed accordingly.", service.ToString()));
                Assert.IsTrue(correctUrl, string.Format("The url in the service {0} is not changed accordingly.", service.ToString()));
            }
        }

        /// <summary>
        /// Since getting started now uses an external app registration, simulation of this process is the best testing option
        /// Fake app registration, then download Node.js rest sample
        /// </summary>
        [TestMethod]
        [Owner("ambate")]
        public void Acceptance_Graph_S03_TC09_CanDownloadCode_Node()
        {
            Platform platform = Platform.Node;
            //Simulate app registration via hardcoded querystring
            GraphBrowser.Goto(GraphUtility.RemoveRedundantPartsfromExtractBaseAddress() + "/getting-started?appID=c4664f74-aec4-4462-93e9-fb84a25d1f28&appName=My%20Node.js%20App&redirectUrl=http://localhost:3000/login&platform=option-node");
            Assert.IsTrue(GraphPages.Office365Page.CardSetupPlatform.IsShowingPlatformSetup(platform), "Failed to choose platform {0}, which should be picked from querystring and selected on page load", platform.ToString());

            GraphPages.Office365Page.CardDownloadCode.DownloadCode();
            Assert.IsTrue(GraphPages.Office365Page.CardDownloadCode.IsCodeDownloaded(), "Failed to download code and display post-download instructions.");
        }

        [TestMethod]
        [Owner("ambate")]
        public void BVT_Graph_S03_TC10_CanLoadGettingStartedPlatformImages()
        {
            //Platform platform = Platform.PHP;
            //GraphPages.Office365Page.CardSetupPlatform.ChoosePlatform(platform);
            GraphBrowser.Goto(GraphUtility.RemoveRedundantPartsfromExtractBaseAddress() + "/getting-started#setup");
            Assert.IsTrue(GraphPages.Office365Page.CanLoadImages());
        }
    }
}
