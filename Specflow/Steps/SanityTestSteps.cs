using Should;
using TechTalk.SpecFlow;

namespace Specflow.Steps
{
    [Binding]
    public class SanityTestSteps
    {
        [Given(@"I store (.*) into the feature context under key '(.*)'")]
        public void GivenIStoreIntoTheFeatureContextUnderKey(int p0, string sanityTest)
        {
            FeatureContext.Current.Add(sanityTest,p0);
        }

        [When(@"I read the key '(.*)' from the context and add (.*) to it and store it in '(.*)'")]
        public void WhenIReadTheKeyFromTheContextAndAddToItAndStoreItIn(string sanityTest0, int p1, string sanityWhen)
        {
            FeatureContext.Current.Add(sanityWhen,FeatureContext.Current.Get<int>(sanityTest0) + 10);
        }

        [Then(@"the '(.*)' key in context should be (.*)\.")]
        public void ThenTheKeyInContextShouldBe_(string sanityWhen0, int p1)
        {
            FeatureContext.Current.Get<int>(sanityWhen0).ShouldEqual(p1);
        }

    }
}
