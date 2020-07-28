using System;
using System.Collections.Generic;
using System.Text;

using RestSharp;
using RestSharp.Serializers.SystemTextJson;

using ChatsPlayHue.Light;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ChatsPlayHue.Renderers.PhilipsHue
{
    public class UserCreationRequestParameters {
        [JsonPropertyName("devicetype")]
        public string DeviceType {get; set;}

        [JsonPropertyName("generateclientkey")]
        public bool GenerateClientKey {get; set;}

        public UserCreationRequestParameters(string deviceType, bool generateClientKey) {
            this.DeviceType = deviceType;
            this.GenerateClientKey = generateClientKey;
        }
    }
}