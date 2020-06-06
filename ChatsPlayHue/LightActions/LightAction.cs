using System;
using System.Collections.Generic;
using System.Text;

namespace ChatsPlayHue.LightActions
{
    public abstract class LightAction
    {
        public abstract void AcceptVisitor(ILightActionVisitor visitor);
    }
}
