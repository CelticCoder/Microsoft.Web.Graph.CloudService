namespace TestFramework
{
    public static class GraphPages
    {
        public static GraphHomePage HomePage
        {
            get
            {
                return new GraphHomePage();
            }
        }

        public static GraphNavigation Navigation
        {
            get
            {
                return new GraphNavigation();
            }
        }

        public static Office365Page.Office365Page Office365Page
        {
            get
            {
                return new Office365Page.Office365Page();
            }
        }
    }
}
