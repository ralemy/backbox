using System;
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
        public ICommand GoForBarcode { get; private set; }

        public MainPageVM(INavigationService navigator){
            this.navigator = navigator;
            this.GoForAssociation = new RelayCommand(_GoForAssociation);
            this.GoForBarcode = new RelayCommand(_GoForBarcode);
        }

        private void _GoForBarcode()
        {
            navigator.NavigateTo(BarCodeScanPage.PageKey);
        }

        private void _GoForAssociation(){
            navigator.NavigateTo(AssociationPage.PageKey);
        }

    }
}