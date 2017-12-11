using System.Linq;
using Should;
using Specflow.Screens;
using TechTalk.SpecFlow;

namespace Specflow.Steps
{
    [Binding]
    public class ShouldNavigateToAssociationPage: StepsBase
    {
        [Given(@"I am in main page")]
        public void GivenIAmInMainPage()
        {
            app.Query(c=>c.Marked("TheMainPage")).Length.ShouldBeGreaterThan(0);
        }

        [When(@"I tap the '(.*)' button")]
        public void WhenITapTheButton(string associate)
        {
            app.Tap(c=>c.Marked(associate));
        }

        [Then(@"I go to the association page")]
        public void ThenIGoToTheAssociationPage()
        {
            var page = FeatureContext.Current.Get<IAssociationScreen>("IAssociationScreen");
            page.WaitForLoad();
            app.Query(page.PageContainer).Length.ShouldBeGreaterThan(0);
        }

    }
}
