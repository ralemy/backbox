using System;
using Xamarin.UITest.Queries;

namespace Specflow.Screens
{
    public interface IAppScreen<T>
    {
        T WaitForLoad();
        Func<AppQuery, AppQuery> PageContainer { get; }
    }
}
