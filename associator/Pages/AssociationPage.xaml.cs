using associator.ViewModels;
using MVVMFramework;
using Xamarin.Forms;

namespace associator.Pages
{
    public partial class AssociationPage : ContentPage
    {
        public static readonly string PageKey = "AssociationPage";
        public AssociationPage()
        {
            InitializeComponent();
            BindingContext = Initializer.GetDependency<AssociationPageVM>();
        }
    }
}
