using System;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Specflow.Server
{
    [TestFixture("3434")]
    public class RestServerFeatureBase
    {
        protected string Port;
        public RestTestServer RestServer;

        public RestServerFeatureBase(string port)
        {
            Port = port;
            RestServer = new RestTestServer(Port);
        }

        [TestFixtureSetUp]
        public void RegisterWithFeatureContext()
        {
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
