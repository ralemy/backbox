using System;
using Should;
using Specflow.Screens;
using TechTalk.SpecFlow;

namespace Specflow.Steps
{
    [Binding]
    public class ShouldReturntoMainPageIfCancelIsTapped : StepsBase
    {
        IBarcodeScreen page;

        public ShouldReturntoMainPageIfCancelIsTapped() : base()
        {
            page = FeatureContext.Current.Get<IBarcodeScreen>();
        }

        [Given("I am in barcode page")]
        public void GivenIamInBarcodePage()
        {
            app.Query(page.CancelButton).Length.ShouldEqual(1);
        }

        [When("I tap the cancel button")]
        public void WhenIPressAdd()
        {
            app.Tap(page.CancelButton);
        }

        [Then(@"I go to Main page")]
        public void ThenIGoToMainPage()
        {
            app.WaitForElement(c => c.Marked("TheMainPage"));
        }
    }
}
