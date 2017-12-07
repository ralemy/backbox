using System;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace associator.Pages
{
    public class BarcodeOverlay : ZXingDefaultOverlay
    {
        Button _Cancel;
        public delegate void CancelButtonClickedDelegate(Button sender, EventArgs e);
        public event CancelButtonClickedDelegate CancelButtonClicked;

        public BarcodeOverlay() : base()
        {
            CreateAndAddComponents();
        }

        private void CreateAndAddComponents()
        {
            _Cancel = new Button
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Start,
                Text = "Cancel",
                TextColor = Color.Azure,
                AutomationId = "Barcode_CancelButton",
            };
            _Cancel.Clicked += (sender, e) =>
            {
                CancelButtonClicked?.Invoke(_Cancel, e);
                if (CancelCommand != null)
                    if (CancelCommand.CanExecute(null))
                        CancelCommand.Execute(null);
            };
            Children.Add(_Cancel, 0, 2);

        }

        public static BindableProperty CancelCommandProperty =
            BindableProperty.Create(nameof(CancelCommand), typeof(ICommand), typeof(BarcodeOverlay),
                defaultValue: default(ICommand),
                propertyChanged: OnCancelCommandChanged);
        public ICommand CancelCommand
        {
            get { return (ICommand)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }

        private static void OnCancelCommandChanged(BindableObject bindable, object oldvalue, object newValue)
        {
            var overlay = bindable as BarcodeOverlay;
            if (overlay?._Cancel == null) return;
            overlay._Cancel.Command = newValue as Command;
        }
    }
}
