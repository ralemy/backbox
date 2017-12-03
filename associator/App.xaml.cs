using MVVMFramework;
using Xamarin.Forms;
using associator.Pages;
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
            return page;
        }

        private void RegisterDependencies()
        {
            Initializer.SetupDI();
            Initializer.Register<MainPageVM>();
        }
    }
}
