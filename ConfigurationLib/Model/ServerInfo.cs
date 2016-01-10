namespace ConfigurationLib.Model
{
    public class ServerInfo
    {
        public string Scheme { get; set; }

        public string Hostname { get; set; }

        public ushort Port { get; set; }

        public ServerInfo()
        {
            Hostname = DefaultConfiguration.DEFAULT_SERVER_HOSTNAME;
            Port = DefaultConfiguration.DEFAULT_SERVER_PORT;
            Scheme = DefaultConfiguration.DEFAULT_SERVER_SCHEME;
        }

        public override string ToString()
        {
            return string.Format("{0}://{1}:{2}/", Scheme, Hostname, Port);
        }
    }
}
