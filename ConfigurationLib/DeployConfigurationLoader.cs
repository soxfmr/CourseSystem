using ConfigurationLib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConfigurationLib
{
    public class DeployConfigurationLoader : Loader
    {
        public bool Export(ServerInfo si, string fn)
        {
            bool bRet = false;

            if (si == null)
            {
                return bRet;
            }

            try
            {
                XmlDocument doc = new XmlDocument();
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

                doc.AppendChild(eleServer);
                doc.Save(fn);

                bRet = true;
            } catch (Exception) {}

            return bRet;
        }

        public ServerInfo Load()
        {
            if (File.Exists(DefaultConfiguration.DEFAULT_DEPLOY_FILENAME))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(DefaultConfiguration.DEFAULT_DEPLOY_FILENAME);

                object[] values;
                ServerInfo si = new ServerInfo();

                XmlNode nodeRoot = doc.SelectSingleNode("server");
                if (nodeRoot == null)
                {
                    return si;
                }

                // You can get this bad code here because of the user always drive the thing out of the way normal
                // Anyway, fuck user...

                values = ParseNode(nodeRoot,
                        new string[] { "hostname", "port", "scheme" },
                        new object[] { DefaultConfiguration.DEFAULT_SERVER_HOSTNAME,
                            DefaultConfiguration.DEFAULT_SERVER_PORT,
                            DefaultConfiguration.DEFAULT_SERVER_SCHEME },
                        new Type[] { typeof(string), typeof(ushort), typeof(string) });
                si.Hostname = (string) values[0];
                si.Port = (ushort) values[1];
                si.Scheme = (string) values[2];

                return si;
            }

            return new ServerInfo();
        }
    }
}
