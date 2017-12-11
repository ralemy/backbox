using Grapevine.Interfaces.Server;
using Grapevine.Server;
using Grapevine.Shared;
using Newtonsoft.Json.Linq;
using Should;
using Specflow.Server;
using TechTalk.SpecFlow;
using Xamarin.UITest;

namespace Specflow.Steps
{
    [Binding]
    public class ShouldSendDataToRestServer : StepsBase
    {
        private RestTestServer RestServer;
        private HttpMethod calledMethod = HttpMethod.ALL;
        private string restEndpoint;

        [Given(@"The REST Server is running")]
        public void GivenTheRESTServerIsRunning()
        {
            RestServer = FeatureContext.Current.Get<RestTestServer>("RestTestServer");
            RestServer.Get("/").ShouldEqual("Server Up");
        }
        [Given(@"I have added a '(.*)' endpoint to return '(.*)'")]
        public void GivenIHaveAddedAEndpointToReturn(string endpoint, string result)
        {
            RestServer.Register(delegate (IHttpContext context)
            {
                calledMethod = context.Request.HttpMethod;
                context.Response.SendResponse(result);
                return context;
            }, HttpMethod.GET, endpoint);
            restEndpoint = endpoint; 
        }

        [Given(@"I have configured the app for use of REST server endpoint")]
        public void GivenIHaveConfiguredTheAppForUseOfRESTServerEndpoint()
        {
            var target = RestServer.GetUrl(restEndpoint);
            var command = new JObject(new JProperty("key","SetTarget"),
                                      new JProperty("payload", target));
            Invoke("SpecflowBackdoor", command.ToString()).ShouldEqual("Target Set");
        }

        [Given(@"Am in the main page")]
        public void GivenAmInTheMainPage()
        {
            app.Query(c => c.Marked("TheMainPage")).Length.ShouldBeGreaterThan(0);
        }

        [When(@"I press Send data button")]
        public void WhenIPressSendDataButton()
        {
            app.Tap(c => c.Marked("CallApiButton"));
        }
        [Then(@"The server will be called at the endpoint")]
        public void ThenTheServerWillBeCalledAtTheEndpoint()
        {
            app.WaitFor(
                () => calledMethod != HttpMethod.ALL,
                "Didn't Call the endpoint"
            );
            calledMethod.ShouldEqual(HttpMethod.GET);
        }


    }
}
