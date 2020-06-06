using System;
using System.Collections.Generic;
using System.Text;

namespace ChatsPlayHue.LightConnections.PhilipsHue
{
    class PhilipsHueConnector : ILightTechnologyConnector
    {
        public PhilipsHueConnector()
        {

        }

        public IList<ILightBridge> GetBridges()
        {
            throw new NotImplementedException();
        }
    }
}
