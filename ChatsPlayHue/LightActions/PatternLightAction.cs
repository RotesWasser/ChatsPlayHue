using System;
using System.Collections.Generic;
using System.Text;

namespace ChatsPlayHue.LightActions
{
    public class PatternLightAction : LightAction
    {
        public uint ID { get; private set; }

        public PatternLightAction(uint ID)
        {
            this.ID = ID;
        }

        public override void AcceptVisitor(ILightActionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
