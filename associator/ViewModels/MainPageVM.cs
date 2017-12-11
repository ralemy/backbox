using System.Windows.Input;
using associator.Pages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace associator
{
    public class MainPageVM : ViewModelBase
    {
        private readonly INavigationService navigator;
        public ICommand GoForAssociation { get; private set; }
        public ICommand CallApiCommand { get; private set; }

        public MainPageVM(INavigationService navigator){
            this.navigator = navigator;
            this.GoForAssociation = new RelayCommand(_GoForAssociation);
            CallApiCommand = new RelayCommand(_CallApi);
        }
        private void _CallApi(){
            var client = new RestSharp.RestClient(Helpers.Settings.TargetURI);
            client.Execute(new RestSharp.RestRequest());
        }

        private void _GoForAssociation(){
            navigator.NavigateTo(AssociationPage.PageKey);
        }

    }
}