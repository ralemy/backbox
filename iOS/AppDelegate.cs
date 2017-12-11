using System;
using System.Collections.Generic;
using System.Linq;

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
        [Export("SpecflowBackdoor:")]
        public NSString SpecflowBackdoor(NSString json)
        {
            JObject command = JObject.Parse(json.ToString());
            switch ((string)command["key"])
            {
                case "SetTarget":
                    Helpers.Settings.TargetURI = (string)command["payload"];
                    return new NSString("Target Set");
                default:
                    return new NSString("Unknown key " + (string)command["key"]);
            }
        }
    }
}
