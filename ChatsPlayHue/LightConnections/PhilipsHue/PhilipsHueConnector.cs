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
        private readonly CredentialsStorage credentialsStorage;
        private readonly IHueConfigurationUI hueUI;
        private RestClient client;

        public PhilipsHueConnector(CredentialsStorage credentialsStorage, IHueConfigurationUI hueUI)
        {   
            client = new RestClient("https://discovery.meethue.com/");
            client.UseSystemTextJson();

            this.credentialsStorage = credentialsStorage;
            this.hueUI = hueUI;
        }


        public IList<ILightBridge> GetBridges()
        {
            // TODO This is terrible because we keep creating new bridges on every GetBridges call here.
            // It would be better if we stored them in a dict of bridge identifier -> bridge object.
            var request = new RestRequest("/", DataFormat.Json);

            var response = client.Get<BridgeDiscoveryElement[]>(request);

            return response.Data.Select(
                x => (ILightBridge) new PhilipsHueBridge(credentialsStorage, hueUI, x.id, x.internalipaddress)).ToList();
        }
    }
}
