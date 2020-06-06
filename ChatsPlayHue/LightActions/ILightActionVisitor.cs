using System;
using System.Collections.Generic;
using System.Text;

namespace ChatsPlayHue.LightActions
{
    public interface ILightActionVisitor
    {
        public void Visit(PatternLightAction action);
        public void Visit(StaticColorLightAction action);
        public void Visit(ToggleLightAction action);
    }
}
