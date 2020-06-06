using ChatsPlayHue.LightActions;
using ChatsPlayHue.LightActionSource;
using ChatsPlayHue.LightActionSource.DummyLightActionSource;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Threading.Tasks;

namespace ChatsPlayHue
{
    class Program
    {
        private static readonly Container container;

        static Program()
        {
            container = new Container();

            container.Register<ILightActionSource, DummyLightActionSource>(Lifestyle.Singleton);

            container.Verify();
        }



        static async Task Main(string[] args)
        {
            var actionSource = container.GetInstance<ILightActionSource>();

            actionSource.Subscribe(new SimpleObserver());

            actionSource.Start();

            Console.ReadKey();
        }

        private class SimpleObserver : IObserver<LightAction>, ILightActionVisitor
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
}
