using NUnit.Framework;
using Xamarin.UITest;
using TechTalk.SpecFlow;
using Specflow;
using Specflow.Screens;

namespace Specflow.Features
{
    [TestFixture(Platform.Android, "", true)]
    [TestFixture(Platform.iOS, iPhone8._11_1, false)]
//    [TestFixture(Platform.iOS,iPhone8.watch_plus_11_1)]
    public class FeatureBase
    {
        protected static IApp app;
        protected Platform platform;
        protected string iOSSimulator;
        protected bool resetDevice;

        public FeatureBase(Platform platform, string iOSSimulator, bool resetDevice = false)
        {
            this.iOSSimulator = iOSSimulator;
            this.platform = platform;
            this.resetDevice = resetDevice;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform, iOSSimulator, resetDevice);
            FeatureContext.Current.Add("App", app);
            ContextRegister.RegisterScreens(platform,app);
        }
        [TearDown]
        public void AfterEachTest()
        {
            FeatureContext.Current.Clear();
        }
    }

}