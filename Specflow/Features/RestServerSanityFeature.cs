using System;
using NUnit.Framework;
using Specflow.Server;
using TechTalk.SpecFlow;

namespace Specflow.Features
{
    public partial class RestServerSanityFeature: RestServerFeatureBase
    {
        public RestServerSanityFeature(string port):base(port){
            
        }
    }
}
