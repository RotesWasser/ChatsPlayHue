using System;
using System.Collections.Generic;
using System.Text;

using RestSharp;
using RestSharp.Serializers.SystemTextJson;

using ChatsPlayHue.Light;
using System.Threading.Tasks;

namespace ChatsPlayHue.LightConnections.PhilipsHue
{
    public class HueAPIError {
        public int type {get; set;}
        public string address {get; set;}
        public string description {get; set;}
    }
}