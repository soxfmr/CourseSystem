using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer
{
    public static class GlobalSettings
    {
        public const string CONNECTION_STRING_SQLSERVER     = "Data Source={0},{1};User ID={2};Password={3};Initial Catalog={4};";

        public const string CONNECTION_STRING_MSSQL         = "server={0};port={1};uid={2};password={3};database={4};";

        public const string NAMESPACE                       = "CourseServer.Controllers";

        public const string APPKEY                          = "EC5883451BB7D0AA6B5950E39ED5F16D";

        public const string DEFAULT_SERVER_HOSTNAME         = "localhost";

        public const ushort DEFAULT_SERVER_PORT             = 2333;

        public const string DEFAULT_SERVER_SCHEME           = "http";

        public const int DEFAULT_SESSION_LIFETIME           = 30;

        public const string KEY_DATABASE                    = "CourseService";

        public static ConnectionStringSettings DATABASE     = ConfigurationManager.ConnectionStrings[KEY_DATABASE];
    }
}
