using ChatsPlayHue.LightActions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatsPlayHue
{
    class SimpleLightActionObserver : IObserver<LightAction>, ILightActionVisitor
    {
        public void OnCompleted()
        {
            Console.WriteLine("Completed!");
        }

        public void OnError(Exception error)
        {
            throw error;
        }

        public void OnNext(LightAction value)
        {
            value.AcceptVisitor(this);
        }

        public void Visit(PatternLightAction action)
        {
            Console.WriteLine(string.Format("Pattern requested: {0}", action.ID));
        }

        public void Visit(StaticColorLightAction action)
        {
            Console.WriteLine(string.Format("Color requested: {0}", action.LightColor));
        }

        public void Visit(ToggleLightAction action)
        {
            Console.WriteLine(string.Format("Toggle requested: {0}", action.NewState));
        }
    }
}
