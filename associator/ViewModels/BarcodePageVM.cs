using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace associator.ViewModels
{
    public class BarcodePageVM : ViewModelBase
    {
        private string _TopText = "Initial TopText";
        public String TopText
        {
            get => _TopText;
            set => Set(() => TopText, ref _TopText, value);
        }

        private string _BottomText = "Initial BottomText";
        public String BottomText
        {
            get => _BottomText;
            private set => Set(() => BottomText, ref _BottomText, value);
        }

        private bool _IsTorchOn;
        public bool IsTorchOn
        {
            get => _IsTorchOn;
            set => Set(() => IsTorchOn, ref _IsTorchOn, value);
        }

        private bool _IsScanning = true;
        public bool IsScanning
        {
            get => _IsScanning;
            set => Set(() => IsScanning, ref _IsScanning, value);
        }

        private bool _IsAnalyzing = true;
        public bool IsAnalyzing
        {
            get => _IsAnalyzing;
            set => Set(() => IsAnalyzing, ref _IsAnalyzing, value);
        }

        private bool _ShowFlashButton = true;
        public bool ShowFlashButton
        {
            get => _ShowFlashButton;
            set => Set(() => ShowFlashButton, ref _ShowFlashButton, value);
        }

        private Result _scanResult;
        public Result ScanResult
        {
            get => _scanResult;
            set => Set(() => ScanResult, ref _scanResult, value);
        }


        private INavigationService navigator;

        public ICommand ScanResultCommand { get; private set; }
        public ICommand CancelOperation { get; private set; }
        public ICommand FlashOperation { get; private set; }

        public BarcodePageVM(INavigationService navigator)
        {
            this.navigator = navigator;
            ScanResultCommand = new RelayCommand(OnScanResult);
            CancelOperation = new RelayCommand(OperationCanceled);
            FlashOperation = new RelayCommand(OperationFlash);
        }

        private void OperationFlash()
        {
        }

        private void OperationCanceled()
        {
            navigator.GoBack();
        }

        public void OnScanResult()
        {

        }
    }
}
