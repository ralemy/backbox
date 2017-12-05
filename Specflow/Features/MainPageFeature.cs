using System;
using Xamarin.UITest;

namespace Specflow.Features
{
    public partial class MainPageFeature : FeatureBase
    {
        public MainPageFeature(Platform p, string iOSSimulator, bool resetSim)
            :base(p,iOSSimulator,resetSim)
        {
        }
    }
}
