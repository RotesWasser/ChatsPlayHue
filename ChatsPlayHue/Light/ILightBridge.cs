using ChatsPlayHue.Light;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatsPlayHue.Light
{
    interface ILightBridge
    {
        Task<IList<ILight>> GetLights();

        Task Connect(); 
    }
}
