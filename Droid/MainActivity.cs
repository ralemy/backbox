using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.Interop;
using Newtonsoft.Json.Linq;

namespace associator.Droid
{
    [Activity(Label = "associator.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }

        [Export]
        public string SpecflowBackdoor(string json)
        {
            JObject command = JObject.Parse(json);
            switch ((string)command["key"])
            {
                case "SetTarget":
                    Helpers.Settings.TargetURI = (string)command["payload"];
                    return ("Target Set");
                default:
                    return "Unknown key " + (string) command["key"];
            }
        }
    }
}
