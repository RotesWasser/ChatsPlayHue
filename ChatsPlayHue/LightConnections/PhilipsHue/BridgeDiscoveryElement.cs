using System;
using System.Collections.Generic;
using System.Text;

using RestSharp;
using RestSharp.Serializers.SystemTextJson;

using ChatsPlayHue.Light;

namespace ChatsPlayHue.Renderers.PhilipsHue
{
    class BridgeDiscoveryElement {
        public string id {get; set;}
        public string internalipaddress {get; set;}
    }
}