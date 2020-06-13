using System;
using System.Collections.Generic;
using System.Text;

using RestSharp;
using RestSharp.Serializers.SystemTextJson;

using ChatsPlayHue.Light;
using System.Threading.Tasks;

namespace ChatsPlayHue.LightConnections.PhilipsHue
{
    public interface IHueConfigurationUI
    {
        void AskUserToPressLinkButton();

        void NotifyAboutConnectionToBridge();
    }
}