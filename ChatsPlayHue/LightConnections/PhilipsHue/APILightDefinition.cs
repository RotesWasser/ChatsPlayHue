using System;
using System.Collections.Generic;
using System.Text;

using RestSharp;
using RestSharp.Serializers.SystemTextJson;

using ChatsPlayHue.Light;
using System.Threading.Tasks;

namespace ChatsPlayHue.Renderers.PhilipsHue
{
    class APILightDefinition {
        public APILightState state {get; set;}
        public string type {get; set;}
        public string name {get; set;}
        public string manufacturername {get; set;}
        public string productname {get; set;}
        public APILightCapabilities capabilities {get; set;}
        public APILightConfig config {get; set;}
        public string uniqueid {get; set;}
    }

        public class APILightCapabilities {
            public bool certified {get; set;}
            public APILightCapabilitiesControl control {get; set;}
            public APILightCapabilitiesStreaming streaming {get; set;}
        }

        public class APILightCapabilitiesControl {
            public uint mindimlevel {get; set;}
            public uint maxlumen {get; set;}
            public string colorgamuttype {get; set;}
            public float[][] colorgamut {get; set;}
            public APIRange<ushort> ct {get; set;}
        }

        public class APILightCapabilitiesStreaming {
            public bool renderer {get; set;}
            public bool proxy {get; set;}
        }

        public class APILightConfig {
            public string archetype {get; set;}
            public string function {get; set;}
            public string direction {get; set;}
        }

        public class APIRange<T> {
            public T min {get; set;}
            public T max {get; set;}
        }

    public class APILightState {
            public bool on {get; set;}
            public byte bri {get; set;}
            public ushort hue {get; set;}
            public byte sat {get; set;}
            public string effect {get; set;}
            public float[] xy {get; set;}
            public string alert {get; set;}
            public string colormode {get; set;}
            public string mode {get; set;}
            public bool reachable {get; set;}
        }
}