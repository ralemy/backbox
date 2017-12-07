using System;
using Xamarin.UITest.Queries;

namespace Specflow.Screens
{
    public interface IBarcodeScreen
    {
        IBarcodeScreen WaitForLoad();
        Func<AppQuery, AppQuery> CancelButton { get; }

    }
}
