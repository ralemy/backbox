using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Specflow.Screens
{
    public class SettingsScreen : AppScreen, ISettingsScreen
    {
        public SettingsScreen(IApp app) : base(app)
        {
            PageKey = "SettingsPage";
        }

        public Func<AppQuery, AppQuery> UseHttpsSwitch
        {
            get
            {
                if (app is Xamarin.UITest.Android.AndroidApp)
                    return c => c.Text("Use Https").Parent(1).Child(1);
                else 
                    return c => c.Marked("Use Https").Index(1);
            }
        }

        public ISettingsScreen WaitForLoad()
        {
            app.WaitForElement(PageContainer);
            return this;
        }
    }
}
