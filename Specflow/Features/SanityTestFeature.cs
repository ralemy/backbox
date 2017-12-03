using Xamarin.UITest;

namespace Specflow.Features
{
    public partial class SanityTestFeature : FeatureBase
    {
        public SanityTestFeature(Platform p, string iOSSimulator, bool resetSim = true) 
            : base(p,iOSSimulator,resetSim)
        {
        }
    }
}
