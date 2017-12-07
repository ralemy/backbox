using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Specflow.Screens
{
    public class BarcodeScreen : IBarcodeScreen
    {
        IApp app;
        public BarcodeScreen(IApp app) => this.app = app;

        public Func<AppQuery, AppQuery> CancelButton { get => c => c.Marked("Barcode_CancelButton"); }

        public IBarcodeScreen WaitForLoad()
        {
            //ToDo: find a way to enusre load
            return this;
        }
    }
}
