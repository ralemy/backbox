using System;
using GalaSoft.MvvmLight;
using MVVMFramework;
using associator.Helpers;
using System.ComponentModel;

namespace associator.ViewModels
{
    public class SettingsPageVM : ViewModelBase
    {
        public bool UseHttps{
            get => Settings.UseHttps;
            set
            {
                if (Settings.UseHttps == value) return;
                Settings.UseHttps = value;
                RaisePropertyChanged("UseHttps");
            }
        }


        private INavigationManager navigator;
        public SettingsPageVM(INavigationManager navigator) : base()
        {
            this.navigator = navigator;
        }
    }
}
