using System;
using System.Collections.Generic;
using System.Text;

using RestSharp;
using RestSharp.Serializers.SystemTextJson;

using ChatsPlayHue.Light;
using System.Threading.Tasks;
using System.Net;

namespace ChatsPlayHue.LightConnections.PhilipsHue
{
    public class PhilipsHueBridge : ILightBridge
    {
        private RestClient client;

        private string id;
        private string bridgeIP;
        private string mac;
        private CredentialsStorage credentialsStorage;
        private IHueConfigurationUI configurationUI;
        private CredentialsPair credentials;
        

        public PhilipsHueBridge(
            CredentialsStorage credentialsStorage,
            IHueConfigurationUI configurationUI, 
            string id, 
            string bridgeIP) {
            this.id = id;
            this.bridgeIP = bridgeIP;
            this.credentialsStorage = credentialsStorage;
            this.configurationUI = configurationUI;

            client = new RestClient(string.Format("http://{0}/api", this.bridgeIP));
            client.UseSystemTextJson();
        }

        public async Task Connect()
        {
            var configRequest = new RestRequest("/whatever/config", DataFormat.Json);

            var configurationResponse = client.Get<BridgeConfigurationResponse>(configRequest);
            mac = configurationResponse.Data.mac;

            credentials = credentialsStorage.GetCredentials(this.mac);

            if (credentials == null) {
                await CreateBridgeUser();
            } else {
                // TODO Test credentials pair.
                var testConfigRequest = new RestRequest(string.Format("/{0}/config", credentials.Username), DataFormat.Json);

                var testConfigResponse = client.Get<BridgeConfigurationResponse>(testConfigRequest);

                if (testConfigResponse.Data.whitelist == null) {
                    await CreateBridgeUser();
                }
            }
        }

        private async Task CreateBridgeUser() {
                var userCreationRequestParameters = new UserCreationRequestParameters("ChatsPlayHue#devmachine", true);

                var userCreationRequest = new RestRequest("/", DataFormat.Json);
                userCreationRequest.AddJsonBody(userCreationRequestParameters);

                var accountCreationResponse = client.Post<HueAPIResponse<UserCreationResponse>[]>(userCreationRequest);

                while (accountCreationResponse.Data[0].error != null) {
                    configurationUI.AskUserToPressLinkButton();
                    accountCreationResponse = client.Post<HueAPIResponse<UserCreationResponse>[]>(userCreationRequest);
                    await Task.Delay(5000);
                }

                configurationUI.NotifyAboutConnectionToBridge();
                
                credentials = new CredentialsPair(
                    accountCreationResponse.Data[0].success.username,
                    accountCreationResponse.Data[0].success.clientkey);

                credentialsStorage.StoreCredentials(mac, credentials);
        }

        public IList<ILight> GetLights()
        {
            // TODO Continue here.
            throw new NotImplementedException();
        }
    }
}