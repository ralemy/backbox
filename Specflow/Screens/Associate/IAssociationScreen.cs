using System;
using Xamarin.UITest.Queries;

namespace Specflow.Screens
{
    public interface IAssociationScreen
    {
        IAssociationScreen WaitForLoad();
        Func<AppQuery, AppQuery> PageContainer { get; }
        Func<AppQuery, AppQuery> PageLabel { get; }
    }
}
