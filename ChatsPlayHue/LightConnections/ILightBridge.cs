using ChatsPlayHue.Light;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatsPlayHue.LightConnections
{
    interface ILightBridge
    {
        IList<ILight> GetLights();
    }
}
