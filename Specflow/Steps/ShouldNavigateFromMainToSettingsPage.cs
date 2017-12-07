using System;
using Should;
using Specflow.Screens;
using TechTalk.SpecFlow;

namespace Specflow.Steps
{
    [Binding]
    public class ShouldNavigateFromMainToSettingsPage : StepsBase
    {
        public ISettingsScreen page { get; private set; }

        [Given(@"I am in Main Page")]
        public void GivenIAmInMainPage()
        {
            app.Query(c => c.Marked("TheMainPage")).Length.ShouldEqual(1);
        }

        [Given(@"There is a '(.*)' button on the page")]
        public void GivenThereIsAButtonOnThePage(string settings)
        {
            app.Query(c=>c.Text(settings)).Length.ShouldEqual(1);
        }

        [When(@"I press the '(.*)' button")]
        public void WhenIPressTheButton(string settings0)
        {
            app.Tap(c=>c.Text(settings0));
        }

        [Then(@"the application will Navigate to Settings Page")]
        public void ThenTheApplicationWillNavigateToSettingsPage()
        {
            page = FeatureContext.Current.Get<ISettingsScreen>();
            page.WaitForLoad();
            app.Query(page.PageContainer).Length.ShouldEqual(1);
        }


    }
}
