using ChatsPlayHue.LightActions;
using ChatsPlayHue.LightActionSource;
using ChatsPlayHue.LightActionSource.DummyLightActionSource;
using ChatsPlayHue.LightConnections;
using ChatsPlayHue.LightConnections.PhilipsHue;
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

            // Technologies
            container.Register<ILightTechnologyConnector, PhilipsHueConnector>(Lifestyle.Singleton);
            // container.Collection.Append<ILightTechnologyConnection, OtherConnector>(Lifestyle.Singleton);

            container.Verify();
        }

        static async Task Main(string[] args)
        {
            var actionSource = container.GetInstance<ILightActionSource>();

            actionSource.Subscribe(new SimpleLightActionObserver());

            actionSource.Start();

            Console.Read();
        }

    }
}
