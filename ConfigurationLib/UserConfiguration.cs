using ConfigurationLib.Model;

namespace ConfigurationLib
{
    public class UserConfiguration
    {
        public UserConfiguration()
        {
            Maintenance = false;
            SessionLifetime = DefaultConfiguration.DEFAULT_SESSION_LIFETIME;

            ServerInfo = new ServerInfo();
            DatabaseInfo = new DatabaseInfo();
        }

        /// <summary>
        /// Determind that the server is been maintain status.
        /// </summary>
        public bool Maintenance { get; set; }

        /// <summary>
        /// The life time of the session for each user.
        /// </summary>
        public int SessionLifetime { get; set; }
        
        public ServerInfo ServerInfo { get; set; }

        public DatabaseInfo DatabaseInfo { get; set; }
    }
}
