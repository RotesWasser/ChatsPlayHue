using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using System.Linq;

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

        private Regex stateSettingRegex = new Regex(@"");
        

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

        public async Task<IList<ILight>> GetLights()
        {
            var lightsRequest = new RestRequest(string.Format("{0}/lights", credentials.Username), Method.GET);

            var lightsResponse = await client.GetAsync<IDictionary<string, APILightDefinition>>(lightsRequest);

            return lightsResponse.Select(pair => (ILight) new PhilipsHueLight(this, uint.Parse(pair.Key), pair.Value)).ToList();
        }

        internal async Task<LightPowerState> SetLightPowerState(PhilipsHueLight light, LightPowerState newState) {
            var lightStateRequest = new RestRequest(string.Format("{0}/lights/{1}/state", credentials.Username, light.BridgeLocalID), Method.PUT);
            lightStateRequest.AddJsonBody(
                new { 
                    on = newState == LightPowerState.On,
                    transitiontime = 1 }
                );

            var stateSettingResponse = (await client.PutAsync<HueAPIResponse<IDictionary<string, object>>[]>(lightStateRequest))[0];

            if (stateSettingResponse.error != null) {
                // TODO Better error handling.
                throw new Exception(stateSettingResponse.error.description);
            }

            return newState;
        }
    }
}