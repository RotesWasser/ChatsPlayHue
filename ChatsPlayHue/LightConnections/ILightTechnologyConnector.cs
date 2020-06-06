using System;
using System.Collections.Generic;
using System.Text;

namespace ChatsPlayHue.LightConnections
{
    interface ILightTechnologyConnector
    {
        IList<ILightBridge> GetBridges();
    }
}
