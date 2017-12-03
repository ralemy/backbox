using System;
using System.Collections.Generic;

using Xamarin.Forms;
using MVVMFramework;
namespace associator.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = Initializer.GetDependency<MainPageVM>();
        }
    }
}
