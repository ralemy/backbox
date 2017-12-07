using System;
using Should;
using Specflow.Screens;
using TechTalk.SpecFlow;

namespace Specflow.Steps
{
    [Binding]
    public class ShouldNavigateToBarcodePage : StepsBase
    {
        [Then(@"I go to the barcode page")]
        public void ThenIGoToTheBarcodePage()
        {
            var page = FeatureContext.Current.Get<IBarcodeScreen>();
            page.WaitForLoad();
            app.Query(page.CancelButton).Length.ShouldEqual(1);
        }

    }
}
