using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseServer.Utils;
using CourseServer.Framework;

namespace CourseServer
{
    public class ConfigurationLoader
    {
        private const string SETTING_FILENAME = "config/settings.xml";

        public UserConfiguration Load()
        {
            if (File.Exists(SETTING_FILENAME))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(SETTING_FILENAME);

                object[] values;
                UserConfiguration config = new UserConfiguration();

                XmlNode nodeRoot = doc.SelectSingleNode("settings");
                if (nodeRoot == null)
                {
                    return config;
                }

                // You can get this bad code here because of the user always drive the thing out of the way normal
                // Anyway, fuck user...

                values = ParseNode(nodeRoot,
                    new string[] { "maintenance", "sessionLifetime" },
                    new object[] { false, GlobalSettings.DEFAULT_SESSION_LIFETIME },
                    new Type[] { typeof(bool), typeof(int) });
                config.Maintenance = (bool) values[0];
                config.SessionLifetime = (int) values[1];

                XmlNode nodeServer = nodeRoot.SelectSingleNode("server");
                if (nodeServer != null)
                {
                    values = ParseNode(nodeRoot, 
                        new string[] { "hostname", "port", "scheme" },
                        new object[] { GlobalSettings.DEFAULT_SERVER_HOSTNAME,
                            GlobalSettings.DEFAULT_SERVER_PORT,
                            GlobalSettings.DEFAULT_SERVER_SCHEME }, 
                        new Type[] { typeof(string), typeof(ushort), typeof(string) });
                    config.ServerInfo.Hostname = (string) values[0];
                    config.ServerInfo.Port = (ushort) values[1];
                    config.ServerInfo.Scheme = (string) values[2];
                }

                XmlNode nodeDatabase = nodeRoot.SelectSingleNode("database");
                if (nodeDatabase != null)
                {
                    values = ParseNode(nodeRoot, 
                        new string[] { "hostname", "port", "username", "password", "database", "timeout" },
                        new object[] { "", 0, "", "", "", DbContextHelper.DEFAULT_TIMEOUT },
                        new Type[] { typeof(string), typeof(ushort), typeof(string), typeof(string), typeof(string),
                        typeof(int) });
                    config.DatabaseInfo.Host = (string) values[0];
                    config.DatabaseInfo.Port = (ushort) values[1];
                    config.DatabaseInfo.Username = (string) values[2];
                    config.DatabaseInfo.Password = (string) values[3];
                    config.DatabaseInfo.Database = (string) values[4];
                    config.DatabaseInfo.Timeout = (int) values[5];
                }
            }

            return new UserConfiguration();
        }

        private object[] ParseNode(XmlNode node, string[] fields, object[] defaults, Type[] types)
        {
            XmlNode child;

            string value;
                        
            object[] Ret = new object[fields.Length];

            for (int i = 0, len = fields.Length; i < len; i++)
            {
                // Set the default value if the node dosen't exists
                child = node.SelectSingleNode(fields[i]);
                if (child == null)
                {
                    Ret[i] = defaults[i];
                    continue;
                }

                // Set the default value if cannot convert the value properly
                // or the value read from config file is empty
                try
                {
                    value = ((XmlElement)child).InnerText;
                    if (!TextUtils.isEmpty(value))
                    {
                        Ret[i] = Convert.ChangeType(value, types[i]);
                        continue;
                    }
                }
                catch(Exception) {}

                Ret[i] = defaults[i];
            }

            return Ret;
        }
    }
}
