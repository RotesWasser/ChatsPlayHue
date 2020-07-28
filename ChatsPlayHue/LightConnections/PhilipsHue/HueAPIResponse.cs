using System;
using System.Collections.Generic;
using System.Text;

using RestSharp;
using RestSharp.Serializers.SystemTextJson;

using ChatsPlayHue.Light;
using System.Threading.Tasks;

namespace ChatsPlayHue.Renderers.PhilipsHue
{
    public class HueAPIResponse<T> {
        public T success {get; set;}
        public HueAPIError error {get; set;}
    }
}