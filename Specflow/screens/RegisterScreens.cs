using TechTalk.SpecFlow;
using Xamarin.UITest;

namespace SpecFlow.screens
{
    public static class ContextRegister    {
        public static void RegisterScreens(Platform platform){
            if (platform == Platform.Android)
                RegisterAndroidScreens();
            else
                RegisterIOSScreens();
        }

        private static void RegisterIOSScreens()
        {
            FeatureContext.Current.Add(ScreenNames.Home,new iOSHomeScreen());
        }

        private static void RegisterAndroidScreens()
        {
            FeatureContext.Current.Add(ScreenNames.Home, new AndroidHomeScreen());
        }
    }
}
