using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace NewsBagUITests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android.InstalledApp("com.companyname.newsbag").PreferIdeSettings().StartApp();
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}