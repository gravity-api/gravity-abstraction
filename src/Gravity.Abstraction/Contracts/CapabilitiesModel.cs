using System.Collections.Generic;

namespace Gravity.Abstraction.Contracts
{
    public class CapabilitiesModel
    {
        public IDictionary<string, object> DesiredCapabilities { get; set; }
        public W3Capabilites Capabilities { get; set; }

        public class W3Capabilites
        {
            public IEnumerable<IDictionary<string, object>> FirstMatch { get; set; }
        }
    }
}
