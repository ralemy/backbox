using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Specflow.Screens
{
    public class AppScreen
    {
        protected IApp app;
        protected string PageKey = "";
        public Func<AppQuery, AppQuery> PageContainer => c => c.Marked(this.PageKey);
        public AppScreen(IApp app) => this.app = app;
    }
}
