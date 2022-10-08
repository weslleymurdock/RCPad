using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace RCPad.UITests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android.StartApp();
            }
            
            // Workaround for iOS simulator reset bug
            Environment.SetEnvironmentVariable("UITEST_FORCE_IOS_SIM_RESTART", "1");

            return ConfigureApp.iOS.StartApp();
        }
    }
}