using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ChatsPlayHue.Light
{
    interface ILight
    {
        Color LightColor { get; set; }
        LightPowerState PowerState { get; set; }

        string Name { get; }
    }
}
