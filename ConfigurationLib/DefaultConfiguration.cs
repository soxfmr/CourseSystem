using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationLib
{
    public static class DefaultConfiguration
    {
        public const string DEFAULT_CONFIG_FOLDER           = "config";

        public const string DEFAULT_CONFG_FILENAME          = "settings.xml";

        public const string DEFAULT_CONFIG_PATH             = DEFAULT_CONFIG_FOLDER + "/" + DEFAULT_CONFG_FILENAME;

        public const string DEFAULT_DEPLOY_FILENAME         = "config.xml";

        public const string DEFAULT_SERVER_HOSTNAME         = "localhost";

        public const ushort DEFAULT_SERVER_PORT             = 2333;

        public const string DEFAULT_SERVER_SCHEME           = "http";

        public const string DEFAULT_DATABASE_HOSTNAME       = "127.0.0.1";

        public const ushort DEFAULT_DATABASE_PORT           = 1433;

        public const string DEFAULT_DATABASE_USER           = "sa";

        public const int DEFAULT_DATABASE_TIMEOUT           = 20;

        public const int DEFAULT_SESSION_LIFETIME           = 30;
    }
}
