using ConfigurationLib;
using ConfigurationLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManager.Config
{
    public class DeployLoader
    {
        public void Init()
        {
            DeployConfigurationLoader loader = new DeployConfigurationLoader();
            // Load the server configuration
            ServerInfo si = loader.Load();

            CourseProvider.ProviderSettings.ServerHost = si.Hostname;
            CourseProvider.ProviderSettings.ServerPort = si.Port;
            CourseProvider.ProviderSettings.ServerScheme = si.Scheme;
        }
    }
}
