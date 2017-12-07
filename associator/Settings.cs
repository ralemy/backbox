using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace associator.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings{
            get => CrossSettings.Current;
        }
        public static bool UseHttps{
            get => AppSettings.GetValueOrDefault(nameof(UseHttps), false);
            set => AppSettings.AddOrUpdateValue(nameof(UseHttps), value);
        }
    }
}
