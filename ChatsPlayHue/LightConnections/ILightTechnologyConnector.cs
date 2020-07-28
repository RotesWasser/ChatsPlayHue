using System;
using System.Collections.Generic;
using System.Text;

using ChatsPlayHue.Light;

namespace ChatsPlayHue.Renderers
{
    interface ILightTechnologyConnector
    {
        IList<IRenderer> GetBridges();
    }
}
