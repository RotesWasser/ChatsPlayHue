using ChatsPlayHue.Light;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatsPlayHue.LightActions
{
    public class ToggleLightAction : LightAction
    {
        public LightPowerState NewState {get; private set;}

        public ToggleLightAction(LightPowerState newState)
        {
            NewState = newState;
        }

        public override void AcceptVisitor(ILightActionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
