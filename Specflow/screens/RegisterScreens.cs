using System;
using TechTalk.SpecFlow;
using Xamarin.UITest;

namespace Specflow.Screens
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

        public static void RegisterScreens(Platform platform, IApp app)
        {
            FeatureContext.Current.Set<IAssociationScreen>(new AssociationScreen(app));
            FeatureContext.Current.Set<IBarcodeScreen>(new BarcodeScreen(app));
            RegisterScreens(platform);
        }
    }
}
