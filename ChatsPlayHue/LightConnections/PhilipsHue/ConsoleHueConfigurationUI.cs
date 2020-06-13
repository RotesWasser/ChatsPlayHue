using System;
using System.Collections.Generic;
using System.Text;

using RestSharp;
using RestSharp.Serializers.SystemTextJson;

using ChatsPlayHue.Light;
using System.Threading.Tasks;

namespace ChatsPlayHue.LightConnections.PhilipsHue
{
    class ConsoleHueConfigurationUI : IHueConfigurationUI
    {
        public void AskUserToPressLinkButton()
        {
            Console.WriteLine("Please press the Link Button on your Hue Bridge");
        }

        public void NotifyAboutConnectionToBridge()
        {
            Console.WriteLine("Connecting to Hue bridge.");
        }
    }
}