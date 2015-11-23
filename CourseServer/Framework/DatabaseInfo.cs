using CourseServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Framework
{
    public class DatabaseInfo
    {
        public const string TAG = "DatabaseInfo";

        public string Host { get; set; }

        public ushort Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Database { get; set; }

        public int Timeout { get; set; }

        /// <summary>
        /// Using HOST:PROT:USERNAME:PASSWORD:DATABASE in order.
        /// </summary>
        public string Format { get; set; }

        public override string ToString()
        {
            if (TextUtils.isEmpty(Format))
            {
                Dumper.Log(TAG, "Empty template for database connection string.");
                return null;
            }

            return string.Format(Format, Host, Port, Username, Password, Database);
        }
    }
}
