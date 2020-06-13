using System;
using System.Collections.Generic;
using System.Text;

using ChatsPlayHue.Light;

namespace ChatsPlayHue.LightConnections
{
    interface ILightTechnologyConnector
    {
        IList<ILightBridge> GetBridges();
    }
}
