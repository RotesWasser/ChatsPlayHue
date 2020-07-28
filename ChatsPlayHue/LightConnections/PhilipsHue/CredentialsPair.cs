using System;
using System.Collections.Generic;
using System.Text;

using System.Configuration;

using RestSharp;
using RestSharp.Serializers.SystemTextJson;

using ChatsPlayHue.Light;

namespace ChatsPlayHue.Renderers.PhilipsHue
{
    public class CredentialsPair {
        public string Username {get; set;}
        public string ClientKey {get; set;}

        public CredentialsPair(string username, string clientKey) {
            this.Username = username;
            this.ClientKey = clientKey;
        }
    } 
}