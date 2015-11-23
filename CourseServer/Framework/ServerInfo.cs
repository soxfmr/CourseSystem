using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Framework
{
    public class ServerInfo
    {
        public string Scheme { get; set; }

        public string Hostname { get; set; }

        public ushort Port { get; set; }

        public ServerInfo()
        {
            Hostname = GlobalSettings.DEFAULT_SERVER_HOSTNAME;
            Port = GlobalSettings.DEFAULT_SERVER_PORT;
            Scheme = GlobalSettings.DEFAULT_SERVER_SCHEME;
        }

        public override string ToString()
        {
            return string.Format("{0}://{1}:{2}/", Scheme, Hostname, Port);
        }
    }
}
