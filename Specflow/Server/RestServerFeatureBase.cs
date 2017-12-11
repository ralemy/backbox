using System;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Specflow.Server
{
    [TestFixture("3434")]
    public class RestServerFeatureBase
    {
        protected string Port;
        protected RestTestServer RestServer;

        public RestServerFeatureBase(string port) => Port = port;

        [TestFixtureSetUp]
        public void RegisterWithFeatureContext()
        {
            RestServer = new RestTestServer(Port);
            RestServer.LogToConsole().Start();
        }
        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            RestServer.Stop();
        }
        [SetUp]
        public void RegisterServerWithContext(){
            FeatureContext.Current.Set<RestTestServer>(RestServer, "RestTestServer");
        }

        [TearDown]
        public void UnRegisterServerFromContext(){
            FeatureContext.Current.Remove("RestTestServer");
        }
    }
}
