using System;
using System.Collections.Generic;
using System.Text;

using RestSharp;
using RestSharp.Serializers.SystemTextJson;

using ChatsPlayHue.Light;

namespace ChatsPlayHue.Renderers.PhilipsHue
{
    class BridgeConfigurationResponse {
        // TODO: Add missing fields as we go.
        public string name {get; set;}
        public string apiversion {get; set;}
        public string swversion {get; set;}
        public string ipaddress {get; set;}
        public string mac {get; set;}
        public IDictionary<string, object> whitelist {get; set;}
    }
}