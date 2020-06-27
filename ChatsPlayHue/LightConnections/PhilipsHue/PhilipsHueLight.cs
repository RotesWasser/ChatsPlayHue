using System;
using System.Collections.Generic;
using System.Text;

using RestSharp;
using RestSharp.Serializers.SystemTextJson;

using ChatsPlayHue.Light;
using System.Threading.Tasks;
using System.Drawing;

namespace ChatsPlayHue.LightConnections.PhilipsHue
{
    class PhilipsHueLight : ILight {
        public uint BridgeLocalID {get; private set; }
        public string Name {get; private set; }


        private readonly PhilipsHueBridge parent;

        public PhilipsHueLight(PhilipsHueBridge parent, uint bridgeLocalID, APILightDefinition definition) {
            this.parent = parent;
            this.BridgeLocalID = bridgeLocalID;
            this.Name = definition.name;
        }


        public Task<Color> GetLightColor()
        {
            throw new NotImplementedException();
        }

        public async Task SetLightColor(Color newColor)
        {
            try {
                await parent.SetLightColor(this, newColor);
            } catch (Exception e) {
                Console.WriteLine(e);
            }
        }

        public Task<LightPowerState> GetPowerState()
        {
            throw new NotImplementedException();
        }

        public async Task SetPowerState(LightPowerState newState)
        {
            try {
                await parent.SetLightPowerState(this, newState);
            } catch (Exception e) {
                Console.WriteLine(e);
            }
        }
    }
}