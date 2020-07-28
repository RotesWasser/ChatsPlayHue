using System;
using System.Collections.Generic;
using System.Text;

using System.Configuration;

using RestSharp;
using RestSharp.Serializers.SystemTextJson;

using ChatsPlayHue.Light;

namespace ChatsPlayHue.Renderers.PhilipsHue
{
    public class CredentialsStorage {

        private Configuration configuration;

        public CredentialsStorage() {
            var roamingConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoaming);
            
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();

            configFileMap.ExeConfigFilename = roamingConfig.FilePath;
            
            configuration = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

        }

        public void StoreCredentials(string macAddress, CredentialsPair toStore) {

            var settings = configuration.AppSettings.Settings;

            if (settings["HueUsername-" + macAddress] != null) 
                settings.Remove("HueUsername-" + macAddress);

            settings.Add("HueUsername-" + macAddress, toStore.Username);


            if (settings["HueClientKey-" + macAddress] != null) 
                settings.Remove("HueClientKey-" + macAddress);

            settings.Add("HueClientKey-" + macAddress, toStore.ClientKey);


            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configuration.AppSettings.SectionInformation.Name);
        }

        public CredentialsPair GetCredentials(string macAddress) {
            var username = configuration.AppSettings.Settings["HueUsername-" + macAddress]?.Value;
            var clientKey = configuration.AppSettings.Settings["HueClientKey-" + macAddress]?.Value;

            if (username != null && clientKey != null) 
                return new CredentialsPair(username, clientKey);
            else
                return null;
        }

    }
}