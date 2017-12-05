using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Specflow.Screens
{
    public class AssociationScreen :IAssociationScreen
    {
        private IApp app;
        private static readonly string ID_AssociationPageLabel = "AssociationPageLabel";
        private static readonly string ID_AssociationPage = "AssociationPage";

        public AssociationScreen(IApp app) => this.app = app;

        public Func<AppQuery, AppQuery> PageLabel {
            get => c => c.Marked(ID_AssociationPageLabel);
        }
        public Func<AppQuery, AppQuery> PageContainer {
            get => c => c.Marked(ID_AssociationPage);
        }

        public IAssociationScreen WaitForLoad()
        {
            app.WaitForElement(PageLabel);
            return this;
        }
    }
}

