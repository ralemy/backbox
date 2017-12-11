using System;
using NUnit.Framework;
using Specflow.Server;
using TechTalk.SpecFlow;
using Xamarin.UITest;

namespace Specflow.Features
{
    public partial class RestApiFeature : FeatureBase
    {
        private RestTestServer RestServer;

        public RestApiFeature(Platform p, string iOSSim, bool reset)
            : base(p, iOSSim, reset)
        {
            var Serverbase = new RestServerFeatureBase("3434");
            RestServer = Serverbase.RestServer;
        }

        [TestFixtureSetUp]
        public void StartRestServer()
        {
            RestServer.LogToConsole().Start();
        }
        [TestFixtureTearDown]
        public void StopRestServer(){
            RestServer.Stop();
        }

        [SetUp]
        public void AddServerToContext()
        {
            FeatureContext.Current.Add("RestTestServer", RestServer);
        }

        [TearDown]
        public void RemoveServerFromContext()
        {
            FeatureContext.Current.Remove("RestTestServer");
        }
    }
}
