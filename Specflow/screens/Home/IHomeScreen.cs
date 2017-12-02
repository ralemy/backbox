using System;
using Xamarin.UITest.Queries;

namespace SpecFlow.screens
{
    public interface IHomeScreen
    {
        Func<AppQuery, AppQuery> addButton { get; }
    }
}