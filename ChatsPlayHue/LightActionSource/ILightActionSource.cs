using System;
using System.Collections.Generic;
using System.Text;
using ChatsPlayHue.LightActions;
using ChatsPlayHue.LightActionSource;

namespace ChatsPlayHue.LightActionSource
{
    interface ILightActionSource : IObservable<LightAction>
    {
        void Start();
        void Stop();
    }
}
