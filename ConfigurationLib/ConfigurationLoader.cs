using ConfigurationLib.Model;
using System;
using System.IO;
using System.Xml;

namespace ConfigurationLib
{
    public class ConfigurationLoader : Loader
    {
        public bool Save(bool maintance, int sessionLifetime,
            ServerInfo si, DatabaseInfo di)
        {
            bool bRet = false;

            if (sessionLifetime <= 0)
            {
                return bRet;
            }

            if (si == null || di == null)
            {
                return bRet;
            }

            // Create the file
            if (!Directory.Exists(DefaultConfiguration.DEFAULT_CONFIG_FOLDER))
            {
                Directory.CreateDirectory(DefaultConfiguration.DEFAULT_CONFIG_FOLDER);
            }

            if (File.Exists(DefaultConfiguration.DEFAULT_CONFIG_PATH))
            {
                File.Delete(DefaultConfiguration.DEFAULT_CONFIG_PATH);
            }

            try
            {
                // Save the configuration
                XmlDocument doc = new XmlDocument();

                XmlElement eleRoot = doc.CreateElement("settings");
                // Server configuration
                XmlElement eleMaintance = doc.CreateElement("maintenance");
                XmlElement eleSessionLifetime = doc.CreateElement("sessionLifetime");

                XmlElement eleServer = doc.CreateElement("server");
                XmlElement eleServerAddress = doc.CreateElement("hostname");
                XmlElement eleServerPort = doc.CreateElement("port");
                XmlElement eleServerScheme = doc.CreateElement("scheme");

                eleServerAddress.InnerText = si.Hostname;
                eleServerPort.InnerText = si.Port + "";
                eleServerScheme.InnerText = si.Scheme;

                eleServer.AppendChild(eleServerAddress);
                eleServer.AppendChild(eleServerPort);
                eleServer.AppendChild(eleServerScheme);

                // Database configuration
                XmlElement eleDb = doc.CreateElement("database");
                XmlElement eleDbAddress = doc.CreateElement("hostname");
                XmlElement eleDbPort = doc.CreateElement("port");
                XmlElement eleDbUser = doc.CreateElement("username");
                XmlElement eleDbPass = doc.CreateElement("password");
                XmlElement eleDbName = doc.CreateElement("database");
                XmlElement eleDbTimeout = doc.CreateElement("timeout");

                eleDbAddress.InnerText = di.Host;
                eleDbPort.InnerText = di.Port + "";
                eleDbUser.InnerText = di.Username;
                eleDbPass.InnerText = di.Password;
                eleDbName.InnerText = di.Database;
                eleDbTimeout.InnerText = di.Timeout + "";

                eleDb.AppendChild(eleDbAddress);
                eleDb.AppendChild(eleDbPort);
                eleDb.AppendChild(eleDbUser);
                eleDb.AppendChild(eleDbPass);
                eleDb.AppendChild(eleDbName);
                eleDb.AppendChild(eleDbTimeout);

                eleMaintance.InnerText = maintance.ToString();
                eleSessionLifetime.InnerText = sessionLifetime + "";

                // Append to the root element
                eleRoot.AppendChild(eleMaintance);
                eleRoot.AppendChild(eleSessionLifetime);
                eleRoot.AppendChild(eleServer);
                eleRoot.AppendChild(eleDb);

                doc.AppendChild(eleRoot);
                doc.Save(DefaultConfiguration.DEFAULT_CONFIG_PATH);

                bRet = true;
            } catch (Exception) { }
            
            return bRet;
        }

        public UserConfiguration Load()
        {
            if (File.Exists(DefaultConfiguration.DEFAULT_CONFIG_PATH))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(DefaultConfiguration.DEFAULT_CONFIG_PATH);

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
                    new object[] { false, DefaultConfiguration.DEFAULT_SESSION_LIFETIME },
                    new Type[] { typeof(bool), typeof(int) });
                config.Maintenance = (bool) values[0];
                config.SessionLifetime = (int) values[1];

                XmlNode nodeServer = nodeRoot.SelectSingleNode("server");
                if (nodeServer != null)
                {
                    values = ParseNode(nodeServer, 
                        new string[] { "hostname", "port", "scheme" },
                        new object[] { DefaultConfiguration.DEFAULT_SERVER_HOSTNAME,
                            DefaultConfiguration.DEFAULT_SERVER_PORT,
                            DefaultConfiguration.DEFAULT_SERVER_SCHEME }, 
                        new Type[] { typeof(string), typeof(ushort), typeof(string) });
                    config.ServerInfo.Hostname = (string) values[0];
                    config.ServerInfo.Port = (ushort) values[1];
                    config.ServerInfo.Scheme = (string) values[2];
                }

                XmlNode nodeDatabase = nodeRoot.SelectSingleNode("database");
                if (nodeDatabase != null)
                {
                    values = ParseNode(nodeDatabase, 
                        new string[] { "hostname", "port", "username", "password", "database", "timeout" },
                        new object[] {
                            DefaultConfiguration.DEFAULT_DATABASE_HOSTNAME,
                            DefaultConfiguration.DEFAULT_DATABASE_PORT,
                            DefaultConfiguration.DEFAULT_DATABASE_USER,
                            "",
                            "",
                            DefaultConfiguration.DEFAULT_DATABASE_TIMEOUT
                        },
                        new Type[] { typeof(string), typeof(ushort), typeof(string), typeof(string), typeof(string),
                        typeof(int) });
                    config.DatabaseInfo.Host = (string) values[0];
                    config.DatabaseInfo.Port = (ushort) values[1];
                    config.DatabaseInfo.Username = (string) values[2];
                    config.DatabaseInfo.Password = (string) values[3];
                    config.DatabaseInfo.Database = (string) values[4];
                    config.DatabaseInfo.Timeout = (int) values[5];
                }

                return config;
            }

            return new UserConfiguration();
        }
    }
}
