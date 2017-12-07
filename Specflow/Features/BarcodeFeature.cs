using System;
using Xamarin.UITest;

namespace Specflow.Features
{
    public partial class BarcodeFeature : FeatureBase
    {
        public BarcodeFeature(Platform p, string iOSSimulator, bool resetSim)
            : base(p, iOSSimulator, resetSim)
        {
        }
    }
}
