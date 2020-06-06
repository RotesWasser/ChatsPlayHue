using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ChatsPlayHue.LightActions
{
    public class StaticColorLightAction : LightAction
    {
        public Color LightColor { get; private set; }

        public StaticColorLightAction(Color color)
        {
            this.LightColor = color;
        }

        public override void AcceptVisitor(ILightActionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
