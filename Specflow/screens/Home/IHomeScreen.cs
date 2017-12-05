using System;
using Xamarin.UITest.Queries;

namespace Specflow.Screens
{
    public interface IHomeScreen 
    {
        Func<AppQuery, AppQuery> addButton { get; }
    }
}