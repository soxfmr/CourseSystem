using System;

namespace ConfigurationLib.Model
{
    public class DatabaseInfo
    {
        public string Host { get; set; }

        public ushort Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Database { get; set; }

        public int Timeout { get; set; }

        public DatabaseInfo()
        {
            Host = DefaultConfiguration.DEFAULT_DATABASE_HOSTNAME;
            Port = DefaultConfiguration.DEFAULT_DATABASE_PORT;
            Username = DefaultConfiguration.DEFAULT_DATABASE_USER;
            Timeout = DefaultConfiguration.DEFAULT_DATABASE_TIMEOUT;
        }

        /// <summary>
        /// Using HOST:PROT:USERNAME:PASSWORD:DATABASE in order.
        /// </summary>
        public string Format { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Format))
            {
                throw new FormatException("Empty template for database connection string.");
            }

            return string.Format(Format, Host, Port, Username, Password, Database);
        }
    }
}
