using System;
using associator.ViewModels;
using MVVMFramework;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace associator.Pages
{
    public class BarCodeScanPage : ContentPage
    {
        ZXingScannerView zxing;
        BarcodeOverlay overlay;
        BarcodePageVM ViewModel;

        public static readonly string PageKey = "BarcodeScanPage";

        public BarCodeScanPage() : base()
        {
            ViewModel = Initializer.GetDependency<BarcodePageVM>();
            BindingContext = ViewModel;
            Content = MakeScannerGrid();
            DelegateBaseEvents();
        }

        private void DelegateBaseEvents()
        {
            base.Appearing += (object sender, EventArgs e) => Init();
            base.Disappearing += (object sender, EventArgs e) => Cleanup();
        }

        private void Cleanup()
        {
            zxing.IsScanning = false;
        }

        private void Init()
        {
            zxing.IsScanning = true;
        }

        private View MakeScannerGrid()
        {
            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            grid.Children.Add(InitZXingView());
            grid.Children.Add(InitOverlay());
            grid.BindingContext = ViewModel;
            return grid;
        }

        private View InitOverlay(){
            overlay = new BarcodeOverlay
            {
                ShowFlashButton = zxing.HasTorch,
                BindingContext=ViewModel
            };

            overlay.FlashButtonClicked += (sender, e) =>
                zxing.IsTorchOn = !zxing.IsTorchOn;
                
            overlay.SetBinding(ZXingDefaultOverlay.TopTextProperty, "TopText");
            overlay.SetBinding(ZXingDefaultOverlay.BottomTextProperty, "BottomText");
            overlay.SetBinding(BarcodeOverlay.CancelCommandProperty, "CancelOperation");
            overlay.SetBinding(ZXingDefaultOverlay.FlashCommandProperty, "FlashOperation");
            return overlay;
        }

        private View InitZXingView()
        {
            zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            zxing.SetBinding(ZXingScannerView.IsScanningProperty, "IsScanning");
            zxing.SetBinding(ZXingScannerView.IsAnalyzingProperty, "IsAnalyzing");
            zxing.SetBinding(ZXingScannerView.IsTorchOnProperty, "IsTorchOn");
            zxing.SetBinding(ZXingScannerView.ScanResultCommandProperty, "ScanResultCommand");
            zxing.SetBinding(ZXingScannerView.ResultProperty, "ScanResult");
            zxing.BindingContext = ViewModel;
            ViewModel.ShowFlashButton = zxing.HasTorch;
            return zxing;
        }

    }
}
