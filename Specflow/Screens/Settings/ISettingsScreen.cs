using System;
using Xamarin.UITest.Queries;

namespace Specflow.Screens
{
    public interface ISettingsScreen : IAppScreen<ISettingsScreen>
    {
        Func<AppQuery, AppQuery> UseHttpsSwitch { get;}
    }
}
