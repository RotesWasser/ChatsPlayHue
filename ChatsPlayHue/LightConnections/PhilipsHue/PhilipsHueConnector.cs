using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Linq;

using RestSharp;
using RestSharp.Serializers.SystemTextJson;

using ChatsPlayHue.Light;
using SimpleInjector;

namespace ChatsPlayHue.LightConnections.PhilipsHue
{
    class PhilipsHueConnector : ILightTechnologyConnector
    {
        private RestClient client;
        private Container diContainer;

        public PhilipsHueConnector(Container diContainer)
        {   
            client = new RestClient("https://discovery.meethue.com/");
            client.UseSystemTextJson();

            this.diContainer = diContainer;
        }

        public IList<ILightBridge> GetBridges()
        {
            var request = new RestRequest("/", DataFormat.Json);

            var response = client.Get<BridgeDiscoveryElement[]>(request);

            return response.Data.Select(
                x => (ILightBridge) new PhilipsHueBridge(diContainer.GetInstance<CredentialsStorage>(), diContainer.GetInstance<IHueConfigurationUI>(), x.id, x.internalipaddress)).ToList();
        }
    }
}
