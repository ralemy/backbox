using Xamarin.Forms;
using MVVMFramework;
namespace associator.Pages
{
    public partial class MainPage : ContentPage
    {
        public static readonly string PageKey = "TheMainPage";
        public MainPage()
        {
            InitializeComponent();
            BindingContext = Initializer.GetDependency<MainPageVM>();
        }
    }
}
