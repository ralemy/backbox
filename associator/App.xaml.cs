using MVVMFramework;
using Xamarin.Forms;
using associator.Pages;
using associator.ViewModels;

namespace associator
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            RegisterDependencies();
            MainPage = RegisterPages(new NavigationPage(new MainPage()));
        }

        private Page RegisterPages(NavigationPage page)
        {
            var nav= Initializer.GetDependency<INavigationManager>();
            nav.SetMain(page);
            nav.Register(AssociationPage.PageKey, typeof(AssociationPage));
            nav.Register(BarCodeScanPage.PageKey, typeof(BarCodeScanPage));
            return page;
        }

        private void RegisterDependencies()
        {
            Initializer.SetupDI();
            Initializer.Register<MainPageVM>();
            Initializer.Register<AssociationPageVM>();
            Initializer.Register<BarcodePageVM>();
        }
    }
}
