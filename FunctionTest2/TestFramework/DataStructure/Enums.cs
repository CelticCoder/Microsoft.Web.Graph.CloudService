using System;
using System.ComponentModel;
using System.Reflection;

namespace TestFramework
{
    public enum SliderMenuItem
    {
        [Description("Get Started")]
        GetStarted,
        News,
        Opportunity,
        Transform,
        [Description("Featured App")]
        FeaturedApp
    }

    public enum ServiceToTry
    {
        [Description("messages")]
        GetMessages,
        [Description("events")]
        GetEvents,
        [Description("contacts")]
        GetContacts,
        GetFiles,
        GetUsers,
        GetGroups
    }

    public enum GetMessagesValue
    {
        Inbox,
        SentItems,
        Drafts,
        DeletedItems
    }

    public enum GetFilesValue
    {
        drive_root_children,
        me_drive
    }

    public enum GetUsersValue
    {
        me,
        me_select_skills,
        me_manager,
        myOrganization_users
    }

    public enum GetGroupValue
    {
        me_memberOf,
        members,
        drive_root_children,
        conversations
    }

    public enum OfficeAppItem
    {
        Word,
        Excel,
        PowerPoint,
        Access,
        Project,
        OneDrive,
        OneNote,
        Outlook,
        SharePoint,
        Skype,
        Yammer
    }

    public enum MenuItemOfResource
    {
        [Description("Patterns and Practices")]
        PatternsAndPractices,
        Events,
        Podcasts,
        Training,
        [Description("Graph Explorer")]
        GraphExplorer,
        [Description("Videos")]
        Videos
    }

    public enum MenuItemOfDocumentation
    {
        [Description("Office UI Fabric")]
        OfficeUIFabricGettingStarted,
        [Description("Office Add-ins")]
        OfficeAddin,
        [Description("Office Add-in Availability")]
        OfficeAddinAvailability,
        [Description("SharePoint Add-ins")]
        SharePointAddin,
        [Description("SharePoint Framework")]
        SharePointFramework,
        [Description("Microsoft Graph API")]
        MicrosoftGraphAPI,
        [Description("Office 365 REST APIs")]
        Office365RESTAPIs,
        [Description("All Documentation")]
        AllDocumentation
    }

    public enum Platform
    {
        Android,
        Angular,
        [Description("ASP.NET MVC")]
        DotNET,
        [Description("iOS Swift")]
        iOS_Swift,
        [Description("iOS Objective C")]
        iOS_Objective_C,
        [Description("Node.js")]
        Node,
        PHP,
        Ruby,
        [Description("Universal Windows Platform")]
        WindowsUniversal,
        Xamarin
    }


    /// <summary>
    /// The sort types
    /// </summary>
    public enum SortType
    {
        /// <summary>
        /// Sort by view count
        /// </summary>
        ViewCount,
        
        /// <summary>
        /// Sort by date
        /// </summary>
        Date
    }

    /// <summary>
    /// The deployment slots enumberation
    /// </summary>
    public enum DeploymentSlot
    {
        [Description("https://msgraph-staging-devx.azurewebsites.net")]
        Devx,
        [Description("http://msgraph-prod-lastgood.azurewebsites.net/")]
        LastGood,
        [Description("http://msgraph-prod-mirror.azurewebsites.net/")]
        Mirror,
        [Description("https://graph.microsoft.io")]
        Production
    }

    public static class EnumExtension
    {
        public static string GetDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }

            else
            {
                return value.ToString();
            }
        }
    }
}
