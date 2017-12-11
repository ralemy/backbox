using Grapevine.Interfaces.Server;
using Grapevine.Server;
using Grapevine.Shared;
using RestSharp;
using Should;
using Specflow.Server;
using TechTalk.SpecFlow;

namespace Specflow.Steps
{
    [Binding]
    public class ShouldStartAndStopRestServer
    {
        private RestTestServer RestServer;

        [Given(@"I have a server set up")]
        public void GivenIHaveAServerSetUp()
        {
            RestServer = FeatureContext.Current.Get<RestTestServer>("RestTestServer");
            RestServer.ShouldNotBeNull();
        }

        [Given(@"I have added a '(.*)' route to return '(.*)'")]
        public void GivenIHaveAddedARouteToReturn(string endpoint, string response)
        {
            RestServer.Register(
                delegate (IHttpContext context)
                {
                    context.Response.SendResponse(response);
                    return context;
                },
                HttpMethod.GET,
                endpoint
            );
        }

        [When(@"I call the '(.*)' endpoint")]
        public void WhenICallTheEndpoint(string p0)
        {
            ScenarioContext.Current.Add("returnvalue", RestServer.Get(p0));
            RestServer.GetLocalIP().ShouldEqual("ddfdsfsd");
        }


        [Then(@"the result should be a '(.*)' message")]
        public void ThenTheResultShouldBeAMessage(string p0)
        {
            ScenarioContext.Current.Get<string>("returnvalue").ShouldEqual(p0);
        }

    }
}
