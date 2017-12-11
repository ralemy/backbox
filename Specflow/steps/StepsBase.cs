using TechTalk.SpecFlow;
using Xamarin.UITest;
using Specflow.Screens;
using Specflow;

namespace Specflow.Steps
{
    public class StepsBase
    {
        protected readonly IApp app;

        public StepsBase()
        {
            app = FeatureContext.Current.Get<IApp>("App");
        }

        public string Invoke(string id, string param)
        {
            if (app is Xamarin.UITest.Android.AndroidApp)
                return app.Invoke(id, param).ToString();
            return app.Invoke(id + ":", param).ToString();
        }


    }

}