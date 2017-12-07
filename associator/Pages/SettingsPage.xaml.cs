using System;
using System.Collections.Generic;
using associator.ViewModels;
using MVVMFramework;
using Xamarin.Forms;

namespace associator.Pages
{
    public partial class SettingsPage : ContentPage
    {
        public static readonly string PageKey = "SettingsPage";
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = Initializer.GetDependency<SettingsPageVM>();
        }
    }
}
