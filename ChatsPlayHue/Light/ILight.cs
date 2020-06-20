using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace ChatsPlayHue.Light
{
    public interface ILight
    {
        Task<Color> GetLightColor();        
        Task SetLightColor(Color newColor);

        Task<LightPowerState> GetPowerState();
        Task SetPowerState(LightPowerState newState);

        string Name { get; }
    }
}
