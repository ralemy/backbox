using System;

using Should;
using Xamarin.UITest;
using TechTalk.SpecFlow;

namespace Specflow.Steps
{
    [Binding]
    public class ShouldBeAbleToStartAssociation : StepsBase
    {
        [Given(@"I am running the app")]
        public void GivenIAmRunningTheApp()
        {
           app.ShouldNotBeNull();
        }

        [When(@"I examine the main page")]
        public void WhenIExamineTheMainPage()
        {
//            app.Query(c=>c.Marked("TheMainPage")).Length.ShouldBeGreaterThan(0);
            app.Repl();
        }

        [Then(@"It has a button marked as '(.*)'")]
        public void ThenItHasAButtonMarkedAs(string associate)
        {
            app.Query(c=>c.Text(associate)).Length.ShouldEqual(1);

        }

   }
}
