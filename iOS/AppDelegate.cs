using System;
using System.Collections.Generic;
using System.Linq;
using associator.ViewModels;
using Foundation;
using Newtonsoft.Json.Linq;
using UIKit;

namespace associator.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
#if ENABLE_TEST_CLOUD
            global::Xamarin.Calabash.Start();
#endif

            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        [Export("ExamineSettings:")]
        public NSString ExamineSettings(NSString key)
        {
            switch(key){
                case "UseHttps":
                    return new NSString(Helpers.Settings.UseHttps ? "true" : "false");
                case "SetHttps":
                    Helpers.Settings.UseHttps = true;
                    return new NSString("true");
                case "ClearHttps":
                    Helpers.Settings.UseHttps = false;
                    return new NSString("false");
                default:
                    return new NSString("Unknown key " + key);
            }
        }
    }
}
