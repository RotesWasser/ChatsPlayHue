using ChatsPlayHue.LightActions;
using ChatsPlayHue.LightActionSource;
using ChatsPlayHue.LightActionSource.DummyLightActionSource;
using ChatsPlayHue.Renderers;
using ChatsPlayHue.Renderers.PhilipsHue;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using ChatsPlayHue.Light;

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
            container.Register<PhilipsHueConnector>(Lifestyle.Singleton);
            container.Collection.Register<ILightTechnologyConnector>(typeof(PhilipsHueConnector));
            container.Register<CredentialsStorage>(Lifestyle.Singleton);

            // UI
            container.Register<IHueConfigurationUI, ConsoleHueConfigurationUI>(Lifestyle.Singleton);


            try {
                container.Verify();
            } catch (Exception ex) {
                Debug.Print(ex.Message);
                System.Environment.Exit(-1);
            }
        }

        static async Task Main(string[] args)
        {
            var actionSource = container.GetInstance<ILightActionSource>();
            actionSource.Start();

            IRenderer whateverBridge = null;

            foreach(var connector in container.GetAllInstances<ILightTechnologyConnector>()) {
                var bridges = connector.GetBridges();

                if (bridges.Count > 0) {
                    whateverBridge = bridges[0];
                    break;
                }
            }

            if (whateverBridge == null) {
                Console.WriteLine("Couldn't find a bridge. Exiting.");
                System.Environment.Exit(-1);
            }

            await whateverBridge.Connect();

            var lightsOnBridge = await whateverBridge.GetLights();
            
            actionSource.Subscribe(new SimpleLightActionObserver(lightsOnBridge));

            Console.Read();
        }

    }
}
