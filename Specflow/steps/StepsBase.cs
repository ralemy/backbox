using TechTalk.SpecFlow;
using Xamarin.UITest;
using Specflow.Screens;
using Specflow;

namespace Specflow.Steps
{
    public class StepsBase
    {
        protected readonly IHomeScreen homeScreen;
        protected readonly IApp app;

        public StepsBase()
        {
            app = FeatureContext.Current.Get<IApp>("App");
            homeScreen = FeatureContext.Current.Get<IHomeScreen>(ScreenNames.Home);
        }
    }

}